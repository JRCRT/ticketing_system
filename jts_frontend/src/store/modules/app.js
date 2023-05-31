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
  addAlert({ commit, state }, value) {
    commit("ADD_ALERT", value);

    setTimeout(() => {
      commit("REMOVE_ALERT", state.alerts[0]);
    }, 3000);
  },
};

const mutations = {
  SET_LOADING(state, value) {
    state.isLoading = value;
  },
  SET_CURRENT_URL(state, value) {
    state.currentUrl = value;
  },
  ADD_ALERT(state, value) {
    state.alerts.push(value);
  },
  REMOVE_ALERT(state, index) {
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
