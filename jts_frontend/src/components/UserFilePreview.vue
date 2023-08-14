<template>
  <div class="file-preview">
    <div class="image-container">
      <img :src="file?.url" :alt="file?.file?.name" :title="file?.file?.name" />
      <span>{{ file?.file?.name }}</span>
    </div>

    <label class="button-wrap" for="input-file">
      <input
        type="file"
        accept="image/*"
        id="input-file"
        @change="onInputChange"
      />
      <button>Change</button>
    </label>
  </div>
</template>

<script>
import { useStore } from "vuex";
export default {
  props: {
    file: {},
  },
  setup() {
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
  z-index: -1;
  top: 15px;
  left: 20px;
  font-size: 17px;
  color: #b8b8b8;
}

.button-wrap {
  position: relative;
}
</style>
