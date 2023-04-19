<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <label>Subject</label>
      <input />
      <label>Background</label>
      <CustomTextArea />
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
export default {
  emits: ["close"],
  components: {
    Modal,
    CustomTextArea,
  },
  data() {
    return {
      subjects: [
        "Additional Unit",
        "Additional Unit",
        "Additional Unit",
        "Additional Unit",
        "Additional Unit",
      ],
    };
  },
};
</script>
