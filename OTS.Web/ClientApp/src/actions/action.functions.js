import { constants } from "../common";

export const fetchSubmitted = () => {
  return { type: constants.FETCH_SUBMITTED };
};

export const fetchCompleted = payload => {
  return { type: constants.FETCH_COMPLETED, payload };
};
