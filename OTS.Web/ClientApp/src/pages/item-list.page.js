import React, { useEffect, Fragment } from "react";
import { useHistory } from "react-router-dom";
import { useSelector, shallowEqual, useDispatch } from "react-redux";
import {
  Box,
  Paper,
  CircularProgress,
  Backdrop,
  Button,
} from "@material-ui/core";
import {
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
} from "@material-ui/core";
import { constants } from "../common";
import { ItemDetailsComponent } from "../components/item-details.component";
import { itemActions } from "../actions";

export const ItemListPage = () => {
  const { items, loading } = useSelector(state => state.items, shallowEqual);
  const history = useHistory();
  const dispatch = useDispatch();

  useEffect(() => {
    (async function () {
      dispatch(itemActions.getItems());
    })();
  }, [dispatch]);

  return <Fragment>
    {items.length > 0 && <Box mb={5}>
        <TableContainer component={Paper}>
          <div style={{ overflow: "auto" }}>
            <Table aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell>Id</TableCell>
                  <TableCell>Name</TableCell>
                  <TableCell>Description</TableCell>
                  <TableCell/>
                </TableRow>
              </TableHead>
            </Table>
            <div style={{ overflow: "auto", height: "400px" }}>
              <Table style={{ tableLayout: "fixed" }}>
                <TableBody>
                  {items.map(item => {
                    return <ItemDetailsComponent item={item} key={item.id} />;
                  })}
                </TableBody>
              </Table>
            </div>
          </div>
        </TableContainer>
      </Box>}
    <Button
      variant="contained"
      color="primary"
      onClick={() => history.replace(constants.HOME_URL)}
    >
      Add Item
    </Button>
    <Backdrop open={loading} style={{ zIndex: 100 }}>
      <CircularProgress />
    </Backdrop>
  </Fragment>;
};
