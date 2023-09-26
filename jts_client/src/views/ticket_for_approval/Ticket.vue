<template>
  <div class="flex flex-col gap-3">
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
        <div class="flex gap-2 items-end justify-end relative w-[680px] mb-1">
          <div class="absolute left-0 w-[180px]">
            <label>Ticket Id</label>
            <input class="input__field h-8" v-model="ticketIdSearchField" />
          </div>
          <div class="absolute left-[189px] w-[165px]">
            <label>Date Created</label>
            <input
              class="input__field h-8"
              type="date"
              v-model="dateCreatedSearchField"
            />
          </div>
          <div class="absolute w-[165px] left-[365px]">
            <label>Prepared By</label>
            <VueMultiselect
              v-model="preparedBy"
              :options="allUsers"
              label="ext_name"
              :show-labels="false"
            />
          </div>
          <div class="flex flex-col absolute left-[540px]">
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
    <!--  <component :is="currentTab" /> -->
    <TicketTab :ticketStatus="currentStatus" :headers="currentHeader" />
  </div>
</template>

<script>
import RejectionReasonModal from "@/components/RejectionReasonModal.vue";
import TicketForm from "@/components/TicketForm.vue";
import VueMultiselect from "vue-multiselect";
import TicketTab from "@/views/ticket_for_approval/TicketTab.vue";
import { useRouter, useRoute } from "vue-router";
import { useStore } from "vuex";
import { TICKET_STATUS } from "@/util/constant";
import { computed, ref, onUnmounted, watch, onMounted } from "vue";
import { useSignalR } from "@quangdao/vue-signalr";

export default {
  components: {
    TicketForm,
    RejectionReasonModal,
    TicketTab,
    VueMultiselect,
  },

  setup() {
    const signalR = useSignalR();
    const router = useRouter();
    const store = useStore();
    const route = useRoute();
    const ticketIdSearchField = ref("");
    const dateCreatedSearchField = ref("");
    const preparedBy = ref({});

    const currentStatus = ref(route.params.status);
    const isTicketFormOpen = computed(() => store.state.app.isTicketFormOpen);
    const allUsers = ref([]);

    const isSelectedRowEmpty = computed(() =>
      store.state.app.selectedTicket.ticket == null ? true : false
    );

    const pendingHeader = [
      {
        width: "80px",
        title: "Priority",
        align: "start",
        sortable: false,
        key: "ticket.priority.name",
      },
      {
        title: "Ticket ID",
        align: "start",
        sortable: false,
        key: "ticket.ticket_id",
      },
      {
        title: "Subject",
        key: "ticket.subject",
        align: "start",
        sortable: false,
      },
      {
        title: "Prepared By",
        key: "ticket.created_by.user.ext_name",
        align: "start",
        sortable: false,
      },

      {
        title: "Date Created",
        key: "ticket.date_created",
        align: "start",
        sortable: false,
      },
    ];

    const approvedHeader = [
      {
        width: "80px",
        title: "Priority",
        align: "start",
        sortable: false,
        key: "ticket.priority.name",
      },
      {
        title: "Ticket ID",
        align: "start",
        sortable: false,
        key: "ticket.ticket_id",
      },
      {
        title: "Subject",
        key: "ticket.subject",
        align: "start",
        sortable: false,
      },
      {
        title: "Prepared By",
        key: "ticket.created_by.user.ext_name",
        align: "start",
        sortable: false,
      },

      {
        title: "Date Created",
        key: "ticket.date_created",
        align: "start",
        sortable: false,
      },

      {
        title: "Approved Date",
        key: "action_date",
        align: "start",
        sortable: false,
      },
    ];

    const rejectedHeader = [
      {
        width: "80px",
        title: "Priority",
        align: "start",
        sortable: false,
        key: "ticket.priority.name",
      },
      {
        title: "Ticket ID",
        align: "start",
        sortable: false,
        key: "ticket.ticket_id",
      },
      {
        title: "Subject",
        key: "ticket.subject",
        align: "start",
        sortable: false,
      },
      {
        title: "Prepared By",
        key: "ticket.created_by.user.ext_name",
        align: "start",
        sortable: false,
      },

      {
        title: "Date Created",
        key: "ticket.date_created",
        align: "start",
        sortable: false,
      },

      {
        title: "Rejected Date",
        key: "action_date",
        align: "start",
        sortable: false,
      },
    ];

    const doneHeader = [
      {
        width: "80px",
        title: "Priority",
        align: "start",
        sortable: false,
        key: "ticket.priority.name",
      },
      {
        title: "Ticket ID",
        align: "start",
        sortable: false,
        key: "ticket.ticket_id",
      },
      {
        title: "Subject",
        key: "ticket.subject",
        align: "start",
        sortable: false,
      },
      {
        title: "Prepared By",
        key: "ticket.created_by.user.ext_name",
        align: "start",
        sortable: false,
      },

      {
        title: "Date Created",
        key: "ticket.date_created",
        align: "start",
        sortable: false,
      },

      {
        title: "Approved Date",
        key: "action_date",
        align: "start",
        sortable: false,
      },
    ];

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

    const setHeader = (status) => {
      switch (status) {
        case TICKET_STATUS.PENDING:
          return pendingHeader;
        case TICKET_STATUS.APPROVED:
          return approvedHeader;
        case TICKET_STATUS.REJECTED:
          return rejectedHeader;
        case TICKET_STATUS.DONE:
          return doneHeader;
      }
    };

    const currentHeader = ref(setHeader(currentStatus.value));

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

    const changeTab = (tab) => {
      currentStatus.value = tab.status;
      currentHeader.value = setHeader(tab.status);
      router.replace({
        name: "TicketForApproval",
        params: { status: tab.status },
      });
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
      const param = {
        items_per_page: 0,
        offset: 0,
        username: "",
        full_name: "",
      };
      await store.dispatch("user/fetchAllUsers", param);
      allUsers.value = [...store.state.user.users.users].map((u) => u.user);
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

    signalR.on("GetTicketForApproval", (ticket) => {
      store.commit("app/SET_SEARCH", String(Date.now()));
    });

    const clear = () => {
      store.commit("app/SET_SELECTED_TICKET", {});
      store.commit("app/SET_SEARCH_TICKET_ID", 0);
      store.commit("app/SET_SEARCH_CREATED_DATE", "1/1/1, 12:00:00");
      ticketIdSearchField.value = "";
      dateCreatedSearchField.value = "";
      preparedBy.value = {};
      store.commit("app/SET_SEARCH", String(Date.now()));
    };

    onUnmounted(() => {
      clear();
    });

    return {
      tabs,
      router,
      currentStatus,
      allUsers,
      isSelectedRowEmpty,
      isTicketFormOpen,
      preparedBy,
      ticketIdSearchField,
      dateCreatedSearchField,
      currentHeader,
      clear,
      search,
      changeTab,

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
