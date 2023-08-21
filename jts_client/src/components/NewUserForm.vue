<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">New</h5>
    </template>
    <template v-slot:content>
      <div class="new_user_form_container">
        <label> Username </label>
        <input v-model="username" autocomplete="false" class="input__field" />
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
        <label>Signature</label>
        <FileUploader :isMultiple="false" accept="image/*" />
      </div>
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button
            class="button-primary mr-2"
            :disabled="isProcessing"
            @click="createUser"
          >
            {{ isProcessing ? "Creating..." : "Create" }}
          </button>
          <button
            class="button-transparent"
            :disabled="isProcessing"
            @click="$emit('close')"
          >
            Cancel
          </button>
        </div>
      </div>
    </template>
  </Modal>
</template>

<script>
import { computed, onMounted, ref, onUnmounted } from "vue";
import { useStore } from "vuex";
import Modal from "@/components/Modal.vue";
import VueMultiselect from "vue-multiselect";
import FileUploader from "@/components/FileUploader.vue";
import "vue-multiselect/dist/vue-multiselect.css";

export default {
  emits: ["close"],
  components: {
    Modal,
    VueMultiselect,
    FileUploader,
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
    const isProcessing = computed(() => store.state.app.isProcessing);

    const validate = () => {
      var alert;
      var hasError = false;
      if (!username.value) {
        alert = {
          type: "danger",
          message: "Please fill up the username field.",
        };
        hasError = true;
      } else if (!password.value) {
        alert = {
          type: "danger",
          message: "Please fill up the password field.",
        };
        hasError = true;
      } else if (!firstname.value) {
        alert = {
          type: "danger",
          message: "Please fill up the firstname field.",
        };
        hasError = true;
      } else if (!lastname.value) {
        alert = {
          type: "danger",
          message: "Please fill up the lastname field.",
        };
        hasError = true;
      } else if (!emailAddress.value) {
        alert = {
          type: "danger",
          message: "Please fill up the email field.",
        };
        hasError = true;
      } else if (!selectedDepartment.value.name) {
        alert = {
          type: "danger",
          message: "Please select department.",
        };
        hasError = true;
      } else if (!selectedRole.value.name) {
        alert = {
          type: "danger",
          message: "Please select role.",
        };
        hasError = true;
      } else if (!selectedJobTitle.value.name) {
        alert = {
          type: "danger",
          message: "Please select job title.",
        };
        hasError = true;
      }

      if (hasError) {
        store.dispatch("app/addAlert", alert);
      }

      return hasError;
    };

    const createUser = async () => {
      if (!validate()) {
        var userFormData = new FormData();
        userFormData.append("first_name", firstname.value);
        userFormData.append("middle_name", middlename.value);
        userFormData.append("last_name", lastname.value);
        userFormData.append("username", username.value);
        userFormData.append("password", password.value);
        userFormData.append("email", emailAddress.value);
        userFormData.append("role_id", selectedRole.value.role_id);
        userFormData.append(
          "department_id",
          selectedDepartment.value.department_id
        );
        userFormData.append("file", store.state.file.file.file);
        userFormData.append(
          "job_title_id",
          selectedJobTitle.value.job_title_id
        );
        userFormData.append("first_name", firstname.value);
        await store.dispatch("user/createUser", userFormData);
        console.log(store.state.file.file.file);
      }
    };

    onMounted(async () => {
      await store.dispatch("role/fetchRoles");
      await store.dispatch("department/fetchDepartments");
      await store.dispatch("jobTitle/fetchJobTitles");
      roles.value = store.state.role.roles;
      departments.value = store.state.department.departments;
      jobTitles.value = store.state.jobTitle.jobTitles;
    });

    onUnmounted(() => {
      store.commit("file/EMPTY_FILE", {});
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
      isProcessing,
      createUser,
    };
  },
};
</script>
