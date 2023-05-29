<script>
import JFP_Logo from "@/assets/jaccs-logo-wo-bg.png";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { onMounted, ref } from "vue";
export default {
  name: "Login Page",
  setup() {
    const username = ref(null);
    const password = ref(null);
    const store = useStore();
    const router = useRouter();
    function getCurrentURL() {
      /* console.log(router.currentRoute.value.path); */
    }
    async function login() {
      await store.dispatch("auth/login", {
        username: username.value,
        password: password.value,
      });
    }

    onMounted(async () => {
      await router.isReady();

      store.dispatch("app/changeUrl", router.currentRoute.value.path);
    });
    return {
      JFP_Logo,
      getCurrentURL,
      login,
      username,
      password,
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
      <input v-model="username" class="input__field" type="text" />
    </div>
    <div class="w-full mb-3">
      <label>Password</label>
      <input v-model="password" class="input__field" type="password" />
    </div>
    <button class="mt-3 h-9 button-primary">Login</button>
  </form>
</template>
