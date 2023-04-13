<template>
  <Table :rowData="rowData" :columnDefs="columnDefs" />
</template>
<script>
import Table from "@/components/Table.vue";
export default {
  components: {
    Table,
  },

  setup() {
    const columnDefs = [
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
    ];
    const rowData = [
      {
        ticketId: 1,
        subject: "Additional Unit",
        preparedBy: "Juan Dela Cruz",
        datePrepared: "04/12/2023",
      },
    ];

    return {
      columnDefs,
      rowData,
    };
  },
};
</script>
