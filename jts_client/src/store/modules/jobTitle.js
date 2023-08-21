import { jobTitles } from "@/services/jobTitleService.js";
const state = () => ({
  jobTitles: [],
});

const getter = {};

const actions = {
  async fetchJobTitles({ commit }) {
    const response = await jobTitles();
    commit("FETCH_JOB_TITLES", response.data);
  },
};

const mutations = {
  FETCH_JOB_TITLES(state, value) {
    state.jobTitles = value;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
