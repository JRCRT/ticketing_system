import {
  tickets,
  ticketsByStatus,
  ticketsToday,
  createTicket,
  uploadFile,
  ticketById,
  myTickets,
  ticketsForApproval,
  changeForApprovalStatus,
} from "@/services/ticketService.js";
import { TICKET_STATUS } from "@/util/constant";

const state = () => ({
  tickets: [],
  ticket: {},

  myTickets: [],
  myPendingTickets: [],
  myApprovedTickets: [],
  myDeclinedTickets: [],

  ticketsForApproval: [],
  pendingTicketsForApproval: [],
  declinedTicketsForApproval: [],
  approvedTicketsForApproval: [],
});

const getters = {
  pendingTickets: (state) => {
    console.log(state.tickets);
    return state.tickets.length == 0
      ? []
      : state.tickets.filter(
          (ticket) => ticket.ticket.status.name == TICKET_STATUS.PENDING
        );
  },

  approvedTickets: (state) => {
    return state.tickets.length == 0
      ? []
      : state.tickets.filter(
          (ticket) => ticket.ticket.status.name == TICKET_STATUS.APPROVED
        );
  },

  declinedTickets: (state) => {
    return state.tickets.length == 0
      ? []
      : state.tickets.filter(
          (ticket) => ticket.ticket.status.name == TICKET_STATUS.DECLINED
        );
  },

  myPendingTickets: (state) => {
    return state.myTickets.length == 0
      ? []
      : state.myTickets.filter(
          (ticket) => ticket.ticket.status.name == TICKET_STATUS.PENDING
        );
  },

  myApprovedTickets: (state) => {
    return state.myTickets.length == 0
      ? []
      : state.myTickets.filter(
          (ticket) => ticket.ticket.status.name == TICKET_STATUS.APPROVED
        );
  },

  myDeclinedTickets: (state) => {
    return state.myTickets.length == 0
      ? []
      : state.myTickets.filter(
          (ticket) => ticket.ticket.status.name == TICKET_STATUS.DECLINED
        );
  },

  approvedTicketsForApproval: (state) => {
    const currentUser = JSON.parse(localStorage.getItem("user"));
    return state.ticketsForApproval.length == 0
      ? []
      : state.ticketsForApproval.filter((ticket) =>
          ticket.signatories.find(
            (s) =>
              s.status.name == TICKET_STATUS.APPROVED &&
              s.user.user_id == currentUser.user_id
          )
        );
  },

  declinedTicketsForApproval: (state) => {
    const currentUser = JSON.parse(localStorage.getItem("user"));
    return state.ticketsForApproval.length == 0
      ? []
      : state.ticketsForApproval.filter((ticket) =>
          ticket.signatories.find(
            (s) =>
              s.status.name == TICKET_STATUS.DECLINED &&
              s.user.user_id == currentUser.user_id
          )
        );
  },

  pendingTicketsForApproval: (state) => {
    const currentUser = JSON.parse(localStorage.getItem("user"));
    return state.ticketsForApproval.length == 0
      ? []
      : state.ticketsForApproval.filter((ticket) =>
          ticket.signatories.find(
            (s) =>
              s.status.name == TICKET_STATUS.PENDING &&
              s.user.user_id == currentUser.user_id
          )
        );
  },
};

const actions = {
  async changeApprovalStatus({ commit, dispatch }, signatory) {
    const response = await changeForApprovalStatus(signatory);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };

    commit("app/SET_TICKET_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },

  async fetchAllTickets({ commit }) {
    const response = await tickets();
    commit("FETCH_TICKETS", response.data);
  },

  async fetchMyTickets({ commit }, userId) {
    const response = await myTickets(userId);
    commit("FETCH_MY_TICKETS", response.data);
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
    commit("app/SET_MODAL_LOADING", true, { root: true });
    const response = await ticketById(id);
    if (response.success) {
      commit("app/SET_MODAL_LOADING", false, { root: true });
    }

    commit("FETCH_TICKET", response.data);
  },

  async fetchTicketsForApproval({ commit }, param) {
    const response = await ticketsForApproval(param);
    commit("FETCH_TICKETS_FOR_APPROVAL", response.data);
  },

  //Tickets For Approval
  async fetchPendingTicketsForApproval({ commit }, param) {
    const response = await ticketsForApproval(param);
    commit("FETCH_PENDING_TICKETS_FOR_APPROVAL", response.data);
  },

  async fetchApprovedTicketsForApproval({ commit }, param) {
    const response = await ticketsForApproval(param);
    commit("FETCH_APPROVED_TICKETS_FOR_APPROVAL", response.data);
  },

  async fetchDeclinedTicketsForApproval({ commit }, param) {
    const response = await ticketsForApproval(param);
    commit("FETCH_DECLINED_TICKETS_FOR_APPROVAL", response.data);
  },

  async createTicket({ commit, dispatch }, ticket) {
    commit("app/SET_LOADING", true, { root: true });
    const response = await createTicket(ticket);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      commit("app/SET_LOADING", false, { root: true });
      commit("app/SET_NEW_TICKET_FORM", false, { root: true });
      dispatch("app/addAlert", alert, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_LOADING", false, { root: true });
    commit("app/SET_NEW_TICKET_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },
};

const mutations = {
  FETCH_TICKETS(state, value) {
    state.tickets = value;
  },

  FETCH_MY_TICKETS(state, value) {
    state.myTickets = value;
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

  FETCH_PENDING_TICKETS_FOR_APPROVAL(state, value) {
    state.pendingTicketsForApproval = value;
  },

  FETCH_DECLINED_TICKETS_FOR_APPROVAL(state, value) {
    state.declinedTicketsForApproval = value;
  },

  FETCH_APPROVED_TICKETS_FOR_APPROVAL(state, value) {
    state.approvedTicketsForApproval = value;
  },

  FETCH_TICKETS_FOR_APPROVAL(state, value) {
    state.ticketsForApproval = value;
  },
  REMOVE_PENDING_TICKETS_FOR_APPROVAL(state, value) {
    const ticketId = value.ticket.ticket_id;
    const newValue = state.pendingTicketsForApproval.filter(
      (t) => t.ticket.ticket_id != ticketId
    );
    state.pendingTicketsForApproval = newValue;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
