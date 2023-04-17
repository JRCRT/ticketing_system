<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>Ticket 001</h5>
    </template>
    <template v-slot:content>
      <select>
        <option v-for="subject in subjects">{{ subject }}</option>
      </select>
      <input />
      <input />
      <input />
    </template>
  </Modal>
</template>
<script>
export default {
  emits: ["close"],
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
