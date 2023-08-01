<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">{{ ticket?.ticket?.ticket_id }}</h5>
    </template>
    <template v-slot:content>
      <div class="ticket_form_container">
        <label>Date Requested</label>
        <div class="rf-detail-container">
          {{ requestedDate }}
        </div>
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
        <div v-if="isPending" class="w-44 flex mx-auto">
          <button class="button-primary mr-2" @click="approved">Approved</button>
          <button class="button-transparent" @click="$emit('close')">
            Declined
          </button>
        </div>

        <div v-else class="w-44 flex mx-auto">
          <button class="button-primary mr-2" >Download</button>
          <button class="button-transparent">
            Print
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
import { useSignalR } from "@quangdao/vue-signalr";
export default {
  emits: ["close"],
  components: {
    Modal,
  },

  setup() {
    const signalR = useSignalR();
    const store = useStore();
    const route = useRoute();
    const ticket = ref({});
    const requestedDate = ref(null);
    const APPROVED_STATUS_ID = 2;
    const currentUser = JSON.parse(localStorage.getItem("user"));
    const isPending = ref(false);
    const signatory = ref({});

    const approved = async () =>{
      const signatoryId = signatory.value.signatory_id;
      const connectionId = signalR.connection.connectionId;
      
      const approver = {
        signatory_id: signatoryId,
        status_id: APPROVED_STATUS_ID,
        connection_id: connectionId
      }
      console.log(approver)
      await store.dispatch("ticket/changeApprovalStatus", approver);
    }

    watch(
      () => route.params.ticketId,
      async (newTicketId, oldTicketId) => {
        if (newTicketId) {
          await store.dispatch("ticket/fetchTicket", newTicketId);
          const fetchedTicket = store.state.ticket.ticket;
          signatory.value = fetchedTicket.signatories.find(s => s.user.user_id == currentUser.user_id);
          isPending.value = signatory.value.status.name === 'Pending'; 
          ticket.value = fetchedTicket;
          requestedDate.value = Intl.DateTimeFormat("en-US").format(
            new Date(ticket.value.ticket.date_created)
          );
        
        }
      },
      { immediate: true }
    );
    return { ticket, requestedDate, isPending, approved };
  },
};
</script>
