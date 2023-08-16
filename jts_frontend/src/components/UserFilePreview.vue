<template>
  <div class="file-preview">
    <div class="image-container">
      <img
        :src="file?.url ?? file?.file_url"
        :alt="file?.file?.name ?? file?.original_file_name"
        :title="file?.file?.name ?? file?.original_file_name"
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
}

.button-wrap {
  position: relative;
}
</style>
