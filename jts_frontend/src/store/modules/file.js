import { UploadedFile } from "@/models/UploadedFile";

const state = () => ({
  files: [],
});

const getter = {};

const actions = {};

const mutations = {
  ADD_FILE(state, value) {
    let newUploadableFiles = [...value]
      .map((file) => new UploadedFile(file))
      .filter((file) => !state.files.some(({ id }) => id === file.id));
    state.files = state.files.concat(newUploadableFiles);
    console.log(state.files);
  },

  REMOVE_FILE(state, value) {
    const index = state.files.indexOf(value);
    if (index > -1) state.files.splice(index, 1);
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
