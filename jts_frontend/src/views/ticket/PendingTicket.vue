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
import { useRouter } from "vue-router";
import { ref } from "vue";
export default {
  components: {
    Table,
    FormattedDate,
  },

  setup() {
    const store = useStore();
    const router = useRouter();
    const gridAPI = ref(null);

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
      // tableApi.value = params.api;
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("ticket/fetchAllPendingTickets");
      params.api.setRowData(store.state.ticket.pendingTickets);
    };

    const onSelectionChanged = () => {
      const selectedRow = gridAPI.value.getSelectedRows();
      store.commit("app/SET_SELECTED_ROW", selectedRow[0]);

      console.log(store.state.app.selectedRow.ticket);
    };

    return {
      columnDefs,
      onGridReady,
      onSelectionChanged,
    };
  },
};
</script>
