import { createRouter, createWebHistory } from "vue-router";
import { nextTick } from "vue";
import Login from "@/views/Login.vue";
import Dashboard from "@/views/Dashboard.vue";
import Ticket from "@/views/Ticket.vue";

const APP_TITLE = "JTS - Ticketing System";
const routes = [
    {
        path: "/",
        name: "Login",
        component: Login,
        meta:{
            title: "Login"
        }
    },
    {
        path: "/dashboard",
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
       document.title = to.meta.title ? `${to.meta.title} - ${APP_TITLE}` : APP_TITLE
    })
})


export default router;


