import axios from "./api";
const users = async (param) => {
  const response = await axios
    .post("/User/GetAllUsers", param)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const updateUser = async (user) => {
  const response = await axios
    .post("/User/UpdateUser", user)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const userById = async (id) => {
  const response = await axios
    .post(`/User/GetUserById?user_id=${id}`)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const usersByRole = async (role) => {
  const response = await axios
    .get(`/User/GetUsersByRole?role=${role}`)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });

  return response;
};

const checkers = async (param) => {
  const response = await axios
    .post("/User/GetCheckers", param)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });

  return response;
};

const relatedParties = async (currentUserId) => {
  const response = await axios
    .get(`/User/GetRelatedParties?currentUserId=${currentUserId}`)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });

  return response;
};

const createUser = async (user) => {
  const response = await axios
    .post("/User/CreateUser", user)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export {
  users,
  createUser,
  usersByRole,
  userById,
  updateUser,
  checkers,
  relatedParties,
};
