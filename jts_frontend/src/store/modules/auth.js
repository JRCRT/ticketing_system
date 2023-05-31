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
  async login({ commit }, { username, password }) {
    commit("app/SET_LOADING", true, { root: true });
    if (!username && !password) {
      commit(
        "app/pushAlert",
        {
          type: "danger",
          message: "The Username and Password Field is required.",
        },
        { root: true }
      );
      commit("app/SET_LOADING", false, { root: true });
      return;
    } else if (!username) {
      commit(
        "app/ADD_ALERT",
        {
          type: "danger",
          message: "The Username Field is required.",
        },
        { root: true }
      );
      commit("app/SET_LOADING", false, { root: true });
      return;
    } else if (!password) {
      commit(
        "app/ADD_ALERT",
        {
          type: "danger",
          message: "The Password Field is required.",
        },
        { root: true }
      );
      commit("app/SET_LOADING", false, { root: true });
      return;
    }
    const response = await authenticate({ username, password });
    if (!response.success) {
      commit(
        "app/ADD_ALERT",
        {
          type: "danger",
          message: response.message,
        },
        { root: true }
      );
      commit("app/SET_LOADING", false, { root: true });

      return;
    }

    commit("app/SET_LOADING", false, { root: true });
    commit("app/SET_CURRENT_PATH", "/", { root: true });
    localStorage.setItem("user", JSON.stringify(response.data));
    console.log(localStorage.getItem("user"));
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
