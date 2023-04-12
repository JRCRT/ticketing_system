<template>
  <div class="ag-theme-alpine">
    <ag-grid-vue
      class="h-80 w-full"
      :columnDefs="columnDefs"
      :rowData="rowData"
      :defaultColDef="defaultColDef"
      :pagination="true"
    >
    </ag-grid-vue>
  </div>
</template>

<script>
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";
import { AgGridVue } from "ag-grid-vue3";
export default {
  components: {
    AgGridVue,
  },
  setup() {
    return {
      columnDefs: [
        { headerName: "Ticket ID", field: "ticketId", sortable: true, flex: 1 },
        { headerName: "Subject", field: "subject", sortable: true, flex: 2 },
        {
          headerName: "Prepared By",
          field: "preparedBy",
          sortable: true,
          flex: 1,
        },
        {
          headerName: "Date Prepared",
          field: "datePrepared",
          sortable: true,
          flex: 1,
        },
        { headerName: "Status", field: "status", sortable: true, flex: 1 },
      ],
      rowData: [
        {
          ticketId: 1,
          subject: "Additional Unit",
          preparedBy: "Juan Dela Cruz",
          datePrepared: "04/12/2023",
          status: "Pending",
        },
      ],
      defaultColDef: {
        resizable: true,
      },
    };
  },
};
</script>
