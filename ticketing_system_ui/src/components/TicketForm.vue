<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <label>Subject</label>
      <input />
      <label>Background</label>
      <Editor
        :api-key="VITE_TINY_API_KEY"
        :init="{
          plugins: 'lists table',
          statusbar: false,
          menubar: 'format table',
        }"
      />
      <label>Contents</label>
      <CustomTextArea />
      <label>Reasons</label>
      <CustomTextArea />
      <label>Others</label>
      <CustomTextArea />
      <label>Attached Documents</label>
      <CustomTextArea />
    </template>
    <template v-slot:footer>
      <button>Next</button>
    </template>
  </Modal>
</template>
<script>
import Modal from "@/components/Modal.vue";
import CustomTextArea from "@/components/CustomTextArea.vue";
import Editor from "@tinymce/tinymce-vue";

import { ref } from "vue";
export default {
  emits: ["close"],
  components: {
    Modal,
    CustomTextArea,
    Editor,
  },

  setup() {
    const VITE_TINY_API_KEY = ref(import.meta.env.VITE_TINY_API_KEY);

    return {
      VITE_TINY_API_KEY,
    };
  },
};
</script>
