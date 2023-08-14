<template>
  <div id="app">
    <DropZone
      class="drop-area"
      @files-dropped="setFile"
      #default="{ dropZoneActive }"
    >
      <label for="file-input" v-if="!file?.file?.name">
        <span v-if="dropZoneActive">
          <span>Drop Them Here</span>
          <span class="smaller">to add them</span>
        </span>
        <span v-else>
          <span>Drag Your Files Here</span>
          <span class="smaller">
            or <strong><em>click here</em></strong> to select files
          </span>
        </span>

        <input
          type="file"
          id="file-input"
          accept="image/*"
          @change="onInputChange"
        />
      </label>
      <div class="file-preview">
        <UserFilePreview :file="file" v-if="file?.file?.name" />
      </div>
    </DropZone>
  </div>
</template>

<script setup>
import DropZone from "@/components/DropZone.vue";
import UserFilePreview from "@/components/UserFilePreview.vue";
import { computed, ref } from "vue";
import { useStore } from "vuex";

const store = useStore();

const file = computed(() => store.state.file.file);
console.log(file.value);
const setFile = (files) => {
  store.commit("file/SET_FILE", files);
};

function onInputChange(e) {
  setFile(e.target.files[0]);
  e.target.value = null;
}
</script>

<style scoped>
.drop-area {
  width: 100%;
  margin: 0 auto;
  padding: 30px;
  background: #ffffff55;
  border-radius: 0.375rem /* 6px */;
  border: 1px solid rgb(176 176 176 / 1);
}

label {
  font-size: 20px;
  cursor: pointer;
  display: flex;
  justify-content: center;
}

span {
  display: block;
}

.smaller {
  font-size: 14px;
}
.file-preview {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 0;
}

button {
  cursor: pointer;
}
</style>
