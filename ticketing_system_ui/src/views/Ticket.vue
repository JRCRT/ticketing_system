<template>
  <div class="flex flex-col gap-3">
    <h4 class="text-primary">Ticket</h4>
    <div class="relative">
      <div class="flex">
        <div
          @click="currentTab = tab.name"
          :class="currentTab == tab.name ? 'active__tab' : 'tab'"
          v-for="tab in tabs"
          :key="tab"
        >
          {{ tab.label }}
        </div>
      </div>
      <div class="border-b w-full absolute bottom-0"></div>
    </div>
    <component :is="currentTab" />
  </div>
</template>

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
      tabs: [
        { name: "PendingTicket", label: "Pending" },
        { name: "ApprovedTicket", label: "Approved" },
        { name: "DeclinedTicket", label: "Declined" },
      ],
    };
  },
};
</script>
