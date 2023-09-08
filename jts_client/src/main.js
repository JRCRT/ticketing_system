import { createApp } from "vue";
import "./style.css";
import "material-design-icons-iconfont/dist/material-design-icons.css";
import "@mdi/font/css/materialdesignicons.css";
//import "@/stylesheet/content-style.css";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import CKEditor from "@ckeditor/ckeditor5-vue";
import { VueSignalR } from "@quangdao/vue-signalr";
import interceptor from "./services/interceptor";
import "vuetify/styles";
import { createVuetify } from "vuetify";
import * as directives from "vuetify/directives";
import { VDataTableServer } from "vuetify/labs/VDataTable";
import { VTextField } from "vuetify/lib/components/index.mjs";

const vuetify = createVuetify({
  components: {
    VDataTableServer,
    VTextField,
  },
  directives,
  theme: {
    defaultTheme: "light",
    themes: {
      light: {
        colors: {
          primary: "#6200ee",
          secondary: "#03dac6",
          accent: "#ff4081",
        },
      },
    },
  },
});
const URL_HUB = import.meta.env.VITE_URL_HUB;
interceptor();
const app = createApp(App);
app.use(vuetify);
app.use(router);
app.use(store);
app.use(CKEditor);
app.use(VueSignalR, {
  url: URL_HUB,
});
app.mount("#app");
