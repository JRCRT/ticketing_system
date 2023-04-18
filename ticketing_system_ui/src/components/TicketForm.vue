<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <label>Subject</label>
      <select>
        <option v-for="subject in subjects">{{ subject }}</option>
      </select>
      <label>Background</label>
      <textarea />
      <label>Contents</label>
      <textarea />
      <label>Reasons</label>
      <textarea />
      <label>Others</label>
      <textarea />
      <label>Attached Documents</label>
      <textarea />
    </template>
    <template v-slot:footer>
      <button>Next</button>
    </template>
  </Modal>
</template>
<script>
import Modal from "@/components/Modal.vue";
export default {
  emits: ["close"],
  components: {
    Modal,
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
