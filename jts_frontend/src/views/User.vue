<template>
  <div>
    <UserForm v-if="isUserFormOpen" @close="closeModal()" />
    <h4 class="text-primary">User</h4>
    <div class="flex justify-between items-end mt-1 mb-2">
      <div>
        <label>Search</label>
        <input class="input__field !w-56 !p-[0.30rem]" />
      </div>
      <div class="flex">
        <button @click="getAPI()" class="button-transparent !w-fit mr-2">
          Open
        </button>

        <button @click="openModal()" class="button-transparent !w-fit">
          New user
        </button>
      </div>
    </div>
    <Table @grid-ready="onGridReady" :columnDefs="columnDefs"></Table>
  </div>
</template>

<script lang="js">
import UserForm from "@/components/UserForm.vue";
import Table from "@/components/Table.vue";
import { ref } from "vue";
import { useStore } from "vuex";
import { computed } from "@vue/reactivity";
import { useSignalR } from '@quangdao/vue-signalr';
export default {
  name: "User",
  components: {
    Table,
    UserForm,
  },

    setup() {
    const store = useStore();
    const signalR = useSignalR();
    const columnDefs = [
      { headerName: "No.", field: "user_id", flex: 1 },
      { headerName: "Name", field: "ext_name", flex: 2 },
      {
        headerName: "Username",
        field: "username",
        flex: 1,
      },
      {
        headerName: "Department",
        field: "department.name",
        flex: 1,
      },
      {
        headerName: "Role",
        field: "role.name",
        flex: 1,
      },
    ];

    const tableApi = ref(null);

    const isUserFormOpen = computed(()=> store.state.app.isUserFormOpen);

    signalR.on('GetUser', user => {
      store.commit("user/ADD_USER", user);
      tableApi.value.setRowData(store.state.user.users)
    });

    const closeModal = () => {
      store.commit("app/SET_USER_FORM", false);
    };

    const openModal = () => {
      store.commit("app/SET_USER_FORM", true);
    };

    const onGridReady = async (params) => {
      tableApi.value = params.api;
      params.api.showLoadingOverlay();
      await store.dispatch("user/fetchUsers");
      params.api.setRowData(store.state.user.users);
      console.log('user', store.state.user.users)
    };

    return {
      columnDefs,
      isUserFormOpen,
      closeModal,
      openModal,
      onGridReady,
    };
  },
};
</script>
