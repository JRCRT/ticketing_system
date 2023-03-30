<script>
export default {
  name: "Login Page",
};
</script>

<template>
  <div class="w-screen h-screen flex justify-center items-center">
    <div class="container w-72 grid gap-3 rounded-md border-2 p-6">
      <div>
        <input type="text" placeholder="Username" />
        <input type="password" placeholder="Password" />
      </div>
      <button>Login</button>
    </div>
  </div>
</template>
