<template>
  <div>
    <UserForm v-if="modalActive" @close="closeModal()" />
    <h4 class="text-primary">User</h4>
    <div class="flex justify-between items-end mt-2 mb-2">
      <div>
        <label>Search</label>
        <input class="input__field !w-56 !p-[0.30rem]" />
      </div>

      <button @click="openModal()" class="button-transparent !w-fit">
        New user
      </button>
    </div>
    <Table :rowData="rowData" :columnDefs="columnDefs"></Table>
  </div>
</template>
<script>
import UserForm from "@/components/UserForm.vue";
import Table from "@/components/Table.vue";

import { ref } from "vue";
export default {
  name: "User",
  components: {
    Table,
    UserForm,
  },

  setup() {
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

      {
        headerName: "Action",
        field: "action",
        flex: 1,
        cellRenderer: `<button>View</button>`,
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

    function openModal() {
      modalActive.value = true;
    }

    return {
      columnDefs,
      rowData,
      modalActive,
      closeModal,
      openModal,
    };
  },
};
</script>
