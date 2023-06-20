import { File } from "@/models/File";
const state = () => ({
  files: [],
});

const getter = {};

const actions = {};

const mutations = {
  ADD_FILE(state, value) {
    let newUploadableFiles = [...value]
      .map((file) => new File(file))
      .filter((file) => !state.files.some(({ id }) => id === file.id));
    state.files = state.files.concat(newUploadableFiles);
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