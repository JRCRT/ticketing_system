import axios from "./api";

const getImage = async (fileName) => {
  const response = await axios
    .get(`/File/${fileName}`)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export { getImage };
