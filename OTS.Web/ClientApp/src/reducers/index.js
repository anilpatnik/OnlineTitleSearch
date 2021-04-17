import { combineReducers } from "redux";
import { searchReducer } from "./search.reducer";

const appReducer = combineReducers({
  items: searchReducer
});

const rootReducer = (state, action) => {
  return appReducer(state, action);
};

export default rootReducer;
