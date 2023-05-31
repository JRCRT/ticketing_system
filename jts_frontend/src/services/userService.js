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

const createUser = async (user) => {
  console.log(user);
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

export { users, createUser };
