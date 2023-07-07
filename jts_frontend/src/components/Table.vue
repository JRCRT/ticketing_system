<template>
  <div class="ag-theme-alpine">
    <ag-grid-vue
      class="h-[450px] w-full"
      :columnDefs="columnDefs"
      :rowData="rowData"
      :defaultColDef="defaultColDef"
      :pagination="true"
      @grid-ready="onGridReady"
      :rowSelection="rowSelection"
      :paginationPageSize="50"
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
  props: ["columnDefs", "rowData", "onGridReady", "onSelectionChange"],
  setup(props) {
    const columnDefs = props.columnDefs;
    const rowData = props.rowData;
    const onGridReady = props.onGridReady;

    const defaultColDef = {
      resizable: true,
      sortable: true,
    };
    const rowSelection = "single";
    return {
      columnDefs,
      rowData,
      defaultColDef,
      onGridReady,
      rowSelection,
    };
  },
};
</script>
