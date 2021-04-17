import React from "react";
import { useHistory } from "react-router-dom";
import { useFormik } from "formik";
import * as Yup from "yup";
import { useDispatch } from "react-redux";
import { Paper, Grid, TextField, Button } from "@material-ui/core";
import { constants } from "../common";
import { itemActions } from "../actions";

export const ItemFormPage = () => {
  const dispatch = useDispatch();
  const history = useHistory();

  const formik = useFormik({
    initialValues: { name: "", desc: "" },
    validationSchema: Yup.object({
      name: Yup.string().required("Required"),
      desc: Yup.string().required("Required"),
    }),
    onSubmit: (values) => {
      const { name, desc } = values;
      dispatch(itemActions.addItem({ name, desc }));
      console.log("dispatch complete");
      formik.resetForm();
      history.replace(constants.ROOT_URL);
      console.log("route change");
    },
  });

  const { name: f1_tou, desc: f2_tou } = formik.touched;
  const { name: f1_err, desc: f2_err } = formik.errors;
  return (
    <Paper className="paper">
      <form className="form" onSubmit={formik.handleSubmit}>
        <Grid container direction="column" spacing={5}>
          <Grid item xs={6}>
            <TextField
              name="name"
              label="Name"
              type="text"
              fullWidth
              helperText={f1_tou && f1_err ? f1_err : ""}
              error={!!(f1_tou && f1_err)}
              {...formik.getFieldProps("name")}
            />
          </Grid>
          <Grid item xs={6}>
            <TextField
              name="desc"
              label="Description"
              type="text"
              fullWidth
              helperText={f2_tou && f2_err ? f2_err : ""}
              error={!!(f2_tou && f2_err)}
              {...formik.getFieldProps("desc")}
            />
          </Grid>
          <Grid item>
            <Button variant="contained" color="primary" type="submit">
              Submit
            </Button>
          </Grid>
        </Grid>
      </form>
    </Paper>
  );
};
