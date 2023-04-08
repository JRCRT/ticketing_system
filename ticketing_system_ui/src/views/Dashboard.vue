<script>
import Table from "@/components/Table.vue";
export default {
  name: "Dashboard",

  components: {
    Table,
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
      <Table />
    </div>
  </div>
</template>
