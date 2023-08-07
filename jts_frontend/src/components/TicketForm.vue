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
        <label v-if="isApproved">Date Approved</label>
        <div v-if="isApproved" class="rf-detail-container">
          {{ dateApproved }}
        </div>

        <label v-if="signatoryStatus == TICKET_STATUS.APPROVED"
          >Date You Approved</label
        >
        <div
          v-if="signatoryStatus == TICKET_STATUS.APPROVED"
          class="rf-detail-container"
        >
          {{ actionDate }}
        </div>

        <label v-if="signatoryStatus == TICKET_STATUS.DECLINED"
          >Date You Declined</label
        >
        <div
          v-if="signatoryStatus == TICKET_STATUS.DECLINED"
          class="rf-detail-container"
        >
          {{ actionDate }}
        </div>
      </div>
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div v-if="isPending && isSignatory" class="w-44 flex mx-auto">
          <button
            class="button-primary mr-2"
            :disabled="isProcessing"
            @click="approved"
          >
            {{ isProcessing ? "Approving..." : "Approved" }}
          </button>

          <button class="button-transparent" @click="openDeclineReasonModal">
            Declined
          </button>
        </div>

        <div v-else class="w-44 flex mx-auto">
          <button class="button-primary mr-2">Download</button>
          <button class="button-transparent">Print</button>
        </div>
      </div>
    </template>
  </Modal>
</template>
<script>
import Modal from "@/components/Modal.vue";

import { useStore } from "vuex";
import { useRoute } from "vue-router";
import { ref, watch, computed } from "vue";
import { useSignalR } from "@quangdao/vue-signalr";
import { TICKET_STATUS, ROLE } from "@/util/constant";
import { formatDate } from "@/util/helper";

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
    const dateApproved = ref(null);
    const APPROVED_STATUS_ID = 2;
    const currentUser = JSON.parse(localStorage.getItem("user"));
    const isPending = ref(false);
    const signatory = ref({});
    const isSignatory = ref(false);
    const isApproved = ref(false);
    const isProcessing = computed(() => store.state.app.isProcessing);
    const signatoryStatus = ref(null);
    const actionDate = ref(null);

    const approved = async () => {
      const signatoryId = signatory.value.signatory_id;
      const connectionId = signalR.connection.connectionId;

      const approver = {
        signatory_id: signatoryId,
        connection_id: connectionId,
      };

      await store.dispatch("ticket/approveTicket", approver);
    };

    const openDeclineReasonModal = () => {
      const signatoryId = signatory.value.signatory_id;
      const connectionId = signalR.connection.connectionId;
      store.commit("app/SET_SIGNATORY", {
        signatoryId: signatoryId,
        connectionId: connectionId,
      });
      store.commit("app/SET_DECLINE_REASON_MODAL", true);
    };

    watch(
      () => route.params.ticketId,
      async (newTicketId, oldTicketId) => {
        if (newTicketId) {
          await store.dispatch("ticket/fetchTicket", newTicketId);
          const fetchedTicket = store.state.ticket.ticket;

          if (
            currentUser.roleModel.name == ROLE.ADMIN ||
            currentUser.roleModel.name == ROLE.CHECKER ||
            currentUser.roleModel.name == ROLE.APPROVER
          ) {
            isSignatory.value = true;
            signatory.value = fetchedTicket.signatories.find(
              (s) => s.user.user_id == currentUser.user_id
            );

            if (signatory.value != null) {
              signatoryStatus.value = signatory.value?.status?.name;
              actionDate.value = formatDate(signatory.value?.action_date);
            }

            isPending.value =
              signatory.value?.status?.name === TICKET_STATUS.PENDING;
          }

          ticket.value = fetchedTicket;

          requestedDate.value = formatDate(ticket.value.ticket.date_created);

          dateApproved.value = formatDate(ticket.value.ticket.date_approved);

          isApproved.value =
            ticket.value.ticket.status.name == TICKET_STATUS.APPROVED;
        }
      },
      { immediate: true }
    );
    return {
      ticket,
      requestedDate,
      isPending,
      isSignatory,
      dateApproved,
      isApproved,
      isProcessing,
      signatoryStatus,
      TICKET_STATUS,
      actionDate,
      approved,
      openDeclineReasonModal,
    };
  },
};
</script>
