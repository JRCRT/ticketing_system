import {
  tickets,
  ticketsByStatus,
  ticketsToday,
  createTicket,
  uploadFile,
  ticketById,
  myTickets,
  ticketsForApproval,
  approveTicket,
  rejectTicket,
  doneTicket,
} from "@/services/ticketService.js";
import { TICKET_STATUS } from "@/util/constant";

const state = () => ({
  tickets: [],
  ticket: {},

  allTickets: [],

  myTickets: [],

  ticketsForApproval: [],
});

const getters = {};

const actions = {
  async approveTicket({ commit, dispatch }, signatory) {
    commit("app/SET_PROCESSING", true, { root: true });
    const response = await approveTicket(signatory);
    var alert;
    if (!response.success) {
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_PROCESSING", false, { root: true });
    commit("app/SET_TICKET_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },

  async rejectTicket({ commit, dispatch }, signatory) {
    commit("app/SET_PROCESSING", true, { root: true });
    const response = await rejectTicket(signatory);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_PROCESSING", false, { root: true });
    commit("app/SET_REJECTION_REASON_MODAL", false, { root: true });
    commit("app/SET_TICKET_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },

  async doneTicket({ commit, dispatch }, request) {
    commit("app/SET_PROCESSING", true, { root: true });
    const response = await doneTicket(request);
    var alert;
    if (!response.success) {
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      return;
    }

    alert = { type: "success", message: response.message };
    commit("app/SET_PROCESSING", false, { root: true });
    commit("app/SET_TICKET_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },

  async fetchAllTodaysTickets({ commit }, userId) {
    const response = await ticketsToday(userId);
    commit("FETCH_TODAYS_TICKETS", response.data);
  },

  async fetchTicket({ commit }, id) {
    commit("app/SET_MODAL_LOADING", true, { root: true });
    const response = await ticketById(id);
    if (response.success) {
      commit("app/SET_MODAL_LOADING", false, { root: true });
    }
    commit("FETCH_TICKET", response.data);
  },

  async fetchAllTickets({ commit }, param) {
    const response = await ticketsByStatus(param);
    commit("FETCH_ALL_TICKETS", response.data);
  },

  //My Tickets
  async fetchMyTickets({ commit }, param) {
    const response = await myTickets(param);
    commit("FETCH_MY_TICKETS", response.data);
  },

  //Tickets For Approval
  async fetchTicketsForApproval({ commit }, param) {
    const response = await ticketsForApproval(param);
    commit("FETCH_TICKETS_FOR_APPROVAL", response.data);
  },

  async createTicket({ commit, dispatch }, ticket) {
    commit("app/SET_PROCESSING", true, { root: true });
    const response = await createTicket(ticket);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      commit("app/SET_PROCESSING", false, { root: true });
      commit("app/SET_NEW_TICKET_FORM", false, { root: true });
      dispatch("app/addAlert", alert, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_PROCESSING", false, { root: true });
    commit("app/SET_NEW_TICKET_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },
};

const mutations = {
  FETCH_TICKET(state, value) {
    state.ticket = value;
  },

  FETCH_TODAYS_TICKETS(state, value) {
    state.todaysTickets = value;
  },

  //All Tickets
  FETCH_ALL_TICKETS(state, value) {
    state.allTickets = value;
  },

  //My Tickets
  FETCH_MY_TICKETS(state, value) {
    state.myTickets = value;
  },

  //Tickets For Approval
  FETCH_TICKETS_FOR_APPROVAL(state, value) {
    state.ticketsForApproval = value;
  },

  REMOVE_PENDING_TICKET_FOR_APPROVAL(state, value) {
    const ticketId = value.ticket.ticket_id;
    const newValue = state.pendingTicketsForApproval.filter(
      (t) => t.ticket.ticket_id != ticketId
    );
    state.pendingTicketsForApproval = newValue;
  },
  REMOVE_DONE_TICKET(state, value) {
    const ticketId = value.ticket.ticket_id;
    const newValue = state.allApprovedTickets.filter(
      (t) => t.ticket.ticket_id != ticketId
    );
    state.allApprovedTickets = newValue;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
