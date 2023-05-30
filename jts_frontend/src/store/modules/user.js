import { users, createUser } from "@/services/userService.js";
import { User } from "@/models/User";
const state = () => ({
  users: [],
});

const getter = {
  users: (state) => {
    return state.user;
  },
};

const actions = {
  async loadUsers({ commit }) {
    const response = await users();
    commit("populateUsers", response.data);
  },

  async createUser(
    { commit },
    {
      username,
      password,
      firstname,
      middlename,
      lastname,
      email,
      role_id,
      department_id,
    }
  ) {
    commit("app/setLoading", true, { root: true });
    const response = await createUser(User);
    if (!response.success) {
      commit(
        "app/pushAlert",
        {
          type: "danger",
          message: response.message,
        },
        { root: true }
      );
      commit("app/setLoading", false, { root: true });
      return;
    }
    commit("addUser", response.data);
    commit("app/setLoading", false, { root: true });
    commit(
      "app/pushAlert",
      {
        type: "success",
        message: response.message,
      },
      { root: true }
    );
  },
};

const mutations = {
  populateUsers(state, value) {
    state.users = value;
  },
  addUser(state, value) {
    state.users.push(value);
  },
};

export default {
  namespaced: true,
  state,
  getter,
  actions,
  mutations,
};
