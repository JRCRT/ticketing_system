import { createStore } from "vuex";
import auth from "./modules/auth";
import app from "./modules/app";
import user from "./modules/user";
import role from "./modules/role";

export default createStore({
  modules: {
    auth,
    app,
    user,
    role,
  },
});
