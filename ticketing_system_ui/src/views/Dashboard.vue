<script>
import Table from "@/components/Table.vue";
import TicketStatus from "@/components/TicketStatus.vue";
import { useRouter } from "vue-router";
import { ref } from "vue";
export default {
  name: "Dashboard",

  components: {
    Table,
    TicketStatus,
  },
  setup() {
    const router = useRouter();
    const gridAPI = ref(null);
    const rowSelection = "single";
    const selectedRow = ref(null);
    const columnDefs = [
      { headerName: "Ticket ID", field: "ticketId", flex: 1 },
      { headerName: "Subject", field: "subject", flex: 2 },
      {
        headerName: "Prepared By",
        field: "preparedBy",
        flex: 1,
      },
      {
        headerName: "Date Prepared",
        field: "datePrepared",
        flex: 1,
      },
      {
        headerName: "Status",
        field: "status",
        flex: 1,
        cellRenderer: TicketStatus,
      },
      {
        headerName: "Action",
        field: "action",
        flex: 1,
      },
    ];
    const rowData = [
      {
        ticketId: 1,
        subject: "Additional Unit",
        preparedBy: "Juan Dela Cruz",
        datePrepared: "04/12/2023",
        status: "Pending",
      },
      {
        ticketId: 2,
        subject: "Additional Unit",
        preparedBy: "Pedro",
        datePrepared: "04/12/2023",
        status: "Approved",
      },
    ];

    function navigateToTicket(status) {
      router.replace({ name: "Ticket", params: { status: status } });
    }

    function onGridReady(params) {
      gridAPI.value = params.api;
    }

    function getSelectedRow() {
      console.log(gridAPI.value.getSelectedRows());
    }

    return {
      columnDefs,
      rowData,
      navigateToTicket,
      onGridReady,
      getSelectedRow,
    };
  },
};
</script>
<template>
  <div class="flex flex-col gap-3">
    <h4 @click="getSelectedRow()" class="text-primary">Dashboard</h4>
    <!--Cards Container-->
    <div class="flex">
      <div @click="navigateToTicket('pending')" class="card">
        <div class="card-content">
          <h1>3</h1>
          <p>Pending</p>
        </div>
      </div>
      <div @click="navigateToTicket('approved')" class="card">
        <div class="card-content">
          <h1>2</h1>
          <p>Approved</p>
        </div>
      </div>
      <div @click="navigateToTicket('declined')" class="card">
        <div class="card-content">
          <h1>2</h1>
          <p>Declined</p>
        </div>
      </div>
    </div>
    <!--Today's Transactions-->
    <div>
      <Table
        :rowData="rowData"
        :columnDefs="columnDefs"
        @grid-ready="onGridReady"
      ></Table>
    </div>
  </div>
</template>
