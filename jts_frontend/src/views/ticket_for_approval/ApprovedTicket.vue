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
import { TICKET_STATUS } from "@/util/constant";

export default {
  components: {
    Table,
    FormattedDate,
  },

  setup() {
    const store = useStore();
    const gridAPI = ref(null);
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
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("ticket/fetchTicketsForApproval", {userId: currentUser.user_id, status: TICKET_STATUS.APPROVED});
      const approvedTicketsForApproval = store.getters["ticket/approvedTicketsForApproval"]
      params.api.setRowData(approvedTicketsForApproval);
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
