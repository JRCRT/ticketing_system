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
  props: ["columnDefs", "rowData"],
  setup(props) {
    const columnDefs = props.columnDefs;
    const rowData = props.rowData;
    const defaultColDef = {
      resizable: true,
    };
    return {
      columnDefs,
      rowData,
      defaultColDef,
    };
  },
};
</script>
