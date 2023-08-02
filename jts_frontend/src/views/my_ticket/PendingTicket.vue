<template>
  <Table
    @grid-ready="onGridReady"
    :columnDefs="columnDefs"
    @selection-changed="onSelectionChanged"
  />
</template>
<script>
import Table from "@/components/Table.vue";
import FormattedDate from "@/components/FormattedDate.vue";
import { useStore } from "vuex";
import { ref } from "vue";
import { useSignalR } from "@quangdao/vue-signalr";
export default {
  components: {
    Table,
    FormattedDate,
  },

  setup() {
    const store = useStore();
    const signalR = useSignalR();
    const gridAPI = ref(null);
    const PENDING_STATUS_ID = 1;
    const currentUser = JSON.parse(localStorage.getItem("user"));
    const columnDefs = [
      { headerName: "Ticket ID", field: "ticket.ticket_id", flex: 1 },
      { headerName: "Subject", field: "ticket.subject", flex: 2 },
      {
        headerName: "Prepared By",
        field: "ticket.user.ext_name",
        flex: 1,
      },
      {
        headerName: "Date Created",
        field: "ticket.date_created",
        flex: 1,
        cellRenderer: FormattedDate,
      },
    ];

    const onGridReady = async (params) => {
      const param = {
        user_id: currentUser.user_id,
        status_id: PENDING_STATUS_ID,
      };
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("ticket/fetchMyPendingTickets", param);
      const myPendingTickets = store.state.ticket.myPendingTickets;
      params.api.setRowData(myPendingTickets);
    };

    signalR.on("GetMyTicket", (ticket) => {
      store.commit("ticket/ADD_MY_PENDING_TICKETS", ticket);
      const myPendingTickets = store.state.ticket.myPendingTickets;
      gridAPI.value.setRowData(myPendingTickets);
    });

    const onSelectionChanged = () => {
      const selectedRow = gridAPI.value.getSelectedRows();
      store.commit("app/SET_SELECTED_TICKET", selectedRow[0]);
    };

    return {
      columnDefs,
      onGridReady,
      onSelectionChanged,
    };
  },
};
</script>
