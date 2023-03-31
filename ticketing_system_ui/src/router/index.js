import { createRouter, createWebHistory } from "vue-router";
import { nextTick } from "vue";
import Login from "@/views/Login.vue";
import Dashboard from "@/views/Dashboard.vue";
import Ticket from "@/views/Ticket.vue";

const routes = [
    {
        path: "/login",
        name: "Login",
        component: Login,
        meta:{
            title: "Login"
        }
    },
    {
        path: "/",
        name: "Dashboard",
        component: Dashboard,
        meta:{
            title: "Dashboard"
        }
    },
    {
        path: "/ticket",
        name: "Ticket",
        component: Ticket,
        meta:{
            title: "Ticket"
        }
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes,
    scrollBehavior(to, from, savedPosition) {
    return { left: 0, top: 0, behavior: "smooth" };
    },
});

router.afterEach((to, from)=>{
    nextTick(()=>{
       document.title = to.meta.title ? `${to.meta.title} | ${import.meta.env.VITE_APP_TITLE}` : import.meta.env.VITE_APP_TITLE
    })
})


export default router;


