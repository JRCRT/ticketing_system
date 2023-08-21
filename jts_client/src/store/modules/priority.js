import { priorities } from "@/services/priorityService.js";
const state = () => ({
  priorities: [],
});

const getters = {};

const actions = {
  async fetchPriorities({ commit }) {
    const response = await priorities();
    commit("FETCH_PRIORITIES", response.data);
  },
};

const mutations = {
  FETCH_PRIORITIES(state, value) {
    state.priorities = value;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
