<script>
export default {
  data() {
    return {
      isPasswordVisible: false,
      inputValue: "",
    };
  },
  props: {},
  methods: {
    togglePasswordVisibility() {
      this.isPasswordVisible = !this.isPasswordVisible;
    },
  },
};
</script>

<template>
  <div class="relative flex items-center h-8">
    <input
      class="bg-white p-1 mt-1 border-2 rounded-md shadow-sm focus:outline-none text-black text-sm w-full h-full"
      placeholder="Password"
      :type="isPasswordVisible ? 'text' : 'password'"
      v-model="inputValue"
    />

    <svg
      @click="togglePasswordVisibility()"
      v-if="inputValue != null && inputValue != '' && isPasswordVisible == true"
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      stroke-width="1.5"
      stroke="currentColor"
      class="w-6 h-6 absolute top-1/2 transform -translate-y-1/2 right-2"
    >
      <path
        stroke-linecap="round"
        stroke-linejoin="round"
        d="M2.036 12.322a1.012 1.012 0 010-.639C3.423 7.51 7.36 4.5 12 4.5c4.638 0 8.573 3.007 9.963 7.178.07.207.07.431 0 .639C20.577 16.49 16.64 19.5 12 19.5c-4.638 0-8.573-3.007-9.963-7.178z"
      />
      <path
        stroke-linecap="round"
        stroke-linejoin="round"
        d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"
      />
    </svg>
    <svg
      @click="togglePasswordVisibility()"
      v-if="
        inputValue != null && inputValue != '' && isPasswordVisible == false
      "
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      stroke-width="1.5"
      stroke="currentColor"
      class="w-6 h-6 absolute top-1/2 transform -translate-y-1/2 right-2"
    >
      <path
        stroke-linecap="round"
        stroke-linejoin="round"
        d="M2.036 12.322a1.012 1.012 0 010-.639C3.423 7.51 7.36 4.5 12 4.5c4.638 0 8.573 3.007 9.963 7.178.07.207.07.431 0 .639C20.577 16.49 16.64 19.5 12 19.5c-4.638 0-8.573-3.007-9.963-7.178z"
      />
      <path
        stroke-linecap="round"
        stroke-linejoin="round"
        d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"
      />
    </svg>
  </div>
</template>
