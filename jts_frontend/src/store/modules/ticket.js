import { tickets, ticketsByStatus } from "@/services/ticketService.js";
const state = () => ({
  tickets: [],
  pendingTickets: [],
  approvedTickets: [],
  declinedTickets: [],
});

const getter = {};

const actions = {
  async fetchAllTickets({ commit }) {
    const response = await tickets();
    commit("FETCH_TICKETS", response.data);
  },
  async fetchAllPendingTickets({ commit }) {
    const response = await ticketsByStatus("Pending");
    commit("FETCH_PENDING_TICKETS", response.data);
  },
  /*   async fetchAllApprovedTickets({ commit }) {
    const response = await ticketsByStatus("Approved");
    commit("FETCH_APPROVED_TICKETS", response.data);
  },
  async fetchAllDeclinedTickets({ commit }) {
    const response = await ticketsByStatus("Declined");
    commit("FETCH_DECLINED_TICKETS", response.data);
  }, */
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
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
