import React, { useEffect, Fragment } from "react";
import { useDispatch, useSelector, shallowEqual } from "react-redux";
import { Box, Paper, CircularProgress, Backdrop } from "@material-ui/core";
import {
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell
} from "@material-ui/core";
import { saerchActions } from "../actions";
import { ListDetailsComponent } from "./list.details.component";

export const ListComponent = () => {
  const { items, loading } = useSelector(state => state.items, shallowEqual);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(saerchActions.getSearchResults("online title search"));
  }, [dispatch]);

  return (
    <Fragment>
      {items.length > 0 && (
        <Box mb={5}>
          <TableContainer component={Paper}>
            <div style={{ overflow: "auto" }}>
              <Table aria-label="simple table">
                <TableHead>
                  <TableRow>
                    <TableCell>Search Engine</TableCell>
                    <TableCell>Ranking Order</TableCell>
                  </TableRow>
                </TableHead>
              </Table>
              <div>
                <Table style={{ tableLayout: "fixed" }}>
                  <TableBody>
                    {items.map((item, index) => {
                      return <ListDetailsComponent item={item} key={index} />;
                    })}
                  </TableBody>
                </Table>
              </div>
            </div>
          </TableContainer>
        </Box>
      )}
      <Backdrop open={loading} style={{ zIndex: 100 }}>
        <CircularProgress />
      </Backdrop>
    </Fragment>
  );
};
