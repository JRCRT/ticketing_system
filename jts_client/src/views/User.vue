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
          @click="openUserForm"
          :disabled="isSelectedRowEmpty"
          class="button-transparent !w-fit mr-2 disabled:bg-lightSecondary disabled:border-none"
        >
          Open
        </button>

        <button @click="openNewUserForm" class="button-transparent !w-fit">
          New user
        </button>
      </div>
    </div>
    <Table
      @grid-ready="onGridReady"
      @selection-changed="onSelectionChanged"
      :columnDefs="columnDefs"
    ></Table>
  </div>
</template>

<script>
import NewUserForm from "@/components/NewUserForm.vue";
import UserForm from "@/components/UserForm.vue";
import Table from "@/components/Table.vue";
import { onUnmounted, ref, watch } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import { computed } from "@vue/reactivity";
import { useSignalR } from "@quangdao/vue-signalr";
export default {
  name: "User",
  components: {
    Table,
    NewUserForm,
    UserForm,
  },

  setup() {
    const store = useStore();
    const router = useRouter();
    const signalR = useSignalR();
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

    const gridAPI = ref(null);
    const fullname = ref(null);
    const username = ref(null);
    const isUserFormOpen = computed(() => store.state.app.isUserFormOpen);
    const isNewUserFormOpen = computed(() => store.state.app.isNewUserFormOpen);

    const isSelectedRowEmpty = computed(() =>
      store.state.app.selectedUser.user_id == null ? true : false
    );

    signalR.on("GetUser", (user) => {
      store.commit("user/ADD_USER", user);
      console.log(user);
      gridAPI.value.setRowData(store.state.user.users);
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

    const openUserForm = () => {
      const userId = store.state.app.selectedUser.user_id;
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

    const onGridReady = async (params) => {
      gridAPI.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("user/fetchAllUsers");
      params.api.setRowData(store.state.user.users);
    };

    const onSelectionChanged = () => {
      const selectedRow = gridAPI.value.getSelectedRows();
      store.commit("app/SET_SELECTED_USER", selectedRow[0].user);
    };

    onUnmounted(() => {
      store.commit("app/SET_SELECTED_USER", {});
    });

    return {
      columnDefs,
      isUserFormOpen,
      isNewUserFormOpen,
      isSelectedRowEmpty,
      fullname,
      username,
      openNewUserForm,
      closeNewUserForm,
      openUserForm,
      closeUserForm,
      onGridReady,
      onSelectionChanged,
    };
  },
};
</script>