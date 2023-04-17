<template>
  <div class="flex flex-col gap-3">
    <Transition name="fade">
      <TicketForm v-if="modalActive" @close="closeModal()" />
    </Transition>
    <h4 class="text-primary">Ticket</h4>
    <div class="relative">
      <div class="flex justify-between">
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

        <button class="w-14 button-transparent" @click="openModal()">
          New
        </button>
      </div>
      <div class="border-b w-full absolute bottom-0"></div>
    </div>
    <component :is="currentTab" />
  </div>
</template>

<script>
import PendingTicket from "@/views/Ticket/PendingTicket.vue";
import ApprovedTicket from "@/views/Ticket/ApprovedTicket.vue";
import DeclinedTicket from "@/views/Ticket/DeclinedTicket.vue";
import TicketForm from "@/components/TicketForm.vue";
export default {
  components: {
    PendingTicket,
    ApprovedTicket,
    DeclinedTicket,
    TicketForm,
  },

  data() {
    return {
      currentTab: "PendingTicket",
      tabs: [
        { name: "PendingTicket", label: "Pending" },
        { name: "ApprovedTicket", label: "Approved" },
        { name: "DeclinedTicket", label: "Declined" },
      ],
      modalActive: false,
    };
  },

  methods: {
    closeModal() {
      this.modalActive = false;
    },
    openModal() {
      this.modalActive = true;
    },
  },
};
</script>
