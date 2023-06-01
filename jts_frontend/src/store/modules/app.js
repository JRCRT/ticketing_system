const state = () => ({
  isLoading: false,
  alerts: [],
  currentUrl: null,
  isUserFormOpen: false,
});

const getter = {};

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
  SET_USER_FORM(state, value) {
    state.isUserFormOpen = value;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  mutations,
  actions,
};
