<template>
  <div class="flex flex-col gap-3">
    <NewTicketForm v-if="isNewTicketFormOpen" @close="closeModal" />

    <h4 class="text-primary">My Tickets</h4>
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

        <div class="flex gap-2 items-end justify-end relative w-[593px] mb-1">
          <div class="absolute left-0 w-[188px]">
            <label>Ticket Id</label>
            <input class="input__field h-8" v-model="ticketIdSearchField" />
          </div>
          <div class="absolute left-[198px] w-[180px]">
            <label>Date Created</label>
            <input
              class="input__field h-8"
              type="date"
              v-model="dateCreatedSearchField"
            />
          </div>

          <button
            class="w-16 mr-4 border button-transparent disabled:bg-lightSecondary disabled:border-none"
            @click="search"
          >
            Search
          </button>
          <button
            class="w-14 border button-transparent disabled:bg-lightSecondary disabled:border-none"
            :disabled="isSelectedRowEmpty"
            @click="openTicket"
          >
            Open
          </button>
          <button class="w-14 border button-transparent" @click="openModal">
            New
          </button>
        </div>
      </div>

      <div class="border-b w-full absolute bottom-0"></div>
    </div>
    <component :is="currentTab" />
  </div>
</template>

<script>
import PendingTicket from "@/views/my_ticket/PendingTicket.vue";
import ApprovedTicket from "@/views/my_ticket/ApprovedTicket.vue";
import RejectedTicket from "@/views/my_ticket/RejectedTicket.vue";
import DoneTicket from "@/views/my_ticket/DoneTicket.vue";
import NewTicketForm from "@/components/NewTicketForm.vue";
import TicketForm from "@/components/TicketForm.vue";

import { useRouter, useRoute } from "vue-router";
import { useStore } from "vuex";
import { TICKET_STATUS } from "@/util/constant";
import { computed, ref, onUnmounted, watch, onMounted } from "vue";

export default {
  components: {
    PendingTicket,
    ApprovedTicket,
    RejectedTicket,
    DoneTicket,
    NewTicketForm,
    TicketForm,
  },

  setup() {
    const router = useRouter();
    const store = useStore();
    const route = useRoute();
    const ticketIdSearchField = ref("");
    const dateCreatedSearchField = ref("");
    const currentStatus = ref(route.params.status);
    const isTicketFormOpen = computed(() => store.state.app.isTicketFormOpen);
    const setTabOnMount = (status) => {
      switch (status) {
        case TICKET_STATUS.PENDING:
          return "PendingTicket";
        case TICKET_STATUS.APPROVED:
          return "ApprovedTicket";
        case TICKET_STATUS.REJECTED:
          return "RejectedTicket";
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
        name: "RejectedTicket",
        label: "Rejected",
        status: TICKET_STATUS.REJECTED,
      },
      {
        name: "DoneTicket",
        label: "Done",
        status: TICKET_STATUS.DONE,
      },
    ];

    const isNewTicketFormOpen = computed(
      () => store.state.app.isNewTicketFormOpen
    );

    function closeModal() {
      store.commit("app/SET_NEW_TICKET_FORM", false);
    }

    watch(
      () => isTicketFormOpen.value,
      async (newIsTicketFormOpen, oldIsTicketFormOpen) => {
        if (!newIsTicketFormOpen) {
          router.replace({
            name: "MyTicket",
            params: { status: currentStatus.value },
          });
        }
      }
    );

    function openModal() {
      store.commit("app/SET_NEW_TICKET_FORM", true);
    }

    function changeTab(tab) {
      currentTab.value = tab.name;
      currentStatus.value = tab.status;
      router.replace({ name: "MyTicket", params: { status: tab.status } });
      store.commit("app/SET_SELECTED_TICKET", {});
    }

    const openTicket = async () => {
      const ticketId = store.state.app.selectedTicket.ticket.ticket_id;
      router.push({
        name: "MyTicketById",
        params: { status: currentStatus.value, ticketId: ticketId },
      });
    };

    const search = () => {
      console.log(new Date(`${dateCreatedSearchField.value}, 12:00:00`));
      store.commit("app/SET_SEARCH", String(Date.now()));
      store.commit(
        "app/SET_SEARCH_TICKET_ID",
        ticketIdSearchField.value === "" ? 0 : Number(ticketIdSearchField.value)
      );
      store.commit(
        "app/SET_SEARCH_CREATED_DATE",
        dateCreatedSearchField.value === ""
          ? "1/1/1, 12:00:00"
          : `${dateCreatedSearchField.value}, 12:00:00`
      );
    };

    onUnmounted(() => {
      store.commit("app/SET_SELECTED_TICKET", {});
    });

    return {
      currentTab,
      tabs,
      isNewTicketFormOpen,
      router,
      currentStatus,
      isSelectedRowEmpty,
      isTicketFormOpen,
      ticketIdSearchField,
      dateCreatedSearchField,
      search,
      closeModal,
      openModal,
      changeTab,
      setTabOnMount,
      openTicket,
    };
  },
};
</script>
