<template>
  <div
    class="alert"
    :class="{
      'alert-danger': alert.type === 'danger',
      'alert-info': alert.type === 'info',
      'alert-success': alert.type === 'success',
    }"
  >
    <span class="message">{{ alert.message }} </span>
    <button
      @click="$emit('remove')"
      class="button button-transparent button-icon button-icon-md"
    >
      <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 20 20"
        fill="currentColor"
      >
        <!-- Icon: X-sm -->
        <path
          fill-rule="evenodd"
          d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
          clip-rule="evenodd"
        />
      </svg>
    </button>
  </div>
</template>

<script>
import { useStore } from "vuex";
export default {
  props: ["alert"],
  emits: ["remove"],
  setup() {
    const store = useStore();
    setTimeout(() => {
      store.dispatch("app/removeAlert", store.state.app.alerts.length - 1);
    }, 5000);
  },
};
</script>

<style></style>
