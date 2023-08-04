<template>
  <div class="flex flex-col gap-3">
    <TicketForm v-if="isTicketFormOpen" @close="closeTicketForm" />
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
      <div class="flex items-center justify-between mb-2">
        <h4 class="text-primary">Today's Ticket</h4>
        <button
          class="w-14 button-transparent mr-2 disabled:bg-lightSecondary disabled:border-none"
          :disabled="isSelectedRowEmpty"
          @click="openTicket"
        >
          Open
        </button>
      </div>

      <Table
        :columnDefs="columnDefs"
        @grid-ready="onGridReady"
        @selection-changed="onSelectionChanged"
      ></Table>
    </div>
  </div>
</template>

<script>
import Table from "@/components/Table.vue";
import TicketStatus from "@/components/TicketStatus.vue";
import FormattedDate from "@/components/FormattedDate.vue";
import TicketForm from "@/components/TicketForm.vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { onMounted, ref, computed, watch, onUnmounted } from "vue";
import { TICKET_STATUS } from "@/util/constant";
export default {
  name: "Dashboard",

  components: {
    Table,
    TicketStatus,
    FormattedDate,
    TicketForm,
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    const gridAPI = ref(null);
    const pendingNum = ref(0);
    const approvedNum = ref(0);
    const declinedNum = ref(0);
    const isSelectedRowEmpty = computed(() =>
      store.state.app.selectedTicket.ticket == null ? true : false
    );
    const isTicketFormOpen = computed(() => store.state.app.isTicketFormOpen);

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

    const openTicket = () => {
      const ticketId = store.state.app.selectedTicket.ticket.ticket_id;
      router.push({
        name: "DashboardTicketById",
        params: { ticketId: ticketId },
      });
    };

    const onSelectionChanged = () => {
      const selectedRow = gridAPI.value.getSelectedRows();
      store.commit("app/SET_SELECTED_TICKET", selectedRow[0]);
    };

    const closeTicketForm = () => {
      store.commit("app/SET_TICKET_FORM", false);
    };

    onUnmounted(() => {
      store.commit("app/SET_SELECTED_TICKET", {});
    });

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

    watch(
      () => isTicketFormOpen.value,
      async (newIsTicketFormOpen, oldIsTicketFormOpen) => {
        if (!newIsTicketFormOpen) {
          router.replace({
            name: "Dashboard",
          });
        }
      }
    );

    return {
      columnDefs,
      pendingNum,
      approvedNum,
      declinedNum,
      navigateToTicket,
      onGridReady,
      getSelectedRow,
      openTicket,
      onSelectionChanged,
      closeTicketForm,
      TICKET_STATUS,
      isSelectedRowEmpty,
      isTicketFormOpen,
    };
  },
};
</script>
