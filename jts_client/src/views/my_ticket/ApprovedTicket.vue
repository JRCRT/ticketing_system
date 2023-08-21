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
    const APPROVED_STATUS_ID = 2;
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
        headerName: "Date Created",
        field: "ticket.date_created",
        flex: 1,
        cellRenderer: FormattedDate,
      },
    ];

    const onGridReady = async (params) => {
      const param = {
        user_id: currentUser.user_id,
        status_id: APPROVED_STATUS_ID,
      };
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("ticket/fetchMyApprovedTickets", param);
      const myApprovedTickets = store.state.ticket.myApprovedTickets;

      params.api.setRowData(myApprovedTickets);
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
