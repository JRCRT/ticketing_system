<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <div class="ticket_form_container">
        <label>Subject</label>
        <div class="rf-detail-container"></div>
        <label>Background</label>
        <div class="rf-detail-container"></div>
        <label>Contents</label>
        <div class="rf-detail-container"></div>
        <label>Reasons</label>
        <div class="rf-detail-container"></div>
        <label>Others</label>
        <div class="rf-detail-container"></div>
        <label>Attached Documents</label>
        <div class="rf-detail-container"></div>
        <label>Prepared By</label>
        <div class="rf-detail-container"></div>
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

import { useStore } from "vuex";
import { computed, onMounted, ref } from "vue";
export default {
  emits: ["close"],
  components: {
    Modal,
  },

  setup() {
    return {};
  },
};
</script>
