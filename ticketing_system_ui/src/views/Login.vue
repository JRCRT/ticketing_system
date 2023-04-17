<script>
export default {
  name: "Login Page",
};
</script>

<template>
  <div
    class="flex h-[calc(100vh-64px)] min-w-[100vw] !mt-0 justify-center items-center mx-0 px-0"
  >
    <div class="bg-primary w-1/2 h-full"></div>
    <div class="w-1/2">
      <div class="flex flex-col items-center w-1/2 mx-auto">
        <h3 class="mb-6 text-primary">Login</h3>
        <input type="text" placeholder="Username" />
        <input type="password" placeholder="Password" />
        <button class="mt-3">Login</button>
      </div>
    </div>
  </div>
</template>
