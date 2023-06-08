<template>
  <Table @grid-ready="onGridReady" :columnDefs="columnDefs" />
</template>
<script>
import Table from "@/components/Table.vue";
import { useStore } from "vuex";
export default {
  components: {
    Table,
  },

  setup() {
    const store = useStore();
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
      },
    ];

    const onGridReady = async (params) => {
      // tableApi.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("ticket/fetchAllDeclinedTickets");
      params.api.setRowData(store.state.ticket.declinedTickets);
    };
    return {
      columnDefs,
      onGridReady,
    };
  },
};
</script>
