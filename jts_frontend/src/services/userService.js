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

const createUser = async (User) => {
  const response = await axios
    .post("/User/CreateUser", User)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export { users, createUser };
