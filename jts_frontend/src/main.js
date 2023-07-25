import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import CKEditor from "@ckeditor/ckeditor5-vue";
import { VueSignalR } from "@quangdao/vue-signalr";
const URL_HUB = import.meta.env.VITE_URL_HUB;

const app = createApp(App);
app.use(router);
app.use(store);
app.use(CKEditor);
app.use(VueSignalR, {
  url: URL_HUB,
});
app.mount("#app");
