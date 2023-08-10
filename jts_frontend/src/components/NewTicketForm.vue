<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">New</h5>
    </template>
    <template v-slot:content>
      <div class="ticket_form_container">
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
        <FileUploader />
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
            class="button-transparent"
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
import "vue-multiselect/dist/vue-multiselect.css";

import BalloonEditor from "@ckeditor/ckeditor5-editor-balloon/src/ballooneditor";
//import InlineEditor from "@ckeditor/ckeditor5-editor-inline/src/inlineeditor";

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
import "@/stylesheet/content-style.css";

import { useStore } from "vuex";
import { computed, onMounted, onUnmounted, ref } from "vue";
import { SIGNATORY_TYPE } from "@/util/constant";
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
    const DEFAULT_DATE_TIME = "2023-06-26T03:51:19.632Z";
    const currentDate = new Date();
    const formatter = new Intl.DateTimeFormat("en-US", {
      year: "numeric",
      month: "2-digit",
      day: "2-digit",
      hour: "2-digit",
      minute: "2-digit",
      second: "2-digit",
      timeZone: "UTC",
    });

    const isProcessing = computed(() => store.state.app.isProcessing);
    const connectionId = signalR.connection.connectionId;
    const formattedDatetime = formatter.format(currentDate);

    const store = useStore();
    const VITE_TINY_API_KEY = ref(import.meta.env.VITE_TINY_API_KEY);
    const background = ref("");
    const subject = ref("");
    const condition = ref("");
    const content = ref("");
    const reason = ref("");
    const other = ref("");

    const editor = BalloonEditor;
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
        formData.append("user_id", currentUser.user_id);
        formData.append("priority_id", selectedPriority.value.priority_id);
        formData.append("date_created", formattedDatetime);
        formData.append("date_approved", DEFAULT_DATE_TIME);
        formData.append("date_declined", DEFAULT_DATE_TIME);
        formData.append("signatories", JSON.stringify(selectedSignatories()));

        await store.dispatch("ticket/createTicket", formData);
      }
    };

    onMounted(async () => {
      store.commit("app/SET_MODAL_LOADING", true);
      await store.dispatch("user/fetchApprovers");
      await store.dispatch("user/fetchCheckers");
      await store.dispatch("user/fetchAllUsers");
      await store.dispatch("priority/fetchPriorities");

      store.commit("app/SET_MODAL_LOADING", false);
      isLoading.value = store.state.app.isLoading;
      approvers.value = store.state.user.approvers;
      checkers.value = store.state.user.checkers;
      relatedParty.value = store.state.user.users;
      priorities.value = store.state.priority.priorities;
      selectedPriority.value = store.state.priority.priorities[0];
    });

    onUnmounted(() => {
      store.commit("file/EMPTY_FILE", []);
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

    /* tableProperties: {
          defaultProperties: {
            borderStyle: "solid",
            borderColor: "black",
            borderWidth: "1px",
            alignment: "left",
            width: "550px",
            height: "450px",
          },
        },
        tableCellProperties: {
          defaultProperties: {
            borderStyle: "solid",
            borderColor: "black",
            borderWidth: "1px",
            horizontalAlignment: "center",
            verticalAlignment: "bottom",
            padding: "10px",
          },
        }, */

    return {
      submitTicket,
      VITE_TINY_API_KEY,
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
