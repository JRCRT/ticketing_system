<script>
import Table from "@/components/Table.vue";
export default {
  name: "Dashboard",

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
      { headerName: "Status", field: "status", sortable: true, flex: 1 },
    ];
    const rowData = [
      {
        ticketId: 1,
        subject: "Additional Unit",
        preparedBy: "Juan Dela Cruz",
        datePrepared: "04/12/2023",
        status: "Pending",
      },
    ];

    return {
      columnDefs,
      rowData,
    };
  },
};
</script>
<template>
  <div class="flex flex-col gap-3">
    <h4 class="text-primary">Dashboard</h4>
    <!--Cards Container-->
    <div class="flex">
      <div class="card">
        <div class="card-content">
          <h1>3</h1>
          <p>Pending</p>
        </div>
      </div>
      <div class="card">
        <div class="card-content">
          <h1>2</h1>
          <p>Approved</p>
        </div>
      </div>
      <div class="card">
        <div class="card-content">
          <h1>2</h1>
          <p>Declined</p>
        </div>
      </div>
    </div>
    <!--Today's Transactions-->
    <div>
      <Table :rowData="rowData" :columnDefs="columnDefs" />
    </div>
  </div>
</template>
