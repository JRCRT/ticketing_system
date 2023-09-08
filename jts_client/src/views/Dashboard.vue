<template>
  <div class="flex flex-col gap-3">
    <h4 @click="getSelectedRow" class="text-primary">Dashboard</h4>
    <!--Cards Container-->
    <div class="flex">
      <div @click="navigateToTicket(TICKET_STATUS.PENDING)" class="card">
        <div class="card-content">
          <h1>{{ pendingNum }}</h1>
          <p>Pending</p>
        </div>
      </div>
      <div @click="navigateToTicket(TICKET_STATUS.APPROVED)" class="card">
        <div class="card-content">
          <h1>{{ approvedNum }}</h1>
          <p>Approved</p>
        </div>
      </div>
      <div @click="navigateToTicket(TICKET_STATUS.REJECTED)" class="card">
        <div class="card-content">
          <h1>{{ rejectedNum }}</h1>
          <p>Rejected</p>
        </div>
      </div>
      <div @click="navigateToTicket(TICKET_STATUS.DONE)" class="card">
        <div class="card-content">
          <h1>{{ doneNum }}</h1>
          <p>Done</p>
        </div>
      </div>
    </div>
    <!--Today's Transactions-->
    <div>
      <div class="flex items-center justify-between mb-2">
        <h4 class="text-primary">Today's Ticket</h4>
        <button
          class="w-14 button-transparent mr-2 disabled:bg-lightSecondary disabled:border-none"
          :disabled="isSelectedRowEmpty"
          @click="openTicket"
        >
          Open
        </button>
      </div>

      <Table
        :columnDefs="columnDefs"
        @grid-ready="onGridReady"
        @selection-changed="onSelectionChanged"
      ></Table>
    </div>
  </div>
</template>

<script>
import Table from "@/components/Table.vue";
import TicketStatus from "@/components/TicketStatus.vue";
import FormattedDate from "@/components/FormattedDate.vue";

import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { onMounted, ref, computed, watch, onUnmounted } from "vue";
import { TICKET_STATUS, ROLE } from "@/util/constant";
export default {
  name: "Dashboard",

  components: {
    Table,
    TicketStatus,
    FormattedDate,
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    const gridAPI = ref(null);
    const pendingNum = ref(0);
    const approvedNum = ref(0);
    const rejectedNum = ref(0);
    const doneNum = ref(0);

    const isSelectedRowEmpty = computed(() =>
      store.state.app.selectedTicket.ticket == null ? true : false
    );
    const isTicketFormOpen = computed(() => store.state.app.isTicketFormOpen);
    const currentUser = JSON.parse(localStorage.getItem("user"));

    const columnDefs = [
      { headerName: "Ticket ID", field: "ticket.ticket_id", flex: 1 },
      { headerName: "Subject", field: "ticket.subject", flex: 2 },
      {
        headerName: "Prepared By",
        field: "ticket.created_by.user.ext_name",
        flex: 1,
      },
      {
        headerName: "Date created",
        field: "ticket.date_created",
        flex: 1,
        cellRenderer: FormattedDate,
      },
      {
        headerName: "Status",
        field: "ticket.status.name",
        flex: 1,
        cellRenderer: TicketStatus,
      },
    ];

    const navigateToTicket = (status) => {
      switch (currentUser.role.name) {
        case ROLE.USER:
          router.replace({ name: "MyTicket", params: { status: status } });
          break;
        case ROLE.ADMIN:
          router.replace({ name: "Ticket", params: { status: status } });
          break;
        default:
          router.replace({
            name: "TicketForApproval",
            params: { status: status },
          });
          break;
      }
    };

    const onGridReady = async (params) => {
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      const userId = currentUser.user_id;
      await store.dispatch("ticket/fetchAllTodaysTickets", userId);
      params.api.setRowData(store.state.ticket.todaysTickets);
    };

    const getSelectedRow = () => {
      console.log(gridAPI.value.getSelectedRows());
    };

    const openTicket = () => {
      const ticketId = store.state.app.selectedTicket.ticket.ticket_id;
      router.push({
        name: "DashboardTicketById",
        params: { ticketId: ticketId },
      });
    };

    const onSelectionChanged = () => {
      const selectedRow = gridAPI.value.getSelectedRows();
      store.commit("app/SET_SELECTED_TICKET", selectedRow[0]);
    };

    onUnmounted(() => {
      store.commit("app/SET_SELECTED_TICKET", {});
    });

    onMounted(async () => {
      const paramPending = {
        user_id: currentUser.user_id,
        status_id: 1,
      };
      const paramApproved = {
        user_id: currentUser.user_id,
        status_id: 2,
      };
      const paramRejected = {
        user_id: currentUser.user_id,
        status_id: 3,
      };
      const paramDone = {
        user_id: currentUser.user_id,
        status_id: 4,
      };
      store.commit("app/SET_LOADING", true);
      switch (currentUser.role.name) {
        case ROLE.USER:
          await store.dispatch("ticket/fetchMyPendingTickets", paramPending);
          await store.dispatch("ticket/fetchMyApprovedTickets", paramApproved);
          await store.dispatch("ticket/fetchMyRejectedTickets", paramRejected);
          await store.dispatch("ticket/fetchMyDoneTickets", paramDone);
          pendingNum.value = store.state.ticket.myPendingTickets.length;
          approvedNum.value = store.state.ticket.myApprovedTickets.length;
          rejectedNum.value = store.state.ticket.myRejectedTickets.length;
          doneNum.value = store.state.ticket.myDoneTickets.length;
          break;
        case ROLE.ADMIN:
          await store.dispatch("ticket/fetchAllPendingTickets");
          await store.dispatch("ticket/fetchAllApprovedTickets");
          await store.dispatch("ticket/fetchAllRejectedTickets");
          await store.dispatch("ticket/fetchAllDoneTickets");
          pendingNum.value = store.state.ticket.allPendingTickets.length;
          approvedNum.value = store.state.ticket.allApprovedTickets.length;
          rejectedNum.value = store.state.ticket.allRejectedTickets.length;
          doneNum.value = store.state.ticket.allDoneTickets.length;
          break;
        default:
          await store.dispatch(
            "ticket/fetchPendingTicketsForApproval",
            paramPending
          );
          await store.dispatch(
            "ticket/fetchApprovedTicketsForApproval",
            paramApproved
          );
          await store.dispatch(
            "ticket/fetchRejectedTicketsForApproval",
            paramRejected
          );
          await store.dispatch("ticket/fetchDoneTicketsForApproval", paramDone);
          pendingNum.value =
            store.state.ticket.pendingTicketsForApproval.length;
          approvedNum.value =
            store.state.ticket.approvedTicketsForApproval.length;
          rejectedNum.value =
            store.state.ticket.rejectedTicketsForApproval.length;
          doneNum.value = store.state.ticket.doneTicketsForApproval.length;
          break;
      }
      store.commit("app/SET_LOADING", false);
    });

    watch(
      () => isTicketFormOpen.value,
      async (newIsTicketFormOpen, oldIsTicketFormOpen) => {
        if (!newIsTicketFormOpen) {
          router.replace({
            name: "Dashboard",
          });
        }
      }
    );

    return {
      columnDefs,
      pendingNum,
      approvedNum,
      rejectedNum,
      doneNum,
      navigateToTicket,
      onGridReady,
      getSelectedRow,
      openTicket,
      onSelectionChanged,
      TICKET_STATUS,
      isSelectedRowEmpty,
    };
  },
};
</script>
