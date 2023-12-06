import { createApp } from "vue";
import "./style.css";
import "material-design-icons-iconfont/dist/material-design-icons.css";
import "@mdi/font/css/materialdesignicons.css";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import { VueSignalR } from "@quangdao/vue-signalr";
import interceptor from "./services/interceptor";
import "vuetify/styles";
import { createVuetify } from "vuetify";
import { VDataTableServer } from "vuetify/lib/components/index.mjs";

const vuetify = createVuetify({
  components: {
    VDataTableServer,
  },
});
const URL_HUB = import.meta.env.VITE_URL_HUB;
interceptor();
const app = createApp(App);
app.use(vuetify);
app.use(router);
app.use(store);
app.use(VueSignalR, {
  url: URL_HUB,
});
app.mount("#app");
