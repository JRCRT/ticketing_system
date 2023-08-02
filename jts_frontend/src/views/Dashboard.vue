<script>
import Table from "@/components/Table.vue";
import TicketStatus from "@/components/TicketStatus.vue";
import FormattedDate from "@/components/FormattedDate.vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { onMounted, ref } from "vue";
import { TICKET_STATUS } from "@/util/constant";
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
    const declinedNum = ref(0);

    const columnDefs = [
      { headerName: "Ticket ID", field: "ticket.ticket_id", flex: 1 },
      { headerName: "Subject", field: "ticket.subject", flex: 2 },
      {
        headerName: "Prepared By",
        field: "ticket.user.ext_name",
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
      router.replace({ name: "Ticket", params: { status: status } });
    };

    const onGridReady = async (params) => {
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("ticket/fetchAllTodaysTickets");
      params.api.setRowData(store.state.ticket.todaysTickets);
    };

    const getSelectedRow = () => {
      console.log(gridAPI.value.getSelectedRows());
    };

    onMounted(async () => {
      store.commit("app/SET_LOADING", true);
      await store.dispatch("ticket/fetchAllPendingTickets");
      await store.dispatch("ticket/fetchAllApprovedTickets");
      await store.dispatch("ticket/fetchAllDeclinedTickets");
      store.commit("app/SET_LOADING", false);
      pendingNum.value = store.state.ticket.allPendingTickets.length;
      approvedNum.value = store.state.ticket.allApprovedTickets.length;
      declinedNum.value = store.state.ticket.allDeclinedTickets.length;
    });

    return {
      columnDefs,
      pendingNum,
      approvedNum,
      declinedNum,
      navigateToTicket,
      onGridReady,
      getSelectedRow,
      TICKET_STATUS,
    };
  },
};
</script>
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
      <div @click="navigateToTicket(TICKET_STATUS.DECLINED)" class="card">
        <div class="card-content">
          <h1>{{ declinedNum }}</h1>
          <p>Declined</p>
        </div>
      </div>
    </div>
    <!--Today's Transactions-->
    <div>
      <Table :columnDefs="columnDefs" @grid-ready="onGridReady"></Table>
    </div>
  </div>
</template>
