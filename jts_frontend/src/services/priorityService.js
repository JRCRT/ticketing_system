import axios from "./api";

const priorities = async () => {
  const response = await axios
    .get("/Priority/GetAllPriorities")
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export { priorities };
