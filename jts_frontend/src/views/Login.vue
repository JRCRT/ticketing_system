<script>
import JFP_Logo from "@/assets/jaccs-logo-wo-bg.png";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { onMounted } from "vue";
export default {
  name: "Login Page",
  setup() {
    const store = useStore();
    const router = useRouter();
    function getCurrentURL() {
      /* console.log(router.currentRoute.value.path); */
    }
    async function login() {
      store.dispatch("auth/login", {
        username: "225RGR",
        password: "P@ssw0rd!",
      });
    }

    onMounted(async () => {
      await router.isReady();

      store.dispatch("app/changeUrl", router.currentRoute.value.path);

      console.log(`currentPath: ${store.state.currentURL}`);
    });
    return {
      JFP_Logo,
      getCurrentURL,
      login,
    };
  },
};
</script>

<template>
  <div
    class="flex flex-col w-80 h-screen !mt-0 justify-center items-center mx-0 px-0"
  >
    <img class="w-60 h-16 object-cover" :src="JFP_Logo" alt="JACCS Logo" />
    <div class="w-full mt-6">
      <label>Username</label>
      <input class="input__field" type="text" />
    </div>
    <div class="w-full mb-3">
      <label>Password</label>
      <input class="input__field" type="password" />
    </div>
    <button @click="login()" class="mt-3 h-9 button-primary">Login</button>
  </div>
</template>
