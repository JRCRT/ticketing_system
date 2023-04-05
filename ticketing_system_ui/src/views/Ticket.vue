<template>
  <div class="mt-6">
    <div class="relative">
      <div class="flex">
        <div
          @click="currentTab = tab"
          :class="{ active: currentTab == tab }"
          class="bg-transparent text-darkInfo w-fit rounded-t-md cursor-pointer p-2 mr-1 border-t border-l border-r border-t-transparent border-l-transparent border-r-transparent z-10 hover:bg-brown"
          v-for="tab in tabs"
          :key="tab"
        >
          {{ tab }}
        </div>
      </div>
      <div class="border-b w-full absolute bottom-0"></div>
    </div>
    <component :is="currentTab" />
  </div>
</template>
<style scoped>
.active {
  border-top-width: 1px;
  border-right-width: 1px;
  border-left-width: 1px;
  --tw-text-opacity: 1;
  border-top-color: rgb(29 78 216 / var(--tw-text-opacity));
  border-right-color: rgb(29 78 216 / var(--tw-text-opacity));
  border-left-color: rgb(29 78 216 / var(--tw-text-opacity));
  background-color: white;
  color: black;
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
