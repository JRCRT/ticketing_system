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
    commit("app/setLoading", true, { root: true });
    const response = await authenticate({ username, password });
    if (!response.success) {
      commit(
        "app/pushAlert",
        {
          type: "danger",
          message: response.message,
        },
        { root: true }
      );
      return;
    }
    commit("setCurrentUser", response.data);
    commit("app/setLoading", false, { root: true });
    commit("app/setCurrentUrl", "/", { root: true });
    router.push({ name: "Dashboard" });
  },

  logout({ commit }) {
    commit("removeCurrentUser");
    localStorage.removeItem("access_token");
  },
};

const mutations = {
  setCurrentUser(state, value) {
    state.user = value;
  },
  removeCurrentUser(state) {
    state.user = null;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
