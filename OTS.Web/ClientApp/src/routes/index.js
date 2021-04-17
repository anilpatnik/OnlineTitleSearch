import React from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import { constants } from "../common";
import { SearchPage } from "../pages";

// prettier-ignore
export const RoutesComponent = () => (
  <Switch>
    <Route exact path={constants.APP_URL} component={SearchPage} />    
    <Redirect to={constants.APP_URL} component={SearchPage} />    
  </Switch>
);
