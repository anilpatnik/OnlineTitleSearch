import React from "react";
import { useSelector } from "react-redux";
import { Box, Paper } from "@material-ui/core";

export const HearderComponent = () => {
  const { items } = useSelector(state => state.items);

  return (
    <Box mb={5}>
      <Paper className="paper">
        <div className="heading">
          Hello, currently you have {items.length} item{items.length !== 1 && "s"}...
        </div>
      </Paper>
    </Box>
  );
};
