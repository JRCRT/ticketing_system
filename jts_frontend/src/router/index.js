import { createRouter, createWebHistory } from "vue-router";
import { nextTick } from "vue";
import store from "@/store";
import Login from "@/views/Login.vue";
import Dashboard from "@/views/Dashboard.vue";
import Ticket from "@/views/my_ticket/Ticket.vue";
import User from "@/views/User.vue";

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
    path: "/Ticket/:status",
    name: "Ticket",
    component: Ticket,
    meta: {
      title: "Ticket",
      authRequired: true,
    },
  },
  {
    path: "/Ticket/:status/Id/:ticketId",
    name: "TicketById",
    component: Ticket,
    meta: {
      title: "Ticket",
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
    else next();
  } else if (to.matched.some((route) => route.meta.adminRequired)) {
    if (!user) next({ name: "Login" });
    else next();
  } else {
    next();
  }
});

export default router;
