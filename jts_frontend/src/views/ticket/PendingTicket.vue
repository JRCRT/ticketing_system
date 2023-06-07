<template>
  <Table @grid-ready="onGridReady" :columnDefs="columnDefs" />
</template>
<script>
import Table from "@/components/Table.vue";
export default {
  components: {
    Table,
  },

  setup() {
    const columnDefs = [
      { headerName: "Ticket ID", field: "ticket_id", flex: 1 },
      { headerName: "Subject", field: "subject", flex: 2 },
      {
        headerName: "Prepared By",
        field: "user.ext_name",
        flex: 1,
      },
      {
        headerName: "Date Prepared",
        field: "datePrepared",
        flex: 1,
      },
    ];

    const onGridReady = async (params) => {
      tableApi.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("ticket/fetchAllPendingTickets");
      params.api.setRowData(store.state.ticket.pendingTickets);
    };

    return {
      columnDefs,

      onGridReady,
    };
  },
};
</script>
