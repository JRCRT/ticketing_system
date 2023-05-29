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

    commit("app/setLoading", false, { root: true });
    commit("app/setCurrentUrl", "/", { root: true });
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
