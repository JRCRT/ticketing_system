<script>
import JFP_Logo from "@/assets/jaccs-logo-wo-bg.png";
export default {
  name: "Login Page",
};
</script>

<template>
  <div
    class="flex h-[calc(100vh-64px)] min-w-[100vw] !mt-0 justify-center items-center mx-0 px-0"
  >
    <div class="w-1/2 h-full">
      <img class="object-cover w-full h-full" :src="JFP_Logo" />
    </div>
    <div class="bg-primary h-full w-1/2">
      <div
        class="h-full flex flex-col items-center w-1/2 mx-auto my-auto justify-center"
      >
        <h3 class="mb-6 text-white">Login</h3>
        <input type="text" placeholder="Username" />
        <input type="password" placeholder="Password" />
        <button class="mt-3">Login</button>
      </div>
    </div>
  </div>
</template>
