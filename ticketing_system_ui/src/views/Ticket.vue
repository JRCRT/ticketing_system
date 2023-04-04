<template>
  <div>
    <div class="flex">
      <button
        @click="currentTab = tab"
        :class="{ active: currentTab == tab }"
        v-for="tab in tabs"
        :key="tab"
      >
        {{ tab }}
      </button>
    </div>
    <component :is="currentTab" />
  </div>
</template>
<style scoped>
.active {
  background-color: green;
}
</style>
<script>
import PendingTicket from "@/components/ticket_status/PendingTicket.vue";
import ApprovedTicket from "@/components/ticket_status/ApprovedTicket.vue";
import DeclinedTicket from "@/components/ticket_status/DeclinedTicket.vue";
export default {
  components: {
    PendingTicket,
    ApprovedTicket,
    DeclinedTicket,
  },
  data() {
    return {
      currentTab: "PendingTicket",
      tabs: ["PendingTicket", "ApprovedTicket", "DeclinedTicket"],
    };
  },
};
</script>
