import {
  users,
  createUser,
  usersByRole,
  userById,
  updateUser,
} from "@/services/userService.js";
import { User } from "@/models/User";
const state = () => ({
  users: [],
  admins: [],
  approvers: [],
  checkers: [],
  user: {},
});

const getter = {
  users: (state) => {
    return state.user;
  },
};

const actions = {
  async fetchAllUsers({ commit }) {
    const response = await users();
    commit("FETCH_USERS", response.data);
  },

  async fetchUser({ commit }, id) {
    const response = await userById(id);
    commit("FETCH_USER", response.data);
  },

  async fetchAdmins({ commit }) {
    const response = await usersByRole("Admin");
    commit("FETCH_ADMINS", response.data);
  },

  async fetchApprovers({ commit }) {
    const response = await usersByRole("Approver");
    commit("FETCH_APPROVERS", response.data);
  },

  async fetchCheckers({ commit }) {
    const response = await usersByRole("Checker");
    commit("FETCH_CHECKERS", response.data);
  },

  async updateUser({ commit, dispatch }, user) {
    commit("app/SET_PROCESSING", true, { root: true });
    const response = await updateUser(user);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      commit("app/SET_PROCESSING", false, { root: true });
      return;
    }
    console.log(response.data);
    alert = { type: "success", message: response.message };
    commit("app/SET_PROCESSING", false, { root: true });
    commit("app/SET_USER_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },

  async createUser({ commit, dispatch }, user) {
    commit("app/SET_PROCESSING", true, { root: true });
    const response = await createUser(user);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      commit("app/SET_PROCESSING", false, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_PROCESSING", false, { root: true });
    commit("app/SET_NEW_USER_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },
};

const mutations = {
  FETCH_USERS(state, value) {
    let users = [...value].map((user) => user.user);
    state.users = users;
  },
  FETCH_ADMINS(state, value) {
    let users = [...value].map((user) => user.user);
    console.log("users:", users);
    state.admins = users;
  },
  FETCH_APPROVERS(state, value) {
    let users = [...value].map((user) => user.user);
    state.approvers = users;
  },
  FETCH_CHECKERS(state, value) {
    let users = [...value].map((user) => user.user);
    state.checkers = users;
  },
  ADD_USER(state, value) {
    state.users.push(value);
  },
  FETCH_USER(state, value) {
    state.user = value.user;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
