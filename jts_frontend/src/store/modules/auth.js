import { authenticate } from "@/services/authService.js";
import router from "@/router";
const state = () => ({
  user: null,
});

const getter = {
  currentUser: (state) => {
    return state.user;
  },
};

const actions = {
  async login({ commit, dispatch }, { username, password }) {
    commit("app/SET_LOADING", true, { root: true });
    if (!username && !password) {
      const alert = {
        type: "danger",
        message: "The Username and Password Field is required.",
      };
      dispatch("app/addAlert", alert, { root: true });
      commit("app/SET_LOADING", false, { root: true });
      return;
    } else if (!username) {
      const alert = {
        type: "danger",
        message: "The Username Field is required.",
      };
      dispatch("app/addAlert", alert, { root: true });
      commit("app/SET_LOADING", false, { root: true });
      return;
    } else if (!password) {
      const alert = {
        type: "danger",
        message: "The Password Field is required.",
      };
      dispatch("app/addAlert", alert, { root: true });
      commit("app/SET_LOADING", false, { root: true });
      return;
    }
    const response = await authenticate({ username, password });
    if (!response.success) {
      const alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      commit("app/SET_LOADING", false, { root: true });
      return;
    }

    commit("app/SET_LOADING", false, { root: true });
    commit("app/SET_CURRENT_URL", "/", { root: true });
    localStorage.setItem("user", JSON.stringify(response.data));
    router.replace({ name: "Dashboard" });
  },

  logout() {
    localStorage.removeItem("user");
  },
};

const mutations = {};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
