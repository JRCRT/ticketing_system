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
import { useSignalR } from "@quangdao/vue-signalr";
export default {
  components: {
    Table,
    FormattedDate,
  },

  setup() {
    const store = useStore();
    const router = useRouter();
    const signalR = useSignalR();
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
      // tableApi.value = params.api;
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      await fetchTicketsForApproval();
    };

    signalR.on('GetTicketForApproval', async () => {
      await fetchTicketsForApproval();
    });

    const fetchTicketsForApproval = async () => {
      await store.dispatch("ticket/fetchTicketsForApproval", currentUser.user_id);
      console.log(store.state.ticket.ticketsForApproval);
      const pendingTicketsForApproval = store.getters["ticket/pendingTicketsForApproval"]
      gridAPI.value.setRowData(pendingTicketsForApproval)
    }

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
