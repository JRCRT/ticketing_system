import axios from "./api";

const authenticate = async ({ username, password }) => {
  const response = await axios
    .post("/Auth", { username, password })
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export { authenticate };
