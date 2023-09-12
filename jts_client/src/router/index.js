import { createRouter, createWebHistory } from "vue-router";
import { nextTick } from "vue";
import store from "@/store";
import Login from "@/views/Login.vue";
import Dashboard from "@/views/Dashboard.vue";
import MyTicket from "@/views/my_ticket/Ticket.vue";
import Ticket from "@/views/all_ticket/Ticket.vue";
import TicketForApproval from "@/views/ticket_for_approval/Ticket.vue";
import RoleManager from "@/views/RoleManager.vue";
import User from "@/views/User.vue";
import ErrorPage from "@/views/ErrorPage.vue";

const routes = [
  {
    path: "/Login",
    name: "Login",
    component: Login,
    meta: {
      title: "Login",
    },
    beforeEnter: (to, from, next) => {
      const user = localStorage.getItem("user");
      if (user) {
        router.replace({ name: "Dashboard" });
      } else {
        store.commit("app/SET_HIDE_NAVBAR", true);
        next();
      }
    },
  },
  {
    path: "/",
    name: "Dashboard",
    component: Dashboard,
    meta: {
      title: "Dashboard",
      authRequired: true,
    },
  },

  {
    path: "/Id/:ticketId",
    name: "DashboardTicketById",
    component: Dashboard,
    meta: {
      title: "Dashboard",
      authRequired: true,
    },
    beforeEnter: (to, from, next) => {
      store.commit("app/SET_TICKET_FORM", true);
      next();
    },
  },

  {
    path: "/Ticket/:status",
    name: "Ticket",
    component: Ticket,
    meta: {
      title: "All Tickets",
      authRequired: true,
    },
  },
  {
    path: "/Ticket/:status/Id/:ticketId",
    name: "TicketById",
    component: Ticket,
    meta: {
      title: "All Tickets",
      authRequired: true,
    },
    beforeEnter: (to, from, next) => {
      store.commit("app/SET_TICKET_FORM", true);
      next();
    },
  },

  {
    path: "/MyTicket/:status",
    name: "MyTicket",
    component: MyTicket,
    meta: {
      title: "My Ticket",
      authRequired: true,
    },
  },

  {
    path: "/MyTicket/:status",
    name: "MyTicketSearch",
    component: MyTicket,
    meta: {
      title: "My Ticket",
      authRequired: true,
    },
  },
  {
    path: "/MyTicket/:status/Id/:ticketId",
    name: "MyTicketById",
    component: MyTicket,
    meta: {
      title: "My Ticket",
      authRequired: true,
    },
    beforeEnter: (to, from, next) => {
      store.commit("app/SET_TICKET_FORM", true);
      next();
    },
  },

  {
    path: "/TicketForApproval/:status",
    name: "TicketForApproval",
    component: TicketForApproval,
    meta: {
      title: "Ticket For Approval",
      authRequired: true,
    },
  },

  {
    path: "/TicketForApproval/:status/Id/:ticketId",
    name: "TicketForApprovalById",
    component: TicketForApproval,
    meta: {
      title: "Ticket For Approval",
      authRequired: true,
    },
    beforeEnter: (to, from, next) => {
      store.commit("app/SET_TICKET_FORM", true);
      next();
    },
  },

  {
    path: "/User",
    name: "User",
    component: User,
    meta: {
      title: "User",
      adminRequired: true,
    },
  },

  {
    path: "/User/Id/:userId",
    name: "UserById",
    component: User,
    meta: {
      title: "User",
      adminRequired: true,
    },
    beforeEnter: (to, from, next) => {
      store.commit("app/SET_USER_FORM", true);
      next();
    },
  },

  {
    path: "/RoleManager",
    name: "RoleManager",
    component: RoleManager,
    meta: {
      title: "Role Manager",
      adminRequired: true,
    },
  },
  {
    path: "/ErrorPage",
    name: "ErrorPage",
    component: ErrorPage,
    meta: {
      title: "Error Page",
    },
    beforeEnter: (to, from, next) => {
      store.commit("app/SET_HIDE_NAVBAR", true);
      next();
    },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    return { left: 0, top: 0, behavior: "smooth" };
  },
});

router.afterEach((to, from) => {
  nextTick(() => {
    document.title = to.meta.title
      ? `${to.meta.title} | ${import.meta.env.VITE_APP_TITLE}`
      : import.meta.env.VITE_APP_TITLE;
  });
});

router.beforeEach((to, from, next) => {
  const user = localStorage.getItem("user");
  if (to.matched.some((route) => route.meta.authRequired)) {
    if (!user) next({ name: "Login" });
    else {
      next();
      store.commit("app/SET_HIDE_NAVBAR", false);
    }
  } else if (to.matched.some((route) => route.meta.adminRequired)) {
    if (!user) next({ name: "Login" });
    else {
      next();
      store.commit("app/SET_HIDE_NAVBAR", false);
    }
  } else {
    next();
  }
});

export default router;
