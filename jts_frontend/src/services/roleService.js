import axios from "./api";

const roles = async () => {
  const response = await axios
    .get("/Role/GetAllRoles")
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export { roles };
