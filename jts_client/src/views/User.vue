<template>
  <div>
    <NewUserForm v-if="isNewUserFormOpen" @close="closeNewUserForm" />
    <UserForm v-if="isUserFormOpen" @close="closeUserForm" />

    <h4 class="text-primary">User</h4>
    <div class="flex justify-between items-end mt-1 mb-2">
      <div class="flex gap-4">
        <div>
          <label>Name</label>
          <input class="input__field !w-56 !p-[0.30rem]" v-model="fullname" />
        </div>
        <div>
          <label>Username</label>
          <input class="input__field !w-56 !p-[0.30rem]" v-model="username" />
        </div>
      </div>

      <div class="flex">
        <button
          @click="searchUser"
          class="button-transparent border !w-fit mr-2 disabled:bg-lightSecondary disabled:border-none"
        >
          Search
        </button>
        <button
          @click="clear"
          class="button-transparent border !w-fit mr-2 disabled:bg-lightSecondary disabled:border-none"
        >
          Clear
        </button>
        <button
          @click="openUserForm"
          :disabled="isSelectedRowEmpty"
          class="button-transparent border !w-fit mr-2 disabled:bg-lightSecondary disabled:border-none"
        >
          Open
        </button>

        <button
          @click="openNewUserForm"
          class="button-transparent border !w-fit"
        >
          New user
        </button>
      </div>
    </div>
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
    </v-data-table-server>
  </div>
</template>

<script>
import NewUserForm from "@/components/NewUserForm.vue";
import UserForm from "@/components/UserForm.vue";
import { onUnmounted, ref, watch } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import { computed } from "@vue/reactivity";
import { useSignalR } from "@quangdao/vue-signalr";

export default {
  name: "User",
  components: {
    NewUserForm,
    UserForm,
  },

  setup() {
    const store = useStore();
    const router = useRouter();
    const signalR = useSignalR();

    const itemsPerPage = ref(10);
    const serverItems = ref([]);
    const loading = ref(true);
    const totalItems = ref(0);
    const recentlyClickedRow = ref([]);
    const fullname = ref("");
    const username = ref("");
    const search = ref("");
    const isUserFormOpen = computed(() => store.state.app.isUserFormOpen);
    const isNewUserFormOpen = computed(() => store.state.app.isNewUserFormOpen);

    const isSelectedRowEmpty = computed(() =>
      store.state.app.selectedUser?.user?.user_id == null ? true : false
    );

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
        title: "No.",
        align: "start",
        sortable: false,
        key: "user.user_id",
      },
      {
        title: "Name",
        key: "user.ext_name",
        align: "start",
        sortable: false,
      },
      {
        title: "Username",
        key: "user.username",
        align: "start",
        sortable: false,
      },

      {
        title: "Department",
        key: "user.department.name",
        align: "start",
        sortable: false,
      },

      {
        title: "Role",
        key: "user.role.name",
        align: "start",
        sortable: false,
      },
    ];

    const loadItems = async ({ page, itemsPerPage, sortBy }) => {
      removeSelect();
      const offset = (page - 1) * itemsPerPage;
      const param = {
        items_per_page: itemsPerPage,
        offset: offset,
        username: username.value,
        full_name: fullname.value,
      };

      loading.value = true;
      await store.dispatch("user/fetchAllUsers", param);
      const users = store.state.user.users;

      serverItems.value = users.users;
      totalItems.value = users.total_items;
      loading.value = false;
      store.commit("app/SET_SELECTED_USER", {});
    };

    const removeSelect = () => {
      if (recentlyClickedRow.value.length) {
        for (var i = 0; i < recentlyClickedRow.value.length; i++) {
          recentlyClickedRow.value[i].classList.remove("selected");
        }
      }
    };

    function rowClick(event, item) {
      const selectedUser = item.item;
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
      store.commit("app/SET_SELECTED_USER", selectedUser);
    }

    const columnDefs = [
      { headerName: "No.", field: "user.user_id", flex: 1 },
      { headerName: "Name", field: "user.ext_name", flex: 2 },
      {
        headerName: "Username",
        field: "user.username",
        flex: 1,
      },
      {
        headerName: "Department",
        field: "user.department.name",
        flex: 1,
      },
      {
        headerName: "Role",
        field: "user.role.name",
        flex: 1,
      },
    ];

    signalR.on("GetUser", (user) => {
      search.value = String(Date.now());
    });

    signalR.on("UpdateUser", (user) => {
      search.value = String(Date.now());
    });

    watch(
      () => isUserFormOpen.value,
      async (newIsUserFormOpen, oldIsUserFormOpen) => {
        if (!newIsUserFormOpen) {
          router.replace({
            name: "User",
          });
        }
      }
    );

    const searchUser = () => {
      search.value = String(Date.now());
    };

    const clear = () => {
      username.value = "";
      fullname.value = "";
      search.value = String(Date.now());
    };

    const openUserForm = () => {
      const userId = store.state.app.selectedUser.user.user_id;
      router.push({
        name: "UserById",
        params: { userId: userId },
      });
    };

    const closeUserForm = () => {
      store.commit("app/SET_USER_FORM", false);
      router.replace({
        name: "User",
      });
    };

    const closeNewUserForm = () => {
      store.commit("app/SET_NEW_USER_FORM", false);
    };

    const openNewUserForm = () => {
      store.commit("app/SET_NEW_USER_FORM", true);
    };

    onUnmounted(() => {
      store.commit("app/SET_SELECTED_USER", {});
    });

    return {
      itemsPerPageOptions,
      itemsPerPage,
      headers,
      search,
      serverItems,
      totalItems,
      loading,
      columnDefs,
      isUserFormOpen,
      isNewUserFormOpen,
      isSelectedRowEmpty,
      fullname,
      username,
      clear,
      searchUser,
      openNewUserForm,
      closeNewUserForm,
      openUserForm,
      closeUserForm,
      loadItems,
      rowClick,
    };
  },
};
</script>
