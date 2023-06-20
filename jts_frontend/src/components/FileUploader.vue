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
      <ul class="image-list" v-show="files.length">
        <FilePreview
          v-for="file of files"
          :key="file.id"
          :file="file"
          tag="li"
          @remove="removeFile"
        />
      </ul>
    </DropZone>
    <!-- <button @click.prevent="uploadFiles(files)" class="upload-button">Upload</button> -->
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
const files = computed(() => store.state.file.files.length);

const addFiles = (files) => {
  store.commit("file/ADD_FILE", files);
};

const removeFile = (file) => {
  store.commit("file/REMOVE_FILE", file);
};

function onInputChange(e) {
  addFiles(e.target.files);
  e.target.value = null; // reset so that selecting the same file again will still cause it to fire this change
}

// Uploader
//import createUploader from "./compositions/file-uploader";
//const { uploadFiles } = createUploader("YOUR URL HERE");
</script>

<style scoped>
.drop-area {
  width: 100%;
  max-width: 800px;
  margin: 0 auto;
  padding: 50px;
  background: #ffffff55;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
  transition: 0.2s ease;

  &[data-active="true"] {
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);
    background: #ffffffcc;
  }
}

label {
  font-size: 36px;
  cursor: pointer;
  display: block;

  span {
    display: block;
  }

  input[type="file"]:not(:focus-visible) {
    position: absolute !important;
    width: 1px !important;
    height: 1px !important;
    padding: 0 !important;
    margin: -1px !important;
    overflow: hidden !important;
    clip: rect(0, 0, 0, 0) !important;
    white-space: nowrap !important;
    border: 0 !important;
  }

  .smaller {
    font-size: 16px;
  }
}

.image-list {
  display: flex;
  list-style: none;
  flex-wrap: wrap;
  padding: 0;
}

.upload-button {
  display: block;
  appearance: none;
  border: 0;
  border-radius: 50px;
  padding: 0.75rem 3rem;
  margin: 1rem auto;
  font-size: 1.25rem;
  font-weight: bold;
  background: #369;
  color: #fff;
  text-transform: uppercase;
}

button {
  cursor: pointer;
}
</style>
