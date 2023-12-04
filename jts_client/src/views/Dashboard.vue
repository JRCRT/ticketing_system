<template>
  <div class="flex flex-col gap-3">
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
      <div @click="navigateToTicket(TICKET_STATUS.REJECTED)" class="card">
        <div class="card-content">
          <h1>{{ rejectedNum }}</h1>
          <p>Rejected</p>
        </div>
      </div>
      <div @click="navigateToTicket(TICKET_STATUS.DONE)" class="card">
        <div class="card-content">
          <h1>{{ doneNum }}</h1>
          <p>Done</p>
        </div>
      </div>
    </div>
    <!--Today's Transactions-->
    <div>
      <div class="flex items-center justify-between mb-2">
        <h4 class="text-primary">Today's Ticket</h4>
        <button
          class="w-14 button-transparent mr-2 border disabled:bg-lightSecondary disabled:border-none"
          :disabled="isSelectedRowEmpty"
          @click="openTicket"
        >
          Open
        </button>
      </div>

      <v-data-table-server
        v-model:items-per-page="itemsPerPage"
        :headers="headers"
        :items-length="totalItems"
        :items="serverItems"
        :loading="loading"
        :hover="true"
        :fixed-header="true"
        :items-per-page-options="itemsPerPageOptions"
        height="450"
        density="comfortable"
        @click:row="rowClick"
        class="elevation-1"
        item-value="name"
        @update:options="loadItems"
      >
        <template v-slot:item.ticket.subject="{ item }">
          <div
            class="max-w-[500px] whitespace-nowrap overflow-hidden text-ellipsis"
          >
            {{ item.ticket.subject }}
          </div>
        </template>
        <template v-slot:item.ticket.date_created="{ item }">
          {{ formatDate(item.ticket.date_created) }}
        </template>
        <template v-slot:item.ticket.status.name="{ item }">
          <div
            class="w-fit p-[3px] text-[14px] text-white rounded-lg"
            :class="getColor(item.ticket.status.name)"
          >
            {{ item.ticket.status.name }}
          </div>
        </template>
      </v-data-table-server>
    </div>
  </div>
</template>

<script>
import TicketStatus from "@/components/TicketStatus.vue";
import FormattedDate from "@/components/FormattedDate.vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { onMounted, ref, computed, watch, onUnmounted } from "vue";
import { TICKET_STATUS, ROLE } from "@/util/constant";
import { formatDate, formatDateTime } from "@/util/helper";

export default {
  name: "Dashboard",

  components: {
    TicketStatus,
    FormattedDate,
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    const gridAPI = ref(null);
    const pendingNum = ref(0);
    const approvedNum = ref(0);
    const rejectedNum = ref(0);
    const doneNum = ref(0);

    const isSelectedRowEmpty = computed(() =>
      store.state.app.selectedTicket.ticket == null ? true : false
    );
    const isTicketFormOpen = computed(() => store.state.app.isTicketFormOpen);
    const currentUser = JSON.parse(localStorage.getItem("user"));
    const itemsPerPage = ref(10);
    const serverItems = ref([]);
    const loading = ref(true);
    const totalItems = ref(0);
    const recentlyClickedRow = ref([]);
    const itemsPerPageOptions = [
      {
        title: "10",
        value: 10,
      },
      {
        title: "15",
        value: 15,
      },
      {
        title: "20",
        value: 20,
      },
      {
        title: "50",
        value: 50,
      },
      {
        title: "100",
        value: 100,
      },
    ];
    const headers = [
      {
        title: "Ticket ID",
        align: "start",
        sortable: false,
        key: "ticket.ticket_id",
      },
      {
        title: "Subject",
        key: "ticket.subject",
        align: "start",
        sortable: false,
      },
      {
        title: "Prepared By",
        key: "ticket.created_by.user.ext_name",
        align: "start",
        sortable: false,
      },

      {
        title: "Date Created",
        key: "ticket.date_created",
        align: "start",
        sortable: false,
      },
      {
        title: "Status",
        key: "ticket.status.name",
        align: "start",
        sortable: false,
      },
    ];

    const loadItems = async ({ page, itemsPerPage, sortBy }) => {
      removeSelect();
      const offset = (page - 1) * itemsPerPage;
      const param = {
        user_id: currentUser.user_id,
        items_per_page: itemsPerPage,
        offset: offset,
        role_id: currentUser.role.role_id,
      };

      loading.value = true;
      await store.dispatch("ticket/fetchAllTodaysTickets", param);
      const todaysTickets = store.state.ticket.todaysTickets;
      serverItems.value = todaysTickets.tickets;
      totalItems.value = todaysTickets.total_items;
      loading.value = false;
      store.commit("app/SET_SELECTED_TICKET", {});
    };

    const removeSelect = () => {
      if (recentlyClickedRow.value.length) {
        for (var i = 0; i < recentlyClickedRow.value.length; i++) {
          recentlyClickedRow.value[i].classList.remove("selected");
        }
      }
    };

    function rowClick(event, item) {
      const selectedTicket = item.item;
      removeSelect();
      const tr =
        event.target.tagName === "DIV"
          ? event.target.parentNode.parentNode
          : event.target.parentNode;

      var tds = tr.getElementsByTagName("td");

      for (var i = 0; i < tds.length; i++) {
        tds[i].classList.add("selected");
      }
      recentlyClickedRow.value = tds;
      store.commit("app/SET_SELECTED_TICKET", selectedTicket);
    }

    const navigateToTicket = (status) => {
      switch (currentUser.role.name) {
        case ROLE.USER:
          router.replace({ name: "MyTicket", params: { status: status } });
          break;
        case ROLE.ADMIN:
          router.replace({ name: "Ticket", params: { status: status } });
          break;
        default:
          router.replace({
            name: "TicketForApproval",
            params: { status: status },
          });
          break;
      }
    };

    const openTicket = () => {
      const ticketId = store.state.app.selectedTicket.ticket.ticket_id;
      router.push({
        name: "DashboardTicketById",
        params: { ticketId: ticketId },
      });
    };

    onUnmounted(() => {
      store.commit("app/SET_SELECTED_TICKET", {});
    });

    onMounted(async () => {
      const paramPending = {
        user_id: currentUser.user_id,
        status_id: 1,
        items_per_page: 0,
        offset: 0,
      };
      const paramApproved = {
        user_id: currentUser.user_id,
        status_id: 2,
        items_per_page: 0,
        offset: 0,
      };
      const paramRejected = {
        user_id: currentUser.user_id,
        status_id: 3,
        items_per_page: 0,
        offset: 0,
      };
      const paramDone = {
        user_id: currentUser.user_id,
        status_id: 4,
        items_per_page: 0,
        offset: 0,
      };

      const paramAllPending = {
        status_id: 1,
        items_per_page: 0,
        offset: 0,
      };
      const paramAllApproved = {
        status_id: 2,
        items_per_page: 0,
        offset: 0,
      };
      const paramAllRejected = {
        status_id: 3,
        items_per_page: 0,
        offset: 0,
      };
      const paramAllDone = {
        status_id: 4,
        items_per_page: 0,
        offset: 0,
      };

      store.commit("app/SET_LOADING", true);
      switch (currentUser.role.name) {
        case ROLE.USER:
          await store.dispatch("ticket/fetchMyPendingTickets", paramPending);
          await store.dispatch("ticket/fetchMyApprovedTickets", paramApproved);
          await store.dispatch("ticket/fetchMyRejectedTickets", paramRejected);
          await store.dispatch("ticket/fetchMyDoneTickets", paramDone);
          pendingNum.value = store.state.ticket.myPendingTickets.total_items;
          approvedNum.value = store.state.ticket.myApprovedTickets.total_items;
          rejectedNum.value = store.state.ticket.myRejectedTickets.total_items;
          doneNum.value = store.state.ticket.myDoneTickets.total_items;
          break;
        case ROLE.ADMIN:
          await store.dispatch(
            "ticket/fetchAllPendingTickets",
            paramAllPending
          );
          await store.dispatch(
            "ticket/fetchAllApprovedTickets",
            paramAllApproved
          );
          await store.dispatch(
            "ticket/fetchAllRejectedTickets",
            paramAllRejected
          );
          await store.dispatch("ticket/fetchAllDoneTickets", paramAllDone);
          pendingNum.value = store.state.ticket.allPendingTickets.total_items;
          approvedNum.value = store.state.ticket.allApprovedTickets.total_items;
          rejectedNum.value = store.state.ticket.allRejectedTickets.total_items;
          doneNum.value = store.state.ticket.allDoneTickets.total_items;
          break;
        default:
          await store.dispatch(
            "ticket/fetchPendingTicketsForApproval",
            paramPending
          );
          await store.dispatch(
            "ticket/fetchApprovedTicketsForApproval",
            paramApproved
          );
          await store.dispatch(
            "ticket/fetchRejectedTicketsForApproval",
            paramRejected
          );
          await store.dispatch("ticket/fetchDoneTicketsForApproval", paramDone);
          pendingNum.value =
            store.state.ticket.pendingTicketsForApproval.total_items;
          approvedNum.value =
            store.state.ticket.approvedTicketsForApproval.total_items;
          rejectedNum.value =
            store.state.ticket.rejectedTicketsForApproval.total_items;
          doneNum.value = store.state.ticket.doneTicketsForApproval.total_items;
          break;
      }
      store.commit("app/SET_LOADING", false);
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

    const getColor = (status) => {
      switch (status) {
        case TICKET_STATUS.PENDING:
          return "bg-orange";
        case TICKET_STATUS.APPROVED:
          return "bg-blue";
        case TICKET_STATUS.REJECTED:
          return "bg-red";
        case TICKET_STATUS.DONE:
          return "bg-primary";
      }
    };

    return {
      pendingNum,
      approvedNum,
      rejectedNum,
      doneNum,
      TICKET_STATUS,
      isSelectedRowEmpty,
      itemsPerPageOptions,
      itemsPerPage,
      headers,
      serverItems,
      totalItems,
      loading,
      formatDate,
      getColor,
      navigateToTicket,
      openTicket,
      loadItems,
      rowClick,
    };
  },
};
</script>
