import { combineReducers } from "redux";
import { itemReducer } from "./item.reducer";

const appReducer = combineReducers({
  items: itemReducer
});

const rootReducer = (state, action) => {
  return appReducer(state, action);
};

export default rootReducer;
