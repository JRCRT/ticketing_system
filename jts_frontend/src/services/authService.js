import axios from "./api";

const authenticate = async ({ username, password }) => {
  const response = await axios
    .post("/Auth", { username, password })
    .then((res) => {
      return res;
    })
    .catch((err) => {
      return err;
    });
  return response;
};

export { authenticate };
