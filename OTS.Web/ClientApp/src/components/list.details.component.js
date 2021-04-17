import React from "react";
import { TableRow, TableCell } from "@material-ui/core";

export const ListDetailsComponent = ({ item }) => {
  return (
    <TableRow>
      <TableCell>{item.SearchEngine}</TableCell>
      <TableCell>{item.RankingOrder}</TableCell>
    </TableRow>
  );
};
