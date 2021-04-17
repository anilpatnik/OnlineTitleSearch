import { constants } from "../common";

const initialState = {
  items: [],
  loading: false
};

export const searchReducer = (state = initialState, { type, payload }) => {
  switch (type) {
    case constants.FETCH_SUBMITTED:
      return { ...state, loading: true };
    case constants.FETCH_COMPLETED:
      return { ...state, items: payload, loading: false };
    default:
      return state;
  }
};
