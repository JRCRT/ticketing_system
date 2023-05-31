import { users, createUser } from "@/services/userService.js";
import { User } from "@/models/User";
const state = () => ({
  users: [],
});

const getter = {
  users: (state) => {
    return state.user;
  },
};

const actions = {
  async fetchUsers({ commit }) {
    const response = await users();
    commit("FETCH_USERS", response.data);
  },

  async createUser({ commit }, user) {
    commit("app/SET_LOADING", true, { root: true });
    const response = await createUser(user);
    var alert;
    if (!response.success) {
      alert = { type: "danger", message: response.message };
      commit("app/ADD_ALERT", alert, { root: true });
      commit("app/SET_LOADING", false, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("ADD_USER", response.data);
    commit("app/SET_LOADING", false, { root: true });
    commit("app/ADD_ALERT", alert, { root: true });
  },
};

const mutations = {
  FETCH_USERS(state, value) {
    state.users = value;
  },
  ADD_USER(state, value) {
    state.users.push(value);
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
