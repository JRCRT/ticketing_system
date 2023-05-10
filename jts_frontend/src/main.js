import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import CKEditor from "@ckeditor/ckeditor5-vue";
import axios from 'axios'
import VueAxios from 'vue-axios'


const app = createApp(App);
app.use(router);
app.use(store);
app.use(CKEditor);
app.use(VueAxios, axios);
app.provide('axios', app.config.globalProperties.axios);
app.mount("#app");
