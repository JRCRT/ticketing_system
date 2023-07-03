<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5>New</h5>
    </template>
    <template v-slot:content>
      <div>
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
          :show-labels="false"
        ></VueMultiselect>

        <label> Job Title </label>
        <VueMultiselect
          :options="jobTitles"
          v-model="selectedJobTitle"
          label="name"
          :show-labels="false"
        ></VueMultiselect>

        <label> Department </label>
        <VueMultiselect
          :options="departments"
          v-model="selectedDepartment"
          label="name"
          :show-labels="false"
        ></VueMultiselect>
      </div>
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button class="button-primary mr-2" @click="createUser">Save</button>
          <button class="button-transparent" @click="$emit('close')">
            Cancel
          </button>
        </div>
      </div>
    </template>
  </Modal>
</template>

<script>
import { onMounted, ref } from "vue";
import { useStore } from "vuex";
import { User } from "@/models/User";
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
    const selectedDepartment = ref({});
    const selectedRole = ref({});
    const selectedJobTitle = ref({});
    const username = ref(null);
    const password = ref(null);
    const firstname = ref(null);
    const middlename = ref(null);
    const lastname = ref(null);
    const emailAddress = ref(null);
    const departments = ref([]);
    const roles = ref([]);
    const jobTitles = ref([]);

    const createUser = async () => {
      const user = new User({
        first_name: firstname.value,
        middle_name: middlename.value,
        last_name: lastname.value,
        username: username.value,
        password: password.value,
        email: emailAddress.value,
        role_id: selectedRole.value.role_id,
        department_id: selectedDepartment.value.department_id,
        job_title_id: selectedJobTitle.value.job_title_id,
      });

      await store.dispatch("user/createUser", user);
      //tableApi.setRowData(store.state.user.users);
    };

    onMounted(async () => {
      await store.dispatch("role/fetchRoles");
      await store.dispatch("department/fetchDepartments");
      await store.dispatch("jobTitle/fetchJobTitles");
      roles.value = store.state.role.roles;
      departments.value = store.state.department.departments;
      jobTitles.value = store.state.jobTitle.jobTitles;
    });

    return {
      roles,
      departments,
      jobTitles,
      selectedDepartment,
      selectedRole,
      selectedJobTitle,
      username,
      password,
      firstname,
      middlename,
      lastname,
      emailAddress,
      createUser,
    };
  },
};
</script>
