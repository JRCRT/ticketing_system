import { roles } from "@/services/roleService.js";
const state = () => ({
  roles: [],
});

const getter = {};

const actions = {
  async loadRoles({ commit }) {
    const response = await roles();
    commit("populateRoles", response.data);
  },
};

const mutations = {
  populateRoles(state, value) {
    state.roles = value;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
