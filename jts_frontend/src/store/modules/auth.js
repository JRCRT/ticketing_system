const state = () => ({
  user: null,
});

const getter = {
  loggedInUser: (state) => {
    return state.user;
  },
};

const actions = {};

const mutations = {
  setLoginUser(state, user) {
    state.user = user;
  },
};
