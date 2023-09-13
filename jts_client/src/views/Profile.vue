<template>
  <div class="flex flex-col gap-3">
    <h4 class="text-primary">Profile</h4>
    <div>
      <label>Username</label>
      <input
        :disabled="true"
        v-model="username"
        class="input__field disabled:bg-[#f5f5f5]"
      />
      <label>Name</label>
      <input
        :disabled="true"
        v-model="name"
        class="input__field disabled:bg-[#f5f5f5]"
      />
      <label>Role</label>
      <input
        :disabled="true"
        v-model="role"
        class="input__field disabled:bg-[#f5f5f5]"
      />
      <label>Department</label>
      <input
        :disabled="true"
        v-model="department"
        class="input__field disabled:bg-[#f5f5f5]"
      />
      <label>Signature</label>
      <div class="flex justify-center border-[1.6px] p-3 rounded-md">
        <UserFilePreview :file="file" />
      </div>
      <button class="button-primary text-[14px] w-16">Save</button>
    </div>
  </div>
</template>
<script>
import UserFilePreview from "@/components/UserFilePreview.vue";
import { computed, onMounted, ref, onUnmounted } from "vue";
import { useStore } from "vuex";
const BASE_URL = import.meta.env.VITE_BASE_URL;
export default {
  components: {
    UserFilePreview,
  },

  setup() {
    const username = ref("");
    const name = ref("");
    const role = ref("");
    const department = ref("");
    const store = useStore();
    const file = computed(() => store.state.file.file);
    const currentUser = JSON.parse(localStorage.getItem("user"));
    const currentProfile = ref({});

    const save = async () => {
      var userformData = new FormData();

      userformData.append("user_id", currentProfile.value.user.user_id);
      userformData.append("first_name", currentProfile.value.user.first_name);
      userformData.append(
        "middle_name",
        currentProfile.value.user.middle_name ?? ""
      );
      userformData.append("last_name", currentProfile.value.user.last_name);
      userformData.append("username", currentProfile.value.user.username);
      userformData.append("short_name", currentProfile.value.user.short_name);
      userformData.append("email", currentProfile.value.user.email);
      userformData.append("role_id", currentProfile.value.user.role.role_id);
      userformData.append(
        "department_id",
        currentProfile.value.user.department.department_id
      );
      userformData.append(
        "job_title_id",
        currentProfile.value.user.job_title_job_title_id
      );
      userformData.append("password", currentProfile.value.user.password_hash);
      userformData.append("file", file.value.file);

      await store.dispatch("user/updateUser", userformData);
    };

    onMounted(async () => {
      await store.dispatch("user/fetchUser", currentUser.user_id);

      const user = store.state.user.user;
      currentProfile.value = user;
      username.value = user.user.username;
      name.value = user.user.ext_name;
      role.value = user.user.role.name;
      department.value = user.user.department.name;
      store.commit(
        "file/SET_IMAGE_URI",
        user.file?.stored_file_name
          ? `${BASE_URL}/File/${user.file.stored_file_name}`
          : ""
      );

      console.log(user);
    });

    onUnmounted(() => {
      store.commit("file/SET_IMAGE_URI", null);
      store.commit("file/EMPTY_FILE", {});
    });
    return {
      file,
      username,
      name,
      role,
      department,
    };
  },
};
</script>
