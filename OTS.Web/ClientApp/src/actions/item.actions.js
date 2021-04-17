import { itemApi } from "../api";
import * as funcs from "./action.functions";

export const getItems = () => {
  return async (dispatch) => {
    dispatch(funcs.LOAD_ITEM());
    const payload = await itemApi.getItems();
    setTimeout(() => dispatch(funcs.GET_ITEMS(payload)), 1000);
  };
};

export const addItem = (payload) => {
  return async (dispatch) => {
    await itemApi.addItem(payload);
    console.log("addItem complete");
  };
};

export const removeItem = (payload) => {
  return async (dispatch) => {
    await itemApi.removeItem(payload);
    dispatch(getItems());
  };
};
