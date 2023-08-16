import axios from "./api";

const getImage = async (fileName) => {
  const response = await axios
    .get(`/File/${fileName}`)
    .then((res) => {
      return res;
    })
    .catch((err) => {
      return err.response;
    });
  return response;
};

export { getImage };
