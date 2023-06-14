<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <div class="ticket_form_container">
        <label>Subject</label>
        <input class="input__field" />
        <label>Condition</label>
        <input class="input__field" />
        <label>Background</label>
        <ckeditor
          v-model="backgroundField"
          :editor="editor"
          :config="editorConfig"
        ></ckeditor>

        <label>Contents</label>
        <ckeditor :editor="editor" :config="editorConfig"></ckeditor>
        <label>Reasons</label>
        <ckeditor :editor="editor" :config="editorConfig"></ckeditor>
        <label>Others</label>
        <ckeditor :editor="editor" :config="editorConfig"></ckeditor>
        <label>Attached Documents</label>
        <input class="input__field" type="file" multiple />
        <label>Checked By</label>
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
        <label>Approvers</label>
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
      </div>
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button class="button-primary mr-2" @click="save">Submit</button>
          <button class="button-transparent" @click="$emit('close')">
            Cancel
          </button>
        </div>
      </div>
    </template>
  </Modal>
</template>
<script>
import Modal from "@/components/Modal.vue";
import VueMultiselect from "vue-multiselect";
import "vue-multiselect/dist/vue-multiselect.css";

import BalloonEditor from "@ckeditor/ckeditor5-editor-balloon/src/ballooneditor";
import EssentialsPlugin from "@ckeditor/ckeditor5-essentials/src/essentials";
import BoldPlugin from "@ckeditor/ckeditor5-basic-styles/src/bold";
import ItalicPlugin from "@ckeditor/ckeditor5-basic-styles/src/italic";
import ParagraphPlugin from "@ckeditor/ckeditor5-paragraph/src/paragraph";
import PasteFromOffice from "@ckeditor/ckeditor5-paste-from-office/src/pastefromoffice";
import Table from "@ckeditor/ckeditor5-table/src/table";
import TableToolbar from "@ckeditor/ckeditor5-table/src/tabletoolbar";
import Highlight from "@ckeditor/ckeditor5-highlight/src/highlight";
import Alignment from "@ckeditor/ckeditor5-alignment/src/alignment";
import TableColumnResize from "@ckeditor/ckeditor5-table/src/tablecolumnresize";

import { useStore } from "vuex";
import { computed, onMounted, ref } from "vue";
export default {
  emits: ["close"],
  components: {
    Modal,
    VueMultiselect,
  },

  setup() {
    const store = useStore();
    const VITE_TINY_API_KEY = ref(import.meta.env.VITE_TINY_API_KEY);
    const backgroundField = ref("");
    const editor = BalloonEditor;
    const approvers = ref([]);
    const checkers = ref([]);
    const isLoading = ref(false);
    const selectedChecker = ref([]);
    const selectedApprover = ref([]);

    onMounted(async () => {
      store.commit("app/SET_LOADING", true);
      await store.dispatch("user/fetchApprovers");
      await store.dispatch("user/fetchCheckers");
      store.commit("app/SET_LOADING", false);
      isLoading.value = store.state.app.isLoading;
      approvers.value = store.state.user.approvers;
      checkers.value = store.state.user.checkers;
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
        Highlight,
        Alignment,
        TableColumnResize,
      ],

      toolbar: {
        items: ["bold", "italic", "|", "insertTable", "alignment", "highlight"],
      },
    };

    const save = () => {};
    return {
      save,
      VITE_TINY_API_KEY,
      editor,
      editorConfig,
      selectedChecker,
      selectedApprover,
      backgroundField,
      approvers,
      checkers,
      isLoading,
    };
  },
};
</script>
