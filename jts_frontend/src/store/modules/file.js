import { UploadedFile } from "@/models/UploadedFile";
import { getImage } from "@/services/fileService.js";

const state = () => ({
  files: [],
  file: {},
  imageURI: null,
});

const getters = {
  files: (state) => {
    return state.files.map((file) => file.file);
  },
};

const actions = {
  async getImage({ commit }, fileName) {
    const response = await getImage(fileName);
    commit("SET_FILE", response);
  },
};

const mutations = {
  ADD_FILES(state, value) {
    let newUploadableFiles = [...value]
      .map((file) => new UploadedFile(file))
      .filter((file) => !state.files.some(({ id }) => id === file.id));
    state.files = state.files.concat(newUploadableFiles);
  },

  REMOVE_FILE(state, value) {
    const index = state.files.indexOf(value);
    if (index > -1) state.files.splice(index, 1);
  },

  SET_FILE(state, value) {
    state.file = new UploadedFile(value);
  },

  EMPTY_FILES(state, value) {
    state.files = value;
  },

  EMPTY_FILE(state, value) {
    state.file = value;
  },

  SET_IMAGE_URI(state, value) {
    state.imageURI = value;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
