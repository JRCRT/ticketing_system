import axios from "./api";
const users = async () => {
  const response = await axios
    .get("/User/GetAllUsers")
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

export { users, createUser, usersByRole, userById, updateUser };
