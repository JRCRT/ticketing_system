<template>
  <div class="flex flex-col gap-3">
    <NewTicketForm v-if="modalActive" @close="closeModal" />

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
        <div class="flex gap-2 items-end justify-end relative w-[712px] mb-1">
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
          <div class="absolute w-[180px] left-[386px]">
            <label>Prepared By</label>
            <VueMultiselect
              v-model="preparedBy"
              :options="allUsers"
              label="ext_name"
              :show-labels="false"
            />
          </div>
          <div class="flex flex-col absolute left-[576px]">
            <button
              class="w-16 border button-transparent disabled:bg-lightSecondary disabled:border-none"
              @click="clear"
            >
              Clear
            </button>
            <button
              class="w-16 border button-transparent disabled:bg-lightSecondary disabled:border-none"
              @click="search"
            >
              Search
            </button>
          </div>

          <button
            class="w-14 border button-transparent mr-2 disabled:bg-lightSecondary disabled:border-none"
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
import RejectedTicket from "@/views/ticket_for_approval/RejectedTicket.vue";
import DoneTicket from "@/views/ticket_for_approval/DoneTicket.vue";
import NewTicketForm from "@/components/NewTicketForm.vue";
import RejectionReasonModal from "@/components/RejectionReasonModal.vue";
import TicketForm from "@/components/TicketForm.vue";
import VueMultiselect from "vue-multiselect";
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
    RejectionReasonModal,
    VueMultiselect,
  },

  setup() {
    const router = useRouter();
    const store = useStore();
    const route = useRoute();
    const ticketIdSearchField = ref("");
    const dateCreatedSearchField = ref("");
    const preparedBy = ref({});
    const currentStatus = ref(route.params.status);
    const isTicketFormOpen = computed(() => store.state.app.isTicketFormOpen);
    const allUsers = ref([]);
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
      clear();
    };

    const openTicket = () => {
      const ticketId = store.state.app.selectedTicket.ticket.ticket_id;
      router.push({
        name: "TicketForApprovalById",
        params: { status: currentStatus.value, ticketId: ticketId },
      });
    };

    onMounted(async () => {
      await store.dispatch("user/fetchAllUsers");
      allUsers.value = [...store.state.user.users].map((u) => u.user);
    });

    const search = () => {
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
      store.commit(
        "app/SET_SEARCH_PREPARED_BY",
        preparedBy.value?.user_id ?? 0
      );
    };

    const clear = () => {
      store.commit("app/SET_SELECTED_TICKET", {});
      store.commit("app/SET_SEARCH_TICKET_ID", 0);
      store.commit("app/SET_SEARCH_CREATED_DATE", "1/1/1, 12:00:00");
      ticketIdSearchField.value = "";
      dateCreatedSearchField.value = "";
      preparedBy.value = {};
    };

    onUnmounted(() => {
      clear();
    });

    return {
      currentTab,
      tabs,
      modalActive,
      router,
      currentStatus,
      allUsers,
      isSelectedRowEmpty,
      isTicketFormOpen,
      preparedBy,
      ticketIdSearchField,
      dateCreatedSearchField,
      clear,
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

<style scoped>
:deep(.multiselect) {
  min-height: 34px !important;
  height: 34px !important;
}

:deep(.multiselect__tags) {
  min-height: 34px !important;
  height: 34px !important;
}
:deep(.multiselect__placeholder) {
  padding-top: 0px !important;
}

:deep(.multiselect__single) {
  font-size: 14px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
</style>
