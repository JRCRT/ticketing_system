import {
  tickets,
  ticketsByStatus,
  ticketsToday,
  createTicket,
  uploadFile,
  ticketById,
} from "@/services/ticketService.js";
import { TICKET_STATUS } from "@/util/constant";
const state = () => ({
  tickets: [],
  pendingTickets: [],
  approvedTickets: [],
  declinedTickets: [],
  todaysTickets: [],
  ticket: {},
});

const getter = {};

const actions = {
  async fetchAllTickets({ commit }) {
    const response = await tickets();
    commit("FETCH_TICKETS", response.data);
  },
  async fetchAllPendingTickets({ commit }) {
    const response = await ticketsByStatus(TICKET_STATUS.PENDING);
    commit("FETCH_PENDING_TICKETS", response.data);
  },
  async fetchAllApprovedTickets({ commit }) {
    const response = await ticketsByStatus(TICKET_STATUS.APPROVED);
    commit("FETCH_APPROVED_TICKETS", response.data);
  },
  async fetchAllDeclinedTickets({ commit }) {
    const response = await ticketsByStatus(TICKET_STATUS.DECLINED);
    commit("FETCH_DECLINED_TICKETS", response.data);
  },
  async fetchAllTodaysTickets({ commit }) {
    const response = await ticketsToday();
    commit("FETCH_TODAYS_TICKETS", response.data);
  },

  async fetchTicket({ commit }, id) {
    const response = await ticketById(id);
    commit("FETCH_TICKET", response.data);
  },

  async createTicket({ commit, dispatch }, ticket) {
    commit("app/SET_LOADING", true, { root: true });
    const response = await createTicket(ticket);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      commit("app/SET_LOADING", false, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_LOADING", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },

  async uploadFile({ commit, dispatch }, file) {
    commit("app/SET_LOADING", true, { root: true });
    const response = await uploadFile(file);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      commit("app/SET_LOADING", false, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_LOADING", false, { root: true });
    commit("app/SET_USER_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },
};

const mutations = {
  FETCH_TICKETS(state, value) {
    state.tickets = value;
  },
  FETCH_PENDING_TICKETS(state, value) {
    state.pendingTickets = value;
  },
  FETCH_APPROVED_TICKETS(state, value) {
    state.approvedTickets = value;
  },
  FETCH_DECLINED_TICKETS(state, value) {
    state.declinedTickets = value;
  },
  FETCH_TODAYS_TICKETS(state, value) {
    state.todaysTickets = value;
  },

  FETCH_TICKET(state, value) {
    state.ticket = value;
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
