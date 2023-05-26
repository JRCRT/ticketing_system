import { createStore } from "vuex";
import auth from "./modules/auth";
import app from "./modules/app";

export default createStore({
  modules: {
    auth,
    app,
  },
});
