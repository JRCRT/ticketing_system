import axios from "./api";

const departments = async () => {
  const response = await axios
    .get("/Department/GetAllDepartments")
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export { departments };
