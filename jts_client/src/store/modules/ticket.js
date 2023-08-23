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
  declineTicket,
} from "@/services/ticketService.js";
import { TICKET_STATUS } from "@/util/constant";

const state = () => ({
  tickets: [],
  ticket: {},

  allPendingTickets: [],
  allApprovedTickets: [],
  allDeclinedTickets: [],
  allDoneTickets: [],

  myTickets: [],
  myPendingTickets: [],
  myApprovedTickets: [],
  myDeclinedTickets: [],
  myDoneTickets: [],

  ticketsForApproval: [],
  pendingTicketsForApproval: [],
  declinedTicketsForApproval: [],
  approvedTicketsForApproval: [],
  doneTicketsForApproval: [],
});

const getters = {};

const actions = {
  async approveTicket({ commit, dispatch }, signatory) {
    commit("app/SET_PROCESSING", true, { root: true });
    const response = await approveTicket(signatory);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_PROCESSING", false, { root: true });
    commit("app/SET_TICKET_FORM", false, { root: true });
    dispatch("app/addAlert", alert, { root: true });
  },

  async declineTicket({ commit, dispatch }, signatory) {
    commit("app/SET_PROCESSING", true, { root: true });
    const response = await declineTicket(signatory);
    var alert;
    if (!response.success) {
      console.log(response);
      alert = { type: "danger", message: response.message };
      dispatch("app/addAlert", alert, { root: true });
      return;
    }
    alert = { type: "success", message: response.message };
    commit("app/SET_PROCESSING", false, { root: true });
    commit("app/SET_DECLINE_REASON_MODAL", false, { root: true });
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

  //All Tickets
  async fetchAllPendingTickets({ commit }) {
    const response = await ticketsByStatus(TICKET_STATUS.PENDING);
    commit("FETCH_ALL_PENDING_TICKETS", response.data);
  },
  async fetchAllApprovedTickets({ commit }) {
    const response = await ticketsByStatus(TICKET_STATUS.APPROVED);
    commit("FETCH_ALL_APPROVED_TICKETS", response.data);
  },
  async fetchAllDeclinedTickets({ commit }) {
    const response = await ticketsByStatus(TICKET_STATUS.DECLINED);
    commit("FETCH_ALL_DECLINED_TICKETS", response.data);
  },
  async fetchAllDoneTickets({ commit }) {
    const response = await ticketsByStatus(TICKET_STATUS.DONE);
    commit("FETCH_ALL_DONE_TICKETS", response.data);
  },

  //My Tickets
  async fetchMyPendingTickets({ commit }, param) {
    const response = await myTickets(param);
    commit("FETCH_MY_PENDING_TICKETS", response.data);
  },
  async fetchMyApprovedTickets({ commit }, param) {
    const response = await myTickets(param);
    commit("FETCH_MY_APPROVED_TICKETS", response.data);
  },
  async fetchMyDeclinedTickets({ commit }, param) {
    const response = await myTickets(param);
    commit("FETCH_MY_DECLINED_TICKETS", response.data);
  },
  async fetchMyDoneTickets({ commit }, param) {
    const response = await myTickets(param);
    commit("FETCH_MY_DONE_TICKETS", response.data);
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
  async fetchDoneTicketsForApproval({ commit }, param) {
    const response = await ticketsForApproval(param);
    commit("FETCH_DONE_TICKETS_FOR_APPROVAL", response.data);
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
  FETCH_ALL_PENDING_TICKETS(state, value) {
    state.allPendingTickets = value;
  },
  FETCH_ALL_APPROVED_TICKETS(state, value) {
    state.allApprovedTickets = value;
  },
  FETCH_ALL_DECLINED_TICKETS(state, value) {
    state.allDeclinedTickets = value;
  },
  FETCH_ALL_DONE_TICKETS(state, value) {
    state.allDoneTickets = value;
  },

  //My Tickets
  FETCH_MY_PENDING_TICKETS(state, value) {
    state.myPendingTickets = value;
  },
  FETCH_MY_APPROVED_TICKETS(state, value) {
    state.myApprovedTickets = value;
  },
  FETCH_MY_DECLINED_TICKETS(state, value) {
    state.myDeclinedTickets = value;
  },
  FETCH_MY_DONE_TICKETS(state, value) {
    state.myDoneTickets = value;
  },
  ADD_MY_PENDING_TICKETS(state, value) {
    state.myPendingTickets.push(value);
  },

  //Tickets For Approval
  FETCH_PENDING_TICKETS_FOR_APPROVAL(state, value) {
    state.pendingTicketsForApproval = value;
  },
  FETCH_DECLINED_TICKETS_FOR_APPROVAL(state, value) {
    state.declinedTicketsForApproval = value;
  },
  FETCH_APPROVED_TICKETS_FOR_APPROVAL(state, value) {
    state.approvedTicketsForApproval = value;
  },
  FETCH_DONE_TICKETS_FOR_APPROVAL(state, value) {
    state.doneTicketsForApproval = value;
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