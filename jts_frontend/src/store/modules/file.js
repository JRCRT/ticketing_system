import { UploadedFile } from "@/models/UploadedFile";

const state = () => ({
  files: [],
});

const getters = {
  files: (state) => {
    return state.files.map((file) => file.file);
  },
};

const actions = {};

const mutations = {
  ADD_FILE(state, value) {
    let newUploadableFiles = [...value]
      .map((file) => new UploadedFile(file))
      .filter((file) => !state.files.some(({ id }) => id === file.id));
    state.files = state.files.concat(newUploadableFiles);
  },

  REMOVE_FILE(state, value) {
    const index = state.files.indexOf(value);
    if (index > -1) state.files.splice(index, 1);
  },

  EMPTY_FILE(state, value){
    state.files = value;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
