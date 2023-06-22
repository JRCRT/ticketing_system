import {
  tickets,
  ticketsByStatus,
  ticketsToday,
} from "@/services/ticketService.js";
import { TICKET_STATUS } from "@/util/constant";
const state = () => ({
  tickets: [],
  pendingTickets: [],
  approvedTickets: [],
  declinedTickets: [],
  todaysTickets: [],
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
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
