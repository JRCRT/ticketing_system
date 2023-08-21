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
export default {
  components: {
    Table,
    FormattedDate,
  },

  setup() {
    const store = useStore();
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
        cellRenderer: FormattedDate,
        flex: 1,
      },
      {
        headerName: "Date Declined",
        field: "ticket.date_declined",
        flex: 1,
        cellRenderer: FormattedDate,
      },
    ];

    const onGridReady = async (params) => {
      // tableApi.value = params.api;
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("ticket/fetchAllDeclinedTickets");
      const declinedTickets = store.state.ticket.allDeclinedTickets;
      params.api.setRowData(declinedTickets);
    };

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
