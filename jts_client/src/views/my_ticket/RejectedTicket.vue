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
    <template v-slot:item.ticket.action_date="{ item }">
      {{ formatDate(item.columns["ticket.action_date"]) }}
    </template>
  </v-data-table-server>
</template>

<script>
import { ref } from "vue";
import { useStore } from "vuex";
import { formatDate } from "@/util/helper";

export default {
  setup() {
    const store = useStore();
    const REJECTED_STATUS_ID = 3;
    const currentUser = JSON.parse(localStorage.getItem("user"));
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
    const itemsPerPage = ref(10);
    const headers = [
      {
        title: "Ticket ID",
        align: "start",
        sortable: false,
        key: "ticket.ticket_id",
      },
      { title: "Subject", key: "ticket.subject", align: "start" },
      {
        title: "Prepared By",
        key: "ticket.created_by.user.ext_name",
        align: "start",
      },

      {
        title: "Rejected Date",
        key: "ticket.action_date",
        align: "start",
      },
    ];
    const search = "";
    const serverItems = ref([]);
    const loading = ref(true);
    const totalItems = ref(0);
    const recentlyClickedRow = ref([]);

    const loadItems = async ({ page, itemsPerPage, sortBy }) => {
      removeSelect();
      const offset = (page - 1) * itemsPerPage;
      const param = {
        user_id: currentUser.user_id,
        status_id: REJECTED_STATUS_ID,
        items_per_page: itemsPerPage,
        offset: offset,
      };

      loading.value = true;
      await store.dispatch("ticket/fetchMyRejectedTickets", param);
      const myRejectedTickets = store.state.ticket.myRejectedTickets;
      serverItems.value = myRejectedTickets;
      totalItems.value =
        myRejectedTickets.length !== 0 ? myRejectedTickets[0].total_items : 0;
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
      const tr = event.target.parentNode;
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
