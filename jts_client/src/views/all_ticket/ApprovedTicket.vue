<template>
  <v-data-table-server
    v-model:items-per-page="itemsPerPage"
    :headers="headers"
    :items-length="totalItems"
    :items="serverItems"
    :loading="loading"
    :search="search"
    :hover="true"
    :fixed-header="true"
    :items-per-page-options="itemsPerPageOptions"
    height="500"
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
        {{ item.columns["ticket.subject"] }}
      </div>
    </template>
    <template v-slot:item.ticket.date_created="{ item }">
      {{ formatDate(item.columns["ticket.date_created"]) }}
    </template>
  </v-data-table-server>
</template>
<script>
import Table from "@/components/Table.vue";
import FormattedDate from "@/components/FormattedDate.vue";
import { useStore } from "vuex";
import { ref, computed } from "vue";
import { useSignalR } from "@quangdao/vue-signalr";
import { formatDate, formatDateTime } from "@/util/helper";

export default {
  components: {
    Table,
    FormattedDate,
  },

  setup() {
    const store = useStore();
    const signalR = useSignalR();
    const gridAPI = ref(null);
    const currentUser = JSON.parse(localStorage.getItem("user"));
    const APPROVED_STATUS_ID = 2;
    const search = computed(() => store.state.app.search);
    const searchTicketId = computed(() => store.state.app.searchTicketId);
    const searchCreatedDate = computed(() => store.state.app.searchCreatedDate);
    const searchPreparedBy = computed(() => store.state.app.searchPreparedBy);
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
    ];

    signalR.on("GetTicketForApproval", (ticket) => {
      store.commit("ticket/REMOVE_PENDING_TICKET_FOR_APPROVAL", ticket);
      const pendingTicketsForApproval =
        store.state.ticket.pendingTicketsForApproval;
      gridAPI.value.setRowData(pendingTicketsForApproval);
    });

    const loadItems = async ({ page, itemsPerPage, sortBy }) => {
      if (
        !Number.isInteger(searchTicketId.value) &&
        searchTicketId.value !== 0
      ) {
        var alert = {
          type: "danger",
          message: "Ticket ID should be whole number.",
        };
        store.dispatch("app/addAlert", alert);
        return;
      }

      removeSelect();
      const offset = (page - 1) * itemsPerPage;
      const param = {
        user_id: currentUser.user_id,
        status_id: APPROVED_STATUS_ID,
        items_per_page: itemsPerPage,
        offset: offset,
        ticket_id: searchTicketId.value,
        date_created: new Date(searchCreatedDate.value),
        prepared_by: searchPreparedBy.value,
      };

      loading.value = true;
      await store.dispatch("ticket/fetchAllApprovedTickets", param);
      const approvedTickets = store.state.ticket.allApprovedTickets;
      serverItems.value = approvedTickets.tickets;
      totalItems.value = approvedTickets.total_items;
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
      const selectedTicket = item.item.raw;
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

    return {
      itemsPerPageOptions,
      itemsPerPage,
      headers,
      search,
      serverItems,
      totalItems,
      loading,
      loadItems,
      rowClick,
      formatDate,
    };
  },
};
</script>
