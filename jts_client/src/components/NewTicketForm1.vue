<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">New</h5>
    </template>
    <template v-slot:content>
      <div class="new_ticket_form_container">
        <label>Subject (Required)</label>
        <input class="input__field" v-model="subject" />
        <label>Condition</label>
        <input class="input__field" v-model="condition" />
        <label>Background (Required)</label>
        <ckeditor
          v-model="background"
          :editor="editor"
          :config="editorConfig"
        ></ckeditor>
        <label>Contents (Required)</label>
        <ckeditor
          :editor="editor"
          :config="editorConfig"
          v-model="content"
        ></ckeditor>
        <label>Reasons (Required)</label>
        <ckeditor
          :editor="editor"
          :config="editorConfig"
          v-model="reason"
        ></ckeditor>
        <label>Others</label>
        <ckeditor
          :editor="editor"
          :config="editorConfig"
          v-model="other"
        ></ckeditor>
        <label>Attached Documents</label>
        <FileUploader :isMultiple="true" />
        <label>Priority</label>
        <VueMultiselect
          v-model="selectedPriority"
          :options="priorities"
          label="name"
          :show-labels="false"
          :allow-empty="false"
        />
        <label>Checked By (Required)</label>
        <VueMultiselect
          @select="selectChecker"
          @remove="removeSelectedChecker"
          v-model="selectedChecker"
          :options="checkers"
          label="ext_name"
          :multiple="true"
          :taggable="true"
          :show-labels="false"
          :loading="isLoading"
          track-by="user_id"
        />
        <label>Approved By (Required)</label>
        <VueMultiselect
          v-model="selectedApprover"
          :options="approvers"
          :multiple="true"
          :taggable="true"
          :show-labels="false"
          :loading="isLoading"
          track-by="user_id"
          label="ext_name"
        />
        <label>Related Partys</label>
        <VueMultiselect
          v-model="selectedRelatedPary"
          :options="relatedParty"
          :multiple="true"
          :taggable="true"
          :show-labels="false"
          :loading="isLoading"
          track-by="user_id"
          label="ext_name"
        />
        <br />
      </div>
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button
            class="button-primary mr-2"
            :disabled="isProcessing"
            @click="submitTicket"
          >
            {{ isProcessing ? "Submiting..." : "Submit" }}
          </button>
          <button
            :disabled="isProcessing"
            class="button-transparent border"
            @click="$emit('close')"
          >
            Cancel
          </button>
        </div>
      </div>
    </template>
  </Modal>
</template>

<script>
import FileUploader from "@/components/FileUploader.vue";
import Modal from "@/components/Modal.vue";
import VueMultiselect from "vue-multiselect";
import ClassicEditor from "@ckeditor/ckeditor5-editor-classic/src/classiceditor";
import EssentialsPlugin from "@ckeditor/ckeditor5-essentials/src/essentials";
import BoldPlugin from "@ckeditor/ckeditor5-basic-styles/src/bold";
import ItalicPlugin from "@ckeditor/ckeditor5-basic-styles/src/italic";
import ParagraphPlugin from "@ckeditor/ckeditor5-paragraph/src/paragraph";
import PasteFromOffice from "@ckeditor/ckeditor5-paste-from-office/src/pastefromoffice";
import Table from "@ckeditor/ckeditor5-table/src/table";
import TableToolbar from "@ckeditor/ckeditor5-table/src/tabletoolbar";
import TableProperties from "@ckeditor/ckeditor5-table/src/tableproperties";
import TableCellProperties from "@ckeditor/ckeditor5-table/src/tablecellproperties";
import Highlight from "@ckeditor/ckeditor5-highlight/src/highlight";
import Alignment from "@ckeditor/ckeditor5-alignment/src/alignment";
import TableColumnResize from "@ckeditor/ckeditor5-table/src/tablecolumnresize";

import { useStore } from "vuex";
import { computed, onMounted, onUnmounted, ref } from "vue";
import { SIGNATORY_TYPE } from "@/util/constant";
import { formatDateTime } from "@/util/helper";
import { Signatory } from "@/models/Signatory";
import { useSignalR } from "@quangdao/vue-signalr";

export default {
  emits: ["close"],
  components: {
    Modal,
    VueMultiselect,
    FileUploader,
  },

  setup() {
    const signalR = useSignalR();
    const PENDING_STATUS = 1;
    const currentDate = new Date();

    const isProcessing = computed(() => store.state.app.isProcessing);
    const connectionId = signalR.connection.connectionId;
    const formattedDatetime = formatDateTime(currentDate);

    const store = useStore();

    const background = ref("");
    const subject = ref("");
    const condition = ref("");
    const content = ref("");
    const reason = ref("");
    const other = ref("");

    const editor = ClassicEditor;
    const approvers = ref([]);
    const checkers = ref([]);
    const relatedParty = ref([]);
    const priorities = ref([]);

    const isLoading = ref(false);
    const selectedChecker = ref([]);
    const selectedApprover = ref([]);
    const selectedRelatedPary = ref([]);
    const selectedPriority = ref(priorities.value[0]);

    const uploadedFiles = computed(() => store.getters["file/files"]);

    const currentUser = JSON.parse(localStorage.getItem("user"));

    const selectedSignatories = () => {
      var formattedApprover = [...selectedApprover.value].map(
        (approver) =>
          new Signatory({
            user_id: approver.user_id,
            type: SIGNATORY_TYPE.APPROVER,
          })
      );

      var formattedChecker = [...selectedChecker.value].map(
        (checker) =>
          new Signatory({
            user_id: checker.user_id,
            type: SIGNATORY_TYPE.CHECKER,
          })
      );

      var formattedParty = [...selectedRelatedPary.value].map(
        (party) =>
          new Signatory({
            user_id: party.user_id,
            type: SIGNATORY_TYPE.PARTY,
          })
      );

      var signatories = formattedApprover.concat(
        formattedChecker,
        formattedParty
      );

      return signatories;
    };

    const validate = () => {
      var alert;
      var hasError = false;
      if (!subject.value) {
        alert = {
          type: "danger",
          message: "Please fill up the subject field.",
        };
        hasError = true;
      } else if (!background.value) {
        alert = {
          type: "danger",
          message: "Please fill up the background field.",
        };
        hasError = true;
      } else if (!content.value) {
        alert = {
          type: "danger",
          message: "Please fill up the content field.",
        };
        hasError = true;
      } else if (!reason.value) {
        alert = { type: "danger", message: "Please fill up the reason field." };
        hasError = true;
      } else if (!selectedApprover.value.length) {
        alert = {
          type: "danger",
          message: "Please select at least 1 approver.",
        };
        hasError = true;
      } else if (!selectedChecker.value.length) {
        alert = {
          type: "danger",
          message: "Please select at least 1 checker.",
        };
        hasError = true;
      }

      if (hasError) {
        store.dispatch("app/addAlert", alert);
      }

      return hasError;
    };

    function selectChecker(selectedOption) {
      const newRelatedParty = [...relatedParty.value].filter(
        (u) => u.user_id != selectedOption.user_id
      );
      relatedParty.value = newRelatedParty;
    }

    function removeSelectedChecker(removedOption) {
      relatedParty.value = relatedParty.value.concat(removedOption);
    }

    function selectApprover(selectedOption) {
      const newRelatedParty = [...relatedParty.value].filter(
        (u) => u.user_id != selectedOption.user_id
      );
      relatedParty.value = newRelatedParty;

      /* const newApprovers = [...approvers.value].filter(
        (u) =>
          u.role.name !== selectedOption.role.name ||
          u.user_id === selectedOption.user_id
      );

      approvers.value = newApprovers; */
    }

    function removeSelectedApprover(removedOption) {
      relatedParty.value = relatedParty.value.concat(removedOption);
    }

    function selectRelatedParty(selectedOption) {
      const newCheckers = [...checkers.value].filter(
        (u) => u.user_id != selectedOption.user_id
      );
      const newApprovers = [...approvers.value].filter(
        (u) => u.user_id != selectedOption.user_id
      );

      checkers.value = newCheckers;
      approvers.value = newApprovers;
    }

    function removeSelectedParty(removedOption) {
      switch (removedOption.role.name) {
        case "Checker":
          checkers.value = checkers.value.concat(removedOption);
          break;
        case "Approver":
          approvers.value = approvers.value.concat(removedOption);
          break;
      }
    }

    const submitTicket = async () => {
      if (!validate()) {
        var formData = new FormData();
        uploadedFiles.value.forEach((file) => {
          formData.append("files", file);
        });
        formData.append("others", other.value);
        formData.append("connection_id", connectionId);
        formData.append("subject", subject.value);
        formData.append("condition", condition.value);
        formData.append("background", background.value);
        formData.append("content", content.value);
        formData.append("reason", reason.value);
        formData.append("status_id", PENDING_STATUS);
        formData.append("created_by", currentUser.user_id);
        formData.append("priority_id", selectedPriority.value.priority_id);
        formData.append("date_created", formattedDatetime);
        formData.append("signatories", JSON.stringify(selectedSignatories()));

        await store.dispatch("ticket/createTicket", formData);
      }
    };

    onMounted(async () => {
      const param = {
        department_id: currentUser.department.department_id,
        user_id: currentUser.user_id,
      };

      store.commit("app/SET_MODAL_LOADING", true);
      await store.dispatch("user/fetchApprovers");
      await store.dispatch("user/fetchCheckers", param);
      await store.dispatch("user/fetchRelatedParties", currentUser.user_id);
      await store.dispatch("priority/fetchPriorities");

      store.commit("app/SET_MODAL_LOADING", false);
      isLoading.value = store.state.app.isLoading;
      approvers.value = [...store.state.user.approvers].map((u) => u.user);
      checkers.value = [...store.state.user.checkers].map((u) => u.user);
      relatedParty.value = [...store.state.user.relatedParties].map(
        (u) => u.user
      );
      priorities.value = store.state.priority.priorities;
      selectedPriority.value = store.state.priority.priorities[0];
    });

    onUnmounted(() => {
      store.commit("file/EMPTY_FILES", []);
    });

    const editorConfig = {
      plugins: [
        EssentialsPlugin,
        BoldPlugin,
        ItalicPlugin,
        ParagraphPlugin,
        PasteFromOffice,
        Table,
        TableToolbar,
        TableProperties,
        TableCellProperties,
        Highlight,
        Alignment,
        TableColumnResize,
      ],
      table: {
        contentToolbar: [
          "tableColumn",
          "tableRow",
          "mergeTableCells",
          "tableProperties",
          "tableCellProperties",
        ],
      },
      toolbar: {
        items: ["bold", "italic", "|", "insertTable", "alignment", "highlight"],
      },
    };

    return {
      submitTicket,
      selectChecker,
      removeSelectedChecker,
      selectRelatedParty,
      removeSelectedParty,
      selectApprover,
      editor,
      editorConfig,
      selectedChecker,
      selectedApprover,
      selectedRelatedPary,
      background,
      subject,
      condition,
      content,
      reason,
      other,
      approvers,
      checkers,
      relatedParty,
      isLoading,
      priorities,
      selectedPriority,
      isProcessing,
    };
  },
};
</script>
