<template>
  <button @click="click">Click</button>
  <RichTextEditor @inputChange="click" />
</template>

<script>
import RichTextEditor from "@/components/RichTextEditor.vue";

import { ref } from "vue";
export default {
  components: {
    RichTextEditor,
  },
  setup() {
    const value = ref(null);
    function click(event) {
      console.log(event.target.value);
    }

    return {
      value,
      click,
    };
  },
};
</script>
