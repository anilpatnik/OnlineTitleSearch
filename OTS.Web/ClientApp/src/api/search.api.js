import { constants } from "../common";

export const getSearchResults = async keywords => {
  const response = await fetch(
    `${constants.API_URL}/search?keywords=${encodeURIComponent(keywords)}`
  );
  return response.json();
};
