import axios from "./api";

const jobTitles = async () => {
  const response = await axios
    .get("/JobTitle/GetAllJobTitles")
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export { jobTitles };