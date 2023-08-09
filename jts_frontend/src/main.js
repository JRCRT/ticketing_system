import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import CKEditor from "@ckeditor/ckeditor5-vue";
import { VueSignalR } from "@quangdao/vue-signalr";
import interceptor from "./services/interceptor";
const URL_HUB = import.meta.env.VITE_URL_HUB;
interceptor();
const app = createApp(App);
app.use(router);
app.use(store);
app.use(CKEditor);
app.use(VueSignalR, {
  url: URL_HUB,
});
app.mount("#app");
