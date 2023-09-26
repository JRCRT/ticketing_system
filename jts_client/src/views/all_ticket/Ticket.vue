<template>
  <div class="flex flex-col gap-3">
    <h4 class="text-primary">All Tickets</h4>
    <div class="relative">
      <div class="flex justify-between">
        <div class="flex">
          <div
            @click="changeTab(tab)"
            :class="
              router.currentRoute.value.params.status == tab.status
                ? 'active__tab'
                : 'tab'
            "
            v-for="tab in tabs"
            :key="tab"
          >
            {{ tab.label }}
          </div>
        </div>
        <div class="flex gap-2 items-end justify-end relative w-[680px] mb-1">
          <div class="absolute left-0 w-[180px]">
            <label>Ticket Id</label>
            <input class="input__field h-8" v-model="ticketIdSearchField" />
          </div>
          <div class="absolute left-[189px] w-[165px]">
            <label>Date Created</label>
            <input
              class="input__field h-8"
              type="date"
              v-model="dateCreatedSearchField"
            />
          </div>
          <div class="absolute w-[165px] left-[365px]">
            <label>Prepared By</label>
            <VueMultiselect
              v-model="preparedBy"
              :options="allUsers"
              label="ext_name"
              :show-labels="false"
            />
          </div>
          <div class="flex flex-col absolute left-[540px]">
            <button
              class="w-16 border button-transparent disabled:bg-lightSecondary disabled:border-none"
              @click="clear"
            >
              Clear
            </button>
            <button
              class="w-16 border button-transparent disabled:bg-lightSecondary disabled:border-none"
              @click="search"
            >
              Search
            </button>
          </div>

          <button
            class="w-14 border button-transparent mr-2 disabled:bg-lightSecondary disabled:border-none"
            :disabled="isSelectedRowEmpty"
            @click="openTicket"
          >
            Open
          </button>
        </div>
      </div>
      <div class="border-b w-full absolute bottom-0"></div>
    </div>
    <!--  <component :is="currentTab" :ticketStatus="currentStatus" /> -->
    <TicketTab :ticketStatus="currentStatus" />
  </div>
</template>

<script>
import TicketTab from "@/views/all_ticket/TicketTab.vue";
import TicketForm from "@/components/TicketForm.vue";
import VueMultiselect from "vue-multiselect";
import { useRouter, useRoute } from "vue-router";
import { useStore } from "vuex";
import { TICKET_STATUS } from "@/util/constant";
import { computed, ref, onUnmounted, watch, onMounted } from "vue";
import { useSignalR } from "@quangdao/vue-signalr";

export default {
  components: {
    TicketForm,
    TicketTab,
    VueMultiselect,
  },

  setup() {
    const signalR = useSignalR();
    const router = useRouter();
    const store = useStore();
    const route = useRoute();
    const allUsers = ref([]);
    const currentStatus = ref(route.params.status);
    const ticketIdSearchField = ref("");
    const dateCreatedSearchField = ref("");
    const preparedBy = ref({});
    const isTicketFormOpen = computed(() => store.state.app.isTicketFormOpen);
    const isSelectedRowEmpty = computed(() =>
      store.state.app.selectedTicket.ticket == null ? true : false
    );

    const tabs = [
      {
        label: "Pending",
        status: TICKET_STATUS.PENDING,
      },
      {
        label: "Approved",
        status: TICKET_STATUS.APPROVED,
      },
      {
        label: "Rejected",
        status: TICKET_STATUS.REJECTED,
      },
      {
        label: "Done",
        status: TICKET_STATUS.DONE,
      },
    ];

    watch(
      () => isTicketFormOpen.value,
      async (newIsTicketFormOpen, oldIsTicketFormOpen) => {
        if (!newIsTicketFormOpen) {
          router.replace({
            name: "Ticket",
            params: { status: currentStatus.value },
          });
        }
      }
    );

    function changeTab(tab) {
      currentStatus.value = tab.status;
      router.replace({ name: "Ticket", params: { status: tab.status } });
      store.commit("app/SET_SELECTED_TICKET", {});
      clear();
    }

    const openTicket = async () => {
      const ticketId = store.state.app.selectedTicket.ticket.ticket_id;
      router.push({
        name: "TicketById",
        params: { status: currentStatus.value, ticketId: ticketId },
      });
    };

    signalR.on("GetAllTicket", (ticket) => {
      store.commit("app/SET_SEARCH", String(Date.now()));
    });

    const search = () => {
      store.commit("app/SET_SEARCH", String(Date.now()));
      store.commit(
        "app/SET_SEARCH_TICKET_ID",
        ticketIdSearchField.value === "" ? 0 : Number(ticketIdSearchField.value)
      );
      store.commit(
        "app/SET_SEARCH_CREATED_DATE",
        dateCreatedSearchField.value === ""
          ? "1/1/1, 12:00:00"
          : `${dateCreatedSearchField.value}, 12:00:00`
      );
      store.commit(
        "app/SET_SEARCH_PREPARED_BY",
        preparedBy.value?.user_id ?? 0
      );
    };

    const clear = () => {
      store.commit("app/SET_SELECTED_TICKET", {});
      store.commit("app/SET_SEARCH_TICKET_ID", 0);
      store.commit("app/SET_SEARCH_CREATED_DATE", "1/1/1, 12:00:00");
      store.commit("app/SET_SEARCH_PREPARED_BY", 0);
      store.commit("app/SET_SEARCH", String(Date.now()));
      ticketIdSearchField.value = "";
      dateCreatedSearchField.value = "";
      preparedBy.value = {};
    };

    onMounted(async () => {
      const param = {
        items_per_page: 0,
        offset: 0,
        username: "",
        full_name: "",
      };
      await store.dispatch("user/fetchAllUsers", param);
      allUsers.value = [...store.state.user.users.users].map((u) => u.user);
    });

    onUnmounted(() => {
      store.commit("app/SET_SELECTED_TICKET", {});
    });

    return {
      tabs,
      router,
      currentStatus,
      isSelectedRowEmpty,
      isTicketFormOpen,
      ticketIdSearchField,
      dateCreatedSearchField,
      allUsers,
      preparedBy,
      clear,
      search,
      changeTab,
      openTicket,
    };
  },
};
</script>

<style scoped>
:deep(.multiselect) {
  min-height: 34px !important;
  height: 34px !important;
}

:deep(.multiselect__tags) {
  min-height: 34px !important;
  height: 34px !important;
}
:deep(.multiselect__placeholder) {
  padding-top: 0px !important;
}

:deep(.multiselect__single) {
  font-size: 14px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
</style>
