<template>
  <div>
    <UserForm v-if="modalActive" @close="closeModal()" />
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
    <Table
      @grid-ready="onGridReady"
      :rowData="rowData"
      :columnDefs="columnDefs"
    ></Table>
  </div>
</template>

<script>
import UserForm from "@/components/UserForm.vue";
import Table from "@/components/Table.vue";
import { onMounted, ref } from "vue";
import store from "../store";
import { computed } from "@vue/reactivity";
export default {
  name: "User",
  components: {
    Table,
    UserForm,
  },

  setup() {
    /*  const axios = inject("axios"); */
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
    ];
    const gridAPI = ref(null);
    const rowData = ref([]);

    onMounted(async () => {
      await store.dispatch("user/loadUsers");
      gridAPI.value.setRowData(store.state.user.users);
    });

    const modalActive = ref(false);

    const closeModal = () => {
      modalActive.value = false;
    };

    const onGridReady = (params) => {
      gridAPI.value = params.api;
    };

    async function getAPI() {}
    const openModal = () => {
      modalActive.value = true;
    };

    return {
      columnDefs,
      rowData,
      modalActive,
      closeModal,
      openModal,
      getAPI,
      onGridReady,
    };
  },
};
</script>
