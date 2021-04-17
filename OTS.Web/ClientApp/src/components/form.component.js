import React from "react";
import { useFormik } from "formik";
import * as Yup from "yup";
import { useDispatch } from "react-redux";
import { Box, Paper, Grid, TextField, Button } from "@material-ui/core";
import { saerchActions } from "../actions";

export const FormComponent = () => {
  const dispatch = useDispatch();

  const formik = useFormik({
    initialValues: { search: "online title search" },
    validationSchema: Yup.object({
      search: Yup.string().required("Required")
    }),
    onSubmit: values => {
      const { search } = values;
      dispatch(saerchActions.getSearchResults(search));
    }
  });

  const { search: f1_tou } = formik.touched;
  const { search: f1_err } = formik.errors;
  return (
    <Box mb={5}>
      <Paper className="paper">
        <form className="form" onSubmit={formik.handleSubmit}>
          <Grid container direction="row" spacing={5}>
            <Grid item xs={9}>
              <TextField
                name="search"
                label="Search"
                type="text"
                fullWidth
                helperText={f1_tou && f1_err ? f1_err : ""}
                error={f1_tou && f1_err ? true : false}
                {...formik.getFieldProps("search")}
              />
            </Grid>
            <Grid item xs={3}>
              <Button variant="contained" color="primary" type="submit">
                Submit
              </Button>
            </Grid>
          </Grid>
        </form>
      </Paper>
    </Box>
  );
};
