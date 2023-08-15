<template>
  <div id="app">
    <DropZone
      class="drop-area"
      @files-dropped="addFiles"
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
          :multiple="props.isMultiple"
          @change="onInputChange"
        />
      </label>

      <ul class="image-list" v-show="files">
        <TicketFilePreview
          v-for="file of files"
          :key="file.id"
          :file="file"
          tag="li"
          @remove="removeFile"
        />
      </ul>

      <div class="file-preview">
        <UserFilePreview :file="file" v-if="file?.file?.name" />
      </div>
    </DropZone>
  </div>
</template>

<script setup>
import DropZone from "@/components/DropZone.vue";
import TicketFilePreview from "@/components/TicketFilePreview.vue";
import UserFilePreview from "@/components/UserFilePreview.vue";
import { computed, ref } from "vue";
import { useStore } from "vuex";

const props = defineProps(["isMultiple"]);

const store = useStore();

const files = computed(() => store.state.file.files);
const file = computed(() => store.state.file.file);

const setFile = (file) => {
  store.commit("file/SET_FILE", file);
};

const addFiles = (files) => {
  store.commit("file/ADD_FILES", files);
};

const removeFile = (file) => {
  store.commit("file/REMOVE_FILE", file);
};

const onInputChange = (e) => {
  if (props.isMultiple) {
    addFiles(e.target.files);
  } else {
    setFile(e.target.files[0]);
  }

  e.target.value = null;
};
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
