import React from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import { constants } from "../common";
import { ItemListPage, ItemFormPage } from "../pages";

// authorization rules
// const User = WithAuth([constants.USER_ROLE, constants.ADMIN_ROLE]);
// const Admin = WithAuth([constants.ADMIN_ROLE]);

// prettier-ignore
export const RoutesComponent = () => (
  <Switch>
    <Route exact path={constants.ROOT_URL} component={ItemListPage} />    
    <Route exact path={constants.HOME_URL} component={ItemFormPage} />        
    <Redirect to={constants.ROOT_URL} component={ItemListPage} />    
  </Switch>
);
