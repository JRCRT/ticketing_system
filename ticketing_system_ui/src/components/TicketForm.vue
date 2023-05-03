<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <label>Subject</label>
      <input class="input__field" />
      <label>Background</label>
      <ckeditor :editor="editor" :config="editorConfig"></ckeditor>
      <label>Contents</label>
      <ckeditor :editor="editor" :config="editorConfig"></ckeditor>
      <label>Reasons</label>
      <ckeditor :editor="editor" :config="editorConfig"></ckeditor>
      <label>Others</label>
      <ckeditor :editor="editor" :config="editorConfig"></ckeditor>
      <label>Attached Documents</label>
      <input class="input__field" type="file" />
      <label>Checked By</label>
      <VueMultiselect
        v-model="selectedChecker"
        :options="options"
        :multiple="true"
        :taggable="true"
      />
      <label>Approvers</label>
      <VueMultiselect
        v-model="selectedApprover"
        :options="options"
        :multiple="true"
        :taggable="true"
      />
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button class="button-primary mr-2">Submit</button>
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

import { ref } from "vue";
export default {
  emits: ["close"],
  components: {
    Modal,
    VueMultiselect,
  },

  setup() {
    const VITE_TINY_API_KEY = ref(import.meta.env.VITE_TINY_API_KEY);
    const editor = BalloonEditor;
    const options = [
      "option 1",
      "option 2",
      "option 3",
      "option 4",
      "option 5",
    ];

    const selectedChecker = ref(null);
    const selectedApprover = ref(null);
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
    return {
      VITE_TINY_API_KEY,
      editor,
      editorConfig,
      options,
      selectedChecker,
      selectedApprover,
    };
  },
};
</script>

<style>
.multiselect__tags {
  border: 1px solid rgb(176, 176, 176) !important;
}
</style>
