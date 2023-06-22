<template>
  <div id="app">
    <DropZone
      class="drop-area"
      @files-dropped="addFiles"
      #default="{ dropZoneActive }"
    >
      <label for="file-input">
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

        <input type="file" id="file-input" multiple @change="onInputChange" />
      </label>
      <ul class="image-list" v-show="files">
        <FilePreview
          v-for="file of files"
          :key="file.id"
          :file="file"
          tag="li"
          @remove="removeFile"
        />
      </ul>
    </DropZone>
  </div>
</template>

<script setup>
// Components
import DropZone from "@/components/DropZone.vue";
import FilePreview from "@/components/FilePreview.vue";
import { computed, ref } from "vue";
import { useStore } from "vuex";

const store = useStore();
// File Management
//import useFileList from "./compositions/file-list";
//const { files, addFiles, removeFile } = useFileList();
const files = computed(() => store.state.file.files);

const addFiles = (files) => {
  store.commit("file/ADD_FILE", files);
};

const removeFile = (file) => {
  store.commit("file/REMOVE_FILE", file);
};

function onInputChange(e) {
  addFiles(e.target.files);
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
.image-list {
  display: flex;
  list-style: none;
  flex-wrap: wrap;
  padding: 0;
}

button {
  cursor: pointer;
}
</style>
