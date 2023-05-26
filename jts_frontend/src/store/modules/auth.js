import { authenticate } from "@/services/authService.js";
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
    }
    console.log(response);
    commit("setCurrentUser", response.data);
    commit("app/setLoading", false, { root: true });
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
