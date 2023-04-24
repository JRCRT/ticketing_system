import { createStore } from "vuex";

export default createStore({
  state() {
    return {
      user: "",
      currentURL: "",
    };
  },
  mutations: {
    login(state) {
      state.user = "admin";
    },
    logout(state) {
      state.user = "";
    },
    changePath(state, url) {
      state.currentURL = url;
    },
  },
});
