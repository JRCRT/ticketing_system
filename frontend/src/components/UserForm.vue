<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <label> Username </label>
      <input class="input__field" />
      <label> Password </label>
      <input class="input__field" type="password" />
      <label> FullName </label>
      <input class="input__field" />
      <label> Email Address </label>
      <input class="input__field" />
      <label> Department </label>
      <VueMultiselect
        :options="departments"
        v-model="selectedDepartment"
      ></VueMultiselect>
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button class="button-primary mr-2">Save</button>
          <button class="button-transparent" @click="$emit('close')">
            Cancel
          </button>
        </div>
      </div>
    </template>
  </Modal>
</template>
<script>
import { ref } from "vue";
import Modal from "@/components/Modal.vue";
import VueMultiselect from "vue-multiselect";
import "vue-multiselect/dist/vue-multiselect.css";
export default {
  emits: ["close"],
  components: {
    Modal,
    VueMultiselect,
  },
  setup() {
    const selectedDepartment = ref(null);
    const departments = [
      "IT",
      "Loan Administration",
      "Accounting",
      "Sales and Marketing",
      "Collection",
      "Internal Control",
    ];
    return {
      departments,
      selectedDepartment,
    };
  },
};
</script>
