<template>
  <div class="w-36 flex flex-col items-center">
    <div
      class="flex flex-col items-center mb-2"
      v-if="file?.file?.name || imageURI"
    >
      <img
        class="object-cover h-[80px] w-[80px]"
        :src="file?.url ?? imageURI"
        :alt="file?.file?.name ?? 'Signature'"
        :title="file?.file?.name ?? ''"
      />
    </div>
    <label
      class="button-primary text-center py-[6px] px-2 w-fit rounded-md cursor-pointer"
      htmlFor="file-input"
    >
      {{ !file?.file?.name && !imageURI ? "Add" : "Change" }}
    </label>
    <input
      type="file"
      accept="image/*"
      id="file-input"
      @change="onInputChange"
    />
  </div>
</template>

<script>
import { computed } from "vue";
import { useStore } from "vuex";
export default {
  setup() {
    const store = useStore();
    const setFile = (files) => {
      store.commit("file/SET_FILE", files);
    };

    const imageURI = computed(() => store.state.file.imageURI);
    const file = computed(() => store.state.file.file);

    function onInputChange(e) {
      setFile(e.target.files[0]);
      e.target.value = null;
    }

    return {
      onInputChange,
      file,
      imageURI,
    };
  },
};
</script>
