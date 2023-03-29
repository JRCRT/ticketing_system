<script>
import PasswordField from "@/components/PasswordField.vue";

export default {
  name: "Login Page",
  components: {
    PasswordField,
  },
};
</script>

<template>
  <div class="w-screen h-screen flex justify-center items-center">
    <div class="container w-72 grid gap-3 rounded-md border-2 p-6">
      <div>
        <input class="input__search" style="text" placeholder="Username" />
        <PasswordField />
      </div>
      <button>Login</button>
    </div>
  </div>
</template>
