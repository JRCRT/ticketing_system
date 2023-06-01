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

  async createUser({ commit, dispatch }, user) {
    commit("app/SET_LOADING", true, { root: true });
    const response = await createUser(user);
    var alert;
    if (!response.success) {
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      commit("app/SET_LOADING", false, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_LOADING", false, { root: true });
    commit("app/SET_USER_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
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
