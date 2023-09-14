<template>
  <Modal class="w-2/4" @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">Reason For Rejection</h5>
    </template>
    <template v-slot:content>
      <label>Reason</label>
      <input class="input__field" v-model="reason" />
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button class="button-primary mr-2" @click="reject">
            {{ isSubmitting ? "Submitting..." : "Submit" }}
          </button>
          <button class="button-transparent" @click="$emit('close')">
            Cancel
          </button>
        </div>
      </div>
    </template>
  </Modal>
</template>
<script>
import Modal from "@/components/Modal.vue";
import { useStore } from "vuex";
import { ref, computed } from "vue";
export default {
  emits: ["close"],
  components: {
    Modal,
  },
  setup() {
    const store = useStore();
    const reason = ref("");
    const approver = computed(() => store.state.app.signatory);
    const isSubmitting = computed(() => store.state.app.isSubmitting);
    const reject = async () => {
      const _approver = {
        signatory_id: approver.value.signatoryId,
        connection_id: approver.value.connectionId,
        rejection_reason: reason.value,
      };
      if (reason.value === "") {
        var alert = {
          type: "danger",
          message: "Kindly fill up the reason for rejection.",
        };
        store.dispatch("app/addAlert", alert);
        return;
      }
      await store.dispatch("ticket/rejectTicket", _approver);
    };
    return {
      reason,
      reject,
      isSubmitting,
    };
  },
};
</script>
