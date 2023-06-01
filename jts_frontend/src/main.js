import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import CKEditor from "@ckeditor/ckeditor5-vue";
import { VueSignalR } from "@quangdao/vue-signalr";
const BASE_URL = import.meta.env.VITE_BASE_URL_HUB;

const app = createApp(App);
app.use(router);
app.use(store);
app.use(CKEditor);
app.use(VueSignalR, {
  url: "http://localhost:5148/user-hub",
});
app.mount("#app");
