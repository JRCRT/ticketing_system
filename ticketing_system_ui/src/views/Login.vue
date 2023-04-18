<script>
import JFP_Logo from "@/assets/jaccs-logo-wo-bg.png";
import { useRoute, useRouter } from "vue-router";
export default {
  name: "Login Page",
  setup() {
    const router = useRouter();
    function getCurrentURL() {
      console.log(router.currentRoute.value.path);
    }
    return {
      JFP_Logo,
      getCurrentURL,
    };
  },
};
</script>

<template>
  <div
    class="flex flex-col w-80 h-screen !mt-0 justify-center items-center mx-0 px-0"
  >
    <img class="w-60 h-16 object-cover" :src="JFP_Logo" />
    <div class="w-full mt-6">
      <label>Username</label>
      <input type="text" />
    </div>
    <div class="w-full mb-3">
      <label>Password</label>
      <input type="password" />
    </div>
    <button @click="getCurrentURL()" class="mt-3">Login</button>
  </div>
</template>
