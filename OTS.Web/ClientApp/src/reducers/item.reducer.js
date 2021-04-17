const initialState = {
  items: [],
  loading: false,
};

export const itemReducer = (state = initialState, { type, payload }) => {
  switch (type) {
    case "GET_ITEMS":
      return { ...state, items: payload, loading: false };
    case "LOAD_ITEM":
      return { ...state, loading: true };
    default:
      return state;
  }
};
