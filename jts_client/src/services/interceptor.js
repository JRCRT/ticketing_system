import axios from "./api";
import router from "../router";
import store from "../store";

export default function setup() {
  axios.interceptors.response.use(
    (response) => {
      return response;
    },
    (error) => {
      /* 
      store.commit("app/SET_MODAL_LOADING", false);
      store.commit("app/SET_LOADING", false);
      router.replace({ name: "ErrorPage" }); */
    }
  );
}
