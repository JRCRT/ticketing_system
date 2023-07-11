<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <div class="ticket_form_container">
        <label>Subject</label>
        <div class="rf-detail-container">{{ ticket.ticket.subject }}</div>
        <label>Background</label>
        <div class="rf-detail-container">
          <div v-html="ticket.ticket.background"></div>
        </div>
        <label>Contents</label>
        <div class="rf-detail-container">
          <div v-html="ticket.ticket.content"></div>
        </div>
        <label>Reasons</label>
        <div class="rf-detail-container">
          <div v-html="ticket.ticket.reason"></div>
        </div>
        <label>Others</label>
        <div class="rf-detail-container">
          <div v-html="ticket.ticket.background"></div>
        </div>
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
import { useRouter } from "vue-router";
import { computed, onMounted, ref } from "vue";
export default {
  emits: ["close"],
  components: {
    Modal,
  },

  setup() {
    const store = useStore();
    const router = useRouter();
    const ticket = ref({});

    onMounted(async () => {
      const ticketId = router.currentRoute.value.query.TicketId;
      await store.dispatch("ticket/fetchTicket", ticketId);
      ticket.value = store.state.ticket.ticket;
      console.log(store.state.ticket.ticket);
    });
    return { ticket };
  },
};
</script>
