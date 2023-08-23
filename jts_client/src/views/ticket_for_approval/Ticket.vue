<template>
  <div class="flex flex-col gap-3">
    <NewTicketForm v-if="modalActive" @close="closeModal" />
    <TicketForm v-if="isTicketFormOpen" @close="closeTicketForm" />
    <DeclineReasonModal
      v-if="isDeclineReasonModalOpen"
      @close="closeDeclineReasonModal"
    />
    <h4 class="text-primary">Tickets For Approval</h4>
    <div class="relative">
      <div class="flex justify-between">
        <div class="flex">
          <div
            @click="changeTab(tab)"
            :class="
              router.currentRoute.value.params.status == tab.status
                ? 'active__tab'
                : 'tab'
            "
            v-for="tab in tabs"
            :key="tab"
          >
            {{ tab.label }}
          </div>
        </div>
        <div>
          <button
            class="w-14 button-transparent mr-2 disabled:bg-lightSecondary disabled:border-none"
            :disabled="isSelectedRowEmpty"
            @click="openTicket"
          >
            Open
          </button>
        </div>
      </div>
      <div class="border-b w-full absolute bottom-0"></div>
    </div>
    <component :is="currentTab" />
  </div>
</template>

<script>
import PendingTicket from "@/views/ticket_for_approval/PendingTicket.vue";
import ApprovedTicket from "@/views/ticket_for_approval/ApprovedTicket.vue";
import DeclinedTicket from "@/views/ticket_for_approval/DeclinedTicket.vue";
import DoneTicket from "@/views/ticket_for_approval/DoneTicket.vue";
import NewTicketForm from "@/components/NewTicketForm.vue";
import DeclineReasonModal from "@/components/DeclineReasonModal.vue";
import TicketForm from "@/components/TicketForm.vue";
import { useRouter, useRoute } from "vue-router";
import { useStore } from "vuex";
import { TICKET_STATUS } from "@/util/constant";
import { computed, ref, onUnmounted, watch } from "vue";

export default {
  components: {
    PendingTicket,
    ApprovedTicket,
    DeclinedTicket,
    DoneTicket,
    NewTicketForm,
    TicketForm,
    DeclineReasonModal,
  },

  setup() {
    const router = useRouter();
    const store = useStore();
    const route = useRoute();
    const currentStatus = ref(route.params.status);
    const isTicketFormOpen = computed(() => store.state.app.isTicketFormOpen);
    const isDeclineReasonModalOpen = computed(
      () => store.state.app.isDeclineReasonModalOpen
    );

    const setTabOnMount = (status) => {
      switch (status) {
        case TICKET_STATUS.PENDING:
          return "PendingTicket";
        case TICKET_STATUS.APPROVED:
          return "ApprovedTicket";
        case TICKET_STATUS.DECLINED:
          return "DeclinedTicket";
        case TICKET_STATUS.DONE:
          return "DoneTicket";
      }
    };

    const currentTab = ref(setTabOnMount(currentStatus.value));
    const isSelectedRowEmpty = computed(() =>
      store.state.app.selectedTicket.ticket == null ? true : false
    );

    const tabs = [
      {
        name: "PendingTicket",
        label: "Pending",
        status: TICKET_STATUS.PENDING,
      },
      {
        name: "ApprovedTicket",
        label: "Approved",
        status: TICKET_STATUS.APPROVED,
      },
      {
        name: "DeclinedTicket",
        label: "Declined",
        status: TICKET_STATUS.DECLINED,
      },
      {
        name: "DoneTicket",
        label: "Done",
        status: TICKET_STATUS.DONE,
      },
    ];

    const modalActive = ref(false);

    const closeModal = () => {
      modalActive.value = false;
    };

    watch(
      () => isTicketFormOpen.value,
      async (newIsTicketFormOpen, oldIsTicketFormOpen) => {
        if (!newIsTicketFormOpen) {
          router.replace({
            name: "TicketForApproval",
            params: { status: currentStatus.value },
          });
        }
      }
    );

    const closeTicketForm = () => {
      store.commit("app/SET_TICKET_FORM", false);
    };

    const closeDeclineReasonModal = () => {
      store.commit("app/SET_DECLINE_REASON_MODAL", false);
    };

    const openModal = () => {
      modalActive.value = true;
    };

    const changeTab = (tab) => {
      currentTab.value = tab.name;
      currentStatus.value = tab.status;
      router.replace({
        name: "TicketForApproval",
        params: { status: tab.status },
      });
      store.commit("app/SET_SELECTED_TICKET", {});
    };

    const openTicket = () => {
      const ticketId = store.state.app.selectedTicket.ticket.ticket_id;
      router.push({
        name: "TicketForApprovalById",
        params: { status: currentStatus.value, ticketId: ticketId },
      });
    };

    onUnmounted(() => {
      store.commit("app/SET_SELECTED_TICKET", {});
    });

    return {
      currentTab,
      tabs,
      modalActive,
      router,
      currentStatus,
      isDeclineReasonModalOpen,
      isSelectedRowEmpty,
      isTicketFormOpen,
      closeModal,
      openModal,
      changeTab,
      setTabOnMount,
      openTicket,
      closeTicketForm,
      closeDeclineReasonModal,
    };
  },
};
</script>