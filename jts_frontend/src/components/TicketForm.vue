<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>{{ ticket?.ticket?.ticket_id }}</h5>
    </template>
    <template v-slot:content>
      <div class="ticket_form_container">
        <label>Subject</label>
        <div class="rf-detail-container">
          {{ ticket?.ticket?.subject }}
        </div>
        <label>Background</label>
        <div class="rf-detail-container">
          <div v-html="ticket?.ticket?.background"></div>
        </div>
        <label>Contents</label>
        <div class="rf-detail-container">
          <div v-html="ticket?.ticket?.content"></div>
        </div>
        <label>Reasons</label>
        <div class="rf-detail-container">
          <div v-html="ticket?.ticket?.reason"></div>
        </div>
        <label>Others</label>
        <div class="rf-detail-container">
          <div v-html="ticket?.ticket?.others"></div>
        </div>
        <label>Attached Documents</label>
        <div class="rf-detail-container"></div>
        <label>Prepared By</label>
        <div class="rf-detail-container">
          <div v-html="ticket?.ticket?.user?.ext_name"></div>
        </div>
      </div>
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

import { useStore } from "vuex";
import { useRoute } from "vue-router";
import { ref, watch } from "vue";
export default {
  emits: ["close"],
  components: {
    Modal,
  },

  setup() {
    const store = useStore();
    const route = useRoute();
    const ticket = ref({});

    watch(
      () => route.params.ticketId,
      async (newTicketId, oldTicketId) => {
        if (newTicketId) {
          await store.dispatch("ticket/fetchTicket", newTicketId);
          ticket.value = store.state.ticket.ticket;
          console.log(ticket.value);
        }
      },
      { immediate: true }
    );
    return { ticket };
  },
};
</script>
