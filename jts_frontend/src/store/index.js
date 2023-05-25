import { createStore } from "vuex";
import { authenticate } from "@/services/authService.js";

export default createStore({
  state() {
    return {
      user: "",
      currentURL: "",
    };
  },
  actions: {
    async login({ commit }, { username, password }) {
      const response = await authenticate({ username, password });
      console.log(response);
      commit("setLoggedInUser", response.data);
    },
  },
  mutations: {
    setLoggedInUser(state, res) {
      state.user = res.user;
      localStorage.setItem("access_token", res.access_token);
    },
    removeLoggedInUser(state) {
      state.user = "";
    },
    changePath(state, url) {
      state.currentURL = url;
    },
    login(state) {
      const response = authenticate();
      console.log(response);
    },
  },
});
