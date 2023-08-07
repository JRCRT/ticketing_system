<template>
  <Modal class="w-2/4" @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">Reason For Decline</h5>
    </template>
    <template v-slot:content>
      <label>Reason</label>
      <input class="input__field" v-model="subject" />
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button class="button-primary mr-2">Ok</button>
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
export default {
  emits: ["close"],
  components: {
    Modal,
  },
  setup() {},
};
</script>
