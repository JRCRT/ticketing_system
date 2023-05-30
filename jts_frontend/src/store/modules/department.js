import { departments } from "@/services/departmentService.js";
const state = () => ({
  departments: [],
});

const getter = {};

const actions = {
  async loadDepartments({ commit }) {
    const response = await departments();
    commit("populateDepartments", response.data);
  },
};

const mutations = {
  populateDepartments(state, value) {
    state.departments = value;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
