<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">User</h5>
    </template>
    <template v-slot:content>
      <div class="user_form_container">
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
        <label> Short Name </label>
        <input v-model="shortName" class="input__field" />
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
        <div class="flex justify-center border-[1.6px] p-3 rounded-md">
          <UserFilePreview :file="file" />
        </div>
      </div>
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button
            class="button-primary mr-2"
            :disabled="isProcessing"
            @click="updateUser"
          >
            {{ isProcessing ? "Saving..." : "Save" }}
          </button>
          <button
            :disabled="isProcessing"
            class="button-transparent"
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
import { onMounted, ref, watch, computed, onUnmounted } from "vue";
import { useStore } from "vuex";
import { useRoute } from "vue-router";
import Modal from "@/components/Modal.vue";
import VueMultiselect from "vue-multiselect";
import UserFilePreview from "@/components/UserFilePreview.vue";
import "vue-multiselect/dist/vue-multiselect.css";
const BASE_URL = import.meta.env.VITE_BASE_URL;
export default {
  emits: ["close"],
  components: {
    Modal,
    VueMultiselect,
    UserFilePreview,
  },

  setup() {
    const store = useStore();
    const route = useRoute();
    const selectedDepartment = ref({});
    const selectedRole = ref({});
    const selectedJobTitle = ref({});
    const username = ref(null);
    const password = ref(null);
    const firstname = ref(null);
    const middlename = ref(null);
    const lastname = ref(null);
    const shortName = ref(null);
    const emailAddress = ref(null);
    const departments = ref([]);
    const roles = ref([]);
    const jobTitles = ref([]);
    const user = ref({});
    const isProcessing = computed(() => store.state.app.isProcessing);
    const file = computed(() => store.state.file.file);

    watch(
      () => route.params.userId,
      async (newUserId, oldUserId) => {
        if (newUserId) {
          await store.dispatch("user/fetchUser", newUserId);
          user.value = store.state.user.user;

          username.value = user.value.user.username;
          firstname.value = user.value.user.first_name;
          middlename.value = user.value.user.middle_name;
          lastname.value = user.value.user.last_name;
          emailAddress.value = user.value.user.email;
          password.value = user.value.user.password_hash;
          selectedDepartment.value = user.value.user.department;
          selectedRole.value = user.value.user.role;
          selectedJobTitle.value = user.value.user.job_title;
          shortName.value = user.value.user.short_name;

          store.commit(
            "file/SET_IMAGE_URI",
            user.value?.file?.stored_file_name
              ? `${BASE_URL}/File/${user.value.file.stored_file_name}`
              : ""
          );
        }
      },
      { immediate: true }
    );

    const updateUser = async () => {
      /* const userData = {
        user_id: user.value.user_id,
        first_name: firstname.value,
        middle_name: middlename.value,
        last_name: lastname.value,
        username: username.value,
        email: emailAddress.value,
        role_id: selectedRole.value.role_id,
        department_id: selectedDepartment.value.department_id,
        job_title_id: selectedJobTitle.value.job_title_id,
        password: password.value,
      }; */

      var userformData = new FormData();

      userformData.append("user_id", user.value.user.user_id);
      userformData.append("first_name", firstname.value);
      userformData.append("middle_name", middlename.value ?? "");
      userformData.append("last_name", lastname.value);
      userformData.append("username", username.value);
      userformData.append("short_name", shortName.value);
      userformData.append("email", emailAddress.value);
      userformData.append("role_id", selectedRole.value.role_id);
      userformData.append(
        "department_id",
        selectedDepartment.value.department_id
      );
      userformData.append("job_title_id", selectedJobTitle.value.job_title_id);
      userformData.append("password", password.value);
      //userformData.append("file", file.value);

      await store.dispatch("user/updateUser", userformData);
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
      store.commit("file/SET_IMAGE_URI", null);
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
      shortName,
      emailAddress,
      isProcessing,
      file,
      updateUser,
    };
  },
};
</script>
