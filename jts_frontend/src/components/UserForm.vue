<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <label> Username </label>
      <input v-model="username" class="input__field" />
      <label> Password </label>
      <input v-model="password" class="input__field" type="password" />
      <label> First Name </label>
      <input v-model="firstname" class="input__field" />
      <label> Middle Name </label>
      <input v-model="middlename" class="input__field" />
      <label> Last Name </label>
      <input v-model="lastname" class="input__field" />
      <label> Email Address </label>
      <input v-model="emailAddress" class="input__field" />
      <label> Role </label>
      <VueMultiselect
        :options="roles"
        v-model="selectedRole"
        label="name"
      ></VueMultiselect>
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

<script lang="js">
import { onMounted, ref } from "vue";
import { useStore } from "vuex";
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
    const store = useStore();
    const selectedDepartment = ref(null);
    const selectedRole = ref(null);
    const username = ref(null);
    const password = ref(null);
    const firstname = ref(null);
    const middlename = ref(null);
    const lastname = ref(null);
    const emailAddress = ref(null);
    const departments = [
      "IT","HR"
    ]
    const roles = ref([]);
    onMounted(async()=>{
      await store.dispatch("role/loadRoles");
      roles.value = store.state.role.roles;
    })
    return {
      roles,
      departments,
      selectedDepartment,
      selectedRole,
      username,
  password ,
    firstname,
middlename,
lastname,
emailAddress,
    };
  },
};
</script>
