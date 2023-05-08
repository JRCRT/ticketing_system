<template>
  <textarea ref="textarea" v-model="modelValue"></textarea>
</template>
<script>
import { nextTick, onMounted, ref, watch } from "vue";
export default {
  props: [],
  setup() {
    const modelValue = ref("");
    const textarea = ref(null);

    watch(modelValue, (_, __) => {
      textarea.value.style.height = "auto";
      nextTick(() => {
        textarea.value.style.height = `${textarea.value.scrollHeight}px`;
      });
    });
    return {
      textarea,
      modelValue,
    };
  },
};
</script>
