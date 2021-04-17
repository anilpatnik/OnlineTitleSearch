import { searchApi } from "../api";
import * as funcs from "./action.functions";

export const getSearchResults = keywords => {
  return async dispatch => {
    dispatch(funcs.fetchSubmitted());
    const payload = await searchApi.getSearchResults(keywords);
    dispatch(funcs.fetchCompleted(payload));
  };
};
