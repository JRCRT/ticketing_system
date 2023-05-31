import { departments } from "@/services/departmentService.js";
const state = () => ({
  departments: [],
});

const getter = {};

const actions = {
  async fetchDepartments({ commit }) {
    const response = await departments();
    commit("FETCH_DEPARTMENTS", response.data);
  },
};

const mutations = {
  FETCH_DEPARTMENTS(state, value) {
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
