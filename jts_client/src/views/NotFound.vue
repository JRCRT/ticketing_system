<template>
  <div class="h-screen">
    <div class="flex flex-col h-1/2 items-center justify-center">
      <h1 class="font-bold text-[100px] text-primary">404</h1>
      <h3 class="text-[20px]">
        Opps! Page not found!
        <a
          class="underline text-primary cursor-pointer"
          @click="redirectToDashboard"
          >Click here</a
        >
        to redirect to Dashboard.
      </h3>
    </div>
  </div>
</template>

<script>
import { useRouter } from "vue-router";
export default {
  setup() {
    const router = useRouter();
    const redirectToDashboard = () => {
      router.replace({ name: "Dashboard" });
    };
    return {
      redirectToDashboard,
    };
  },
};
</script>
