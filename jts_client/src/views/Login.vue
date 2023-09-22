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
      <Password @inputChange="inputChange" />
    </div>

    <button class="mt-3 h-9 button-primary w-full" :disabled="isLoading">
      {{ isLoading ? "Logging..." : "Login" }}
    </button>
  </form>
</template>

<script lang="js">
import JFP_Logo from "@/assets/jaccs-logo-wo-bg.png";
import Password from "@/components/Password.vue"
import { useStore } from "vuex";
import { onMounted, ref } from "vue";
import { computed } from "@vue/reactivity";
export default {
  name: "Login Page",
  components:{
    Password
  },
  setup() {

    const username = ref(null);
    const password = ref(null);
    const usernameField = ref(null);
    const store = useStore();

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

    function inputChange (value) {
      password.value = value;
    }

    onMounted( () => {
      usernameField.value.focus();
    });

    return {
      login,
      inputChange,
      JFP_Logo,
      username,
      password,
      isLoading,
      usernameField,
    };
  },
};
</script>
