<template>
  <button @click="show = !show">Toggle Slide + Fade</button>
  <Transition name="slide-fade">
    <p v-if="show">hello</p>
  </Transition>
</template>

<style scoped></style>
<script>
import Loader from "@/components/Loader.vue";
export default {
  components: {
    Loader,
  },
  data() {
    return {
      show: true,
    };
  },
};
</script>
