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
      <input />
      <label>Contents</label>
      <input />
      <label>Reasons</label>
      <input />
      <label>Others</label>
      <input />
      <label>Attached Documents</label>
      <input />
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
