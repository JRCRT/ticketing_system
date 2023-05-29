import { users } from "@/services/userService.js";
const state = () => ({
  users: [],
});

const getter = {
  users: (state) => {
    return state.user;
  },
};

const actions = {
  async loadUsers({ commit }) {
    const response = await users();
    commit("populateUsers", response.data);
  },
};

const mutations = {
  populateUsers(state, value) {
    state.users = value;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
