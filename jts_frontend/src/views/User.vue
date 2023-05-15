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
    <Table :rowData="rowData" :columnDefs="columnDefs"></Table>
  </div>
</template>
<script>
import UserForm from "@/components/UserForm.vue";
import Table from "@/components/Table.vue";
import axios from "@/services/api";
import { ref, inject } from "vue";
export default {
  name: "User",
  components: {
    Table,
    UserForm,
  },

  setup() {
    /*  const axios = inject("axios"); */
    const columnDefs = [
      { headerName: "No.", field: "no", flex: 1 },
      { headerName: "Name", field: "name", flex: 2 },
      {
        headerName: "Code",
        field: "code",
        flex: 1,
      },
      {
        headerName: "Department",
        field: "department",
        flex: 1,
      },
    ];
    const rowData = [
      {
        no: 1,
        name: "Juan Dela Cruz",
        code: "226JOD",
        department: "IT",
      },
    ];

    const modalActive = ref(false);

    function closeModal() {
      modalActive.value = false;
    }

    async function getAPI() {
      await axios.get("/User").then((response) => {
        console.log(response.data);
      });
    }
    function openModal() {
      modalActive.value = true;
    }

    return {
      columnDefs,
      rowData,
      modalActive,
      closeModal,
      openModal,
      getAPI,
    };
  },
};
</script>
