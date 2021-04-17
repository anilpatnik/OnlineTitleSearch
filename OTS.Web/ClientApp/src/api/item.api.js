import { axios, constants } from "../common";

const url = `${constants.API_URL}/genres`;

export const getItems = async () => {
  const response = await axios.get(url);
  return response.json();
};

export const addItem = async (payload) => {
  const response = await axios.post(url, payload);
  return response.json();
};

export const removeItem = async (payload) => {
  const response = await axios.delele(`${url}/${payload}`);
  return response.json();
};
