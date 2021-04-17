import * as types from "./action.types";

export const LOAD_ITEM = () => {
  return { type: types.LOAD_ITEM };
};

export const GET_ITEMS = (payload) => {
  return { type: types.GET_ITEMS, payload };
};

export const REMOVE_ITEM = (payload) => {
  return { type: types.REMOVE_ITEM, payload };
};
