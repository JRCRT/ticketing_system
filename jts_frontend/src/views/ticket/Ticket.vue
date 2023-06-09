<template>
  <div class="flex flex-col gap-3">
    <NewTicketForm v-if="modalActive" @close="closeModal" />
    <TicketForm v-if="isTicketFormOpen" @close="closeTicketForm" />
    <h4 class="text-primary">Ticket</h4>
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
          <button class="w-14 button-transparent" @click="openModal">
            New
          </button>
        </div>
      </div>
      <div class="border-b w-full absolute bottom-0"></div>
    </div>
    <KeepAlive>
      <component :is="currentTab" />
    </KeepAlive>
  </div>
</template>

<script>
import PendingTicket from "@/views/Ticket/PendingTicket.vue";
import ApprovedTicket from "@/views/Ticket/ApprovedTicket.vue";
import DeclinedTicket from "@/views/Ticket/DeclinedTicket.vue";
import NewTicketForm from "@/components/NewTicketForm.vue";
import TicketForm from "@/components/TicketForm.vue";
import { useRouter, useRoute } from "vue-router";
import { useStore } from "vuex";
import { TICKET_STATUS } from "@/util/constant";
import { computed, ref } from "vue";

export default {
  components: {
    PendingTicket,
    ApprovedTicket,
    DeclinedTicket,
    NewTicketForm,
    TicketForm,
  },

  setup() {
    const router = useRouter();
    const store = useStore();
    const route = useRoute();
    const currentStatus = ref(route.params.status);
    const isTicketFormOpen = computed(() => store.state.app.isTicketFormOpen);
    const setTabOnMount = (status) => {
      switch (status) {
        case TICKET_STATUS.PENDING:
          return "PendingTicket";
        case TICKET_STATUS.APPROVED:
          return "ApprovedTicket";
        case TICKET_STATUS.DECLINED:
          return "DeclinedTicket";
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
    ];
    const modalActive = ref(false);

    function closeModal() {
      modalActive.value = false;
    }

    const closeTicketForm = () => {
      store.commit("app/SET_TICKET_FORM", false);
      router.replace({
        name: "Ticket",
        params: { status: currentStatus.value },
      });
    };

    function openModal() {
      modalActive.value = true;
    }

    function changeTab(tab) {
      currentTab.value = tab.name;
      currentStatus.value = tab.status;
      router.replace({ name: "Ticket", params: { status: tab.status } });
      store.commit("app/SET_SELECTED_TICKET", {});
    }

    const openTicket = async () => {
      const ticketId = store.state.app.selectedTicket.ticket.ticket_id;
      router.push({
        name: "TicketById",
        params: { status: currentStatus.value, ticketId: ticketId },
      });
    };

    return {
      currentTab,
      tabs,
      modalActive,
      router,
      currentStatus,
      closeModal,
      openModal,
      changeTab,
      setTabOnMount,
      openTicket,
      isSelectedRowEmpty,
      isTicketFormOpen,
      closeTicketForm,
    };
  },
};
</script>
