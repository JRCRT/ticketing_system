import {
  users,
  createUser,
  usersByRole,
  userById,
  updateUser,
  checkers,
  relatedParties,
} from "@/services/userService.js";
import { User } from "@/models/User";
const state = () => ({
  users: [],
  admins: [],
  approvers: [],
  checkers: [],
  relatedParties: [],
  user: {},
});

const getter = {
  users: (state) => {
    return state.user;
  },
};

const actions = {
  async fetchAllUsers({ commit }, param) {
    const response = await users(param);
    commit("FETCH_USERS", response.data);
  },

  async fetchUser({ commit }, id) {
    commit("app/SET_MODAL_LOADING", true, { root: true });
    const response = await userById(id);
    if (response.success) {
      commit("FETCH_USER", response.data);
      commit("app/SET_MODAL_LOADING", false, { root: true });
    }
  },

  async fetchAdmins({ commit }) {
    const response = await usersByRole("Admin");
    commit("FETCH_ADMINS", response.data);
  },

  async fetchApprovers({ commit }) {
    const response = await usersByRole("Approver");
    commit("FETCH_APPROVERS", response.data);
  },

  async fetchCheckers({ commit }, param) {
    const response = await checkers(param);
    commit("FETCH_CHECKERS", response.data);
  },

  async fetchRelatedParties({ commit }, currentUserId) {
    const response = await relatedParties(currentUserId);
    commit("FETCH_RELATED_PARTIES", response.data);
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
    state.users = value;
  },
  FETCH_ADMINS(state, value) {
    state.admins = value;
  },
  FETCH_APPROVERS(state, value) {
    state.approvers = value;
  },
  FETCH_CHECKERS(state, value) {
    state.checkers = value;
  },
  ADD_USER(state, value) {
    state.users.push(value);
  },
  FETCH_USER(state, value) {
    state.user = value;
  },

  FETCH_RELATED_PARTIES(state, value) {
    state.relatedParties = value;
  },

  UPDATE_USER(state, value) {
    const updatedUser = value;
    const index = state.users.indexOf(
      state.users.find((u) => u.user.user_id === updatedUser.user.user_id)
    );
    state.users.splice(index, 1, updatedUser);
  },

  SEARCH_USER(state, value) {
    console.log(value);
    const result = state.users.filter((u) =>
      u.user.ext_name.toLowerCase().includes(value.toLowerCase())
    );
    state.users = result;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
