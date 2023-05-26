const state = () => ({
  isLoading: false,
  alerts: [],
  currentUrl: null,
});

const getter = {
  currentUrl: (state) => {
    return state.currentUrl;
  },
};
const actions = {
  changeUrl({ commit }, value) {
    commit("setCurrentUrl", value);
  },
  addAlert({ commit }, value) {
    commit("pushAlert", value);
  },
  removeAlert({ commit }, index) {
    commit("popAlert", index);
  },
};

const mutations = {
  setLoading(state, value) {
    state.isLoading = value;
  },
  setCurrentUrl(state, value) {
    state.currentUrl = value;
  },
  pushAlert(state, value) {
    state.alerts.push(value);
  },
  popAlert(state, index) {
    state.alerts.splice(index, 1);
  },
};

export default {
  namespaced: true,
  state,
  getter,
  mutations,
  actions,
};
