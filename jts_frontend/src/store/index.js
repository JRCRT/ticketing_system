import { createStore } from "vuex";
import auth from "./modules/auth";
import app from "./modules/app";
import user from "./modules/user";
import role from "./modules/role";
import department from "./modules/department";
import ticket from "./modules/ticket";
import file from "./modules/file";

export default createStore({
  modules: {
    auth,
    app,
    user,
    role,
    department,
    ticket,
    file,
  },
});
