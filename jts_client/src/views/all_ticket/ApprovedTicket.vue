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
    const columnDefs = [
      { headerName: "Ticket ID", field: "ticket.ticket_id", flex: 1 },
      { headerName: "Subject", field: "ticket.subject", flex: 2 },
      {
        headerName: "Prepared By",
        field: "ticket.created_by.user.ext_name",
        flex: 1,
      },
      {
        headerName: "Date Created",
        field: "ticket.date_created",
        flex: 1,
        cellRenderer: FormattedDate,
      },
      {
        headerName: "Date Approved",
        field: "ticket.action_date",
        flex: 1,
        cellRenderer: FormattedDate,
      },
    ];

    signalR.on("GetAllTicket", (ticket) => {
      store.commit("ticket/REMOVE_DONE_TICKET", ticket);
      const allApprovedTickets = store.state.ticket.allApprovedTickets;
      gridAPI.value.setRowData(allApprovedTickets);
    });

    const onGridReady = async (params) => {
      // tableApi.value = params.api;
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("ticket/fetchAllApprovedTickets");
      const approvedTickets = store.state.ticket.allApprovedTickets;
      params.api.setRowData(approvedTickets);
    };

    const onSelectionChanged = async () => {
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
