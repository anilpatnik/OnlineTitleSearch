import React, { Fragment } from "react";
import { AppBar, Toolbar, Typography, Container, Box, Grid, Button } from "@material-ui/core";
import { RoutesComponent } from "../routes";

export const App = () => {
  return (
    <Fragment>
      <AppBar position="static">
        <Toolbar>
          <Grid container direction="row">
            <Grid item xs={6}>
              <Typography variant="h6">Company Logo</Typography>
            </Grid>
            <Grid item xs={6}>
              <Button
                variant="contained"
                color="primary"
                disableElevation
                style={{ float: "right" }}
              >
                Logout
              </Button>
            </Grid>
          </Grid>
        </Toolbar>
      </AppBar>
      <Box mt={5}>
        <Container maxWidth="md">
          <RoutesComponent />
        </Container>
      </Box>
    </Fragment>
  );
};
