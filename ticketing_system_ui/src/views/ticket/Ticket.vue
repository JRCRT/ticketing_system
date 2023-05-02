<template>
  <div class="flex flex-col gap-3">
    <TicketForm v-if="modalActive" @close="closeModal()" />

    <h4 class="text-primary">Ticket</h4>
    <div class="relative">
      <div class="flex justify-between">
        <div class="flex">
          <div
            @click="changeTab(tab)"
            :class="
              router.currentRoute.value.params.status == tab.status
                ? 'active__tab'
                : 'tab'
            "
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
    <KeepAlive>
      <component :is="currentTab" />
    </KeepAlive>
  </div>
</template>

<script>
import PendingTicket from "@/views/Ticket/PendingTicket.vue";
import ApprovedTicket from "@/views/Ticket/ApprovedTicket.vue";
import DeclinedTicket from "@/views/Ticket/DeclinedTicket.vue";
import TicketForm from "@/components/TicketForm.vue";
import { ref } from "vue";
import { useRouter } from "vue-router";

export default {
  components: {
    PendingTicket,
    ApprovedTicket,
    DeclinedTicket,
    TicketForm,
  },

  setup() {
    const router = useRouter();
    const currentTab = ref("PendingTicket");
    const tabs = [
      { name: "PendingTicket", label: "Pending", status: "pending" },
      { name: "ApprovedTicket", label: "Approved", status: "approved" },
      { name: "DeclinedTicket", label: "Declined", status: "declined" },
    ];
    const modalActive = ref(false);

    function closeModal() {
      modalActive.value = false;
    }

    function openModal() {
      modalActive.value = true;
    }

    function changeTab(tab) {
      currentTab.value = tab.name;
      router.replace({ name: "Ticket", params: { status: tab.status } });
    }

    return {
      currentTab,
      tabs,
      modalActive,
      closeModal,
      openModal,
      changeTab,
      router,
    };
  },
};
</script>
