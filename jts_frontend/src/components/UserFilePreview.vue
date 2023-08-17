<template>
  <div class="file-preview">
    <div class="image-container" v-if="file?.file?.name || imageURI">
      <img
        :src="imageURI ?? file?.url"
        :alt="file?.file?.name ?? 'Signature'"
        :title="file?.file?.name ?? ''"
      />
      <span>{{ file?.file?.name }}</span>
    </div>

    <label class="button-wrap" for="input-file">
      <button>
        <input
          type="file"
          accept="image/*"
          id="input-file"
          @change="onInputChange"
        />Change
      </button>
    </label>
  </div>
</template>

<script>
import { useStore } from "vuex";
export default {
  props: ["file", "imageURI"],
  setup(props) {
    const file = props.file;
    const imageURI = props.imageURI;
    const store = useStore();
    const setFile = (files) => {
      store.commit("file/SET_FILE", files);
    };

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

<style scoped>
.file-preview {
  width: 150px;
  margin: 1rem;
  overflow: hidden;
}
.image-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

span {
  font-size: 12px;
  text-align: center;
}

img {
  width: 80px;
  height: 80px;
  display: block;
  object-fit: cover;
}

input[type="file"] {
  position: absolute;
}

.button-wrap {
  position: relative;
}
</style>
