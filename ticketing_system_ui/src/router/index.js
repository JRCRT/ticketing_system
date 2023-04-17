import { createRouter, createWebHistory } from "vue-router";
import { nextTick } from "vue";
import Login from "@/views/Login.vue";
import Dashboard from "@/views/Dashboard.vue";
import Ticket from "@/views/ticket/Ticket.vue";
const user = "";
const routes = [
  {
    path: "/login",
    name: "Login",
    component: Login,
    meta: {
      title: "Login",
    },
    beforeEnter: (to, from, next) => {
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
    path: "/ticket",
    name: "Ticket",
    component: Ticket,
    meta: {
      title: "Ticket",
      authRequired: true,
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
  if (to.matched.some((route) => route.meta.authRequired)) {
    if (!user) next({ name: "Login" });
    else next();
  } else if (to.matched.some((route) => route.meta.adminRequired)) {
  } else {
    next();
  }
});

export default router;
