import { roles } from "@/services/roleService.js";
const state = () => ({
  roles: [],
});

const getter = {};

const actions = {
  async fetchRoles({ commit }) {
    const response = await roles();
    commit("FETCH_ROLES", response.data);
  },
};

const mutations = {
  FETCH_ROLES(state, value) {
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
