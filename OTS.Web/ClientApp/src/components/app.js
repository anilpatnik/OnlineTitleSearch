import React, { Fragment } from "react";
import {
  AppBar,
  Toolbar,
  Typography,
  Container,
  Box,
  Grid,
  Button,
} from "@material-ui/core";
import { HearderComponent } from "./header.component";
import { RoutesComponent } from "../routes";

export const App = () => {
  return (
    <Fragment>
      <AppBar position="static">
        <Toolbar>
          <Grid container direction="row">
            <Grid item xs={6}>
              <Typography variant="h6">Dingy</Typography>
            </Grid>
            <Grid item xs={6}>
              <Button
                variant="contained"
                color="primary"
                disableElevation
                style={{ float: "right" }}
              >
                Login
              </Button>
            </Grid>
          </Grid>
        </Toolbar>
      </AppBar>
      <Box mt={5}>
        <Container maxWidth="md">
          <HearderComponent />
          <RoutesComponent />
        </Container>
      </Box>
    </Fragment>
  );
};
