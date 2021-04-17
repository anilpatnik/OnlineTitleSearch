import React from "react";
import { useDispatch } from "react-redux";
import { IconButton, TableRow, TableCell } from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";
import { itemActions } from "../actions";

export const ItemDetailsComponent = ({ item }) => {
  const dispatch = useDispatch();

  return (
    <TableRow key={item.id}>
      <TableCell>{item.id}</TableCell>
      <TableCell>{item.name}</TableCell>
      <TableCell>{item.desc}</TableCell>
      <TableCell align="right">
        <IconButton
          edge="end"
          aria-label="delete"
          onClick={() => dispatch(itemActions.removeItem(item.id))}
        >
          <DeleteIcon />
        </IconButton>
      </TableCell>
    </TableRow>
  );
};
