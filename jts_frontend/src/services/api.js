import axios from "axios";
const BASE_URL = import.meta.env.VITE_BASE_URL;
const currentUser = JSON.parse(localStorage.getItem("user"));
export default axios.create({
  baseURL: BASE_URL,
  headers: {
    "Authorization": `Bearer ${currentUser.access_token}`
  }, 
});

export const axiosPrivate = axios.create({
  baseURL: BASE_URL,
  headers: {
    "Content-Type": "multipart/form-data",
  },
  withCredentials: true,
});
