<script lang="js">
import JFP_Logo from "@/assets/jaccs-logo-wo-bg.png";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { onMounted, ref } from "vue";
import { computed } from "@vue/reactivity";
export default {
  name: "Login Page",
  setup() {
    const username = ref(null);
    const password = ref(null);
    const usernameField = ref(null);
    const store = useStore();
    const router = useRouter();
    const isLoading = computed(() => store.state.app.isLoggingIn);
    const login = async () => {
      await store.dispatch("auth/login", {
        username: username.value,
        password: password.value,
      });
      username.value = '';
      password.value = '';
      usernameField.value.focus();
    };

    onMounted(async () => {
      await router.isReady();
      store.commit("app/SET_CURRENT_URL", router.currentRoute.value.path);
      usernameField.value.focus();
    });

    return {
      login,
      JFP_Logo,
      username,
      password,
      isLoading,
      usernameField,
    };
  },
};
</script>

<template>
  <form
    @submit.prevent="login"
    :on-keyup.enter="login"
    class="flex flex-col w-80 h-screen !mt-0 justify-center items-center mx-0 px-0"
  >
    <img class="w-60 h-16 object-cover" :src="JFP_Logo" alt="JACCS Logo" />

    <div class="w-full mt-6">
      <label>Username</label>
      <input
        ref="usernameField"
        v-model="username"
        class="input__field"
        type="text"
      />
    </div>
    <div class="w-full mb-3">
      <label>Password</label>
      <input v-model="password" class="input__field" type="password" />
    </div>
    <button class="mt-3 h-9 button-primary" :disabled="isLoading">
      {{ isLoading ? "Logging..." : "Login" }}
    </button>
  </form>
</template>
