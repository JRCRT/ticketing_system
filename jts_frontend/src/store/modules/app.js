const state = () => ({
  isLoading: false,
  alerts: [],
  currentUrl: null,
  currentUser: {},
  isNewUserFormOpen: false,
  isNewTicketFormOpen: false,
  isDeclineReasonModalOpen: false,
  isLoggingIn: false,
  selectedTicket: {},
  selectedUser: {},
  isTicketFormOpen: false,
  isUserFormOpen: false,
  isModalLoading: false,
  isProcessing: false,
  signatory: {},
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
  SET_NEW_USER_FORM(state, value) {
    state.isNewUserFormOpen = value;
  },
  SET_NEW_TICKET_FORM(state, value) {
    state.isNewTicketFormOpen = value;
  },
  SET_LOGIN_LOADING(state, value) {
    state.isLoggingIn = value;
  },
  SET_SELECTED_TICKET(state, value) {
    state.selectedTicket = value;
  },
  SET_SELECTED_USER(state, value) {
    state.selectedUser = value;
  },
  SET_TICKET_FORM(state, value) {
    state.isTicketFormOpen = value;
  },
  SET_USER_FORM(state, value) {
    state.isUserFormOpen = value;
  },
  SET_MODAL_LOADING(state, value) {
    state.isModalLoading = value;
  },
  SET_PROCESSING(state, value) {
    state.isProcessing = value;
  },
  SET_CURRENT_USER(state, value) {
    state.currentUser = value;
  },
  SET_DECLINE_REASON_MODAL(state, value) {
    state.isDeclineReasonModalOpen = value;
  },
  SET_SIGNATORY(state, value) {
    state.signatory = value;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  mutations,
  actions,
};
