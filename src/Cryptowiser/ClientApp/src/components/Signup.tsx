import axios from "axios";
import React from "react";
import { useForm } from "./useForm";
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Link from '@material-ui/core/Link';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
// import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';


const useStyles = makeStyles((theme) => ({
    paper: {
      marginTop: theme.spacing(8),
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
    },
    avatar: {
      margin: theme.spacing(1),
      backgroundColor: theme.palette.secondary.main,
    },
    form: {
      width: '100%', // Fix IE 11 issue.
      marginTop: theme.spacing(3),
    },
    submit: {
      margin: theme.spacing(3, 0, 2),
    },
  }));

function Signup() {
    // defining the initial state for the form
    const initialState = {
        Username: "",
        Password: "",
        FirstName: "",
        LastName: "",
    };


    // getting the event handlers from our custom hook
    const { onChange, onSubmit, values } = useForm(
        signupUserCallback,
        initialState
    );

    // a submit function that will execute upon form submission
    async function signupUserCallback() {
        // send "values" to api
        
        await axios.post(
            'api/user/register',
            values
        )
        .then(response => {
                console.log(response.data);
                window.location.replace("/login");
            })
        .catch(error => {
            
            console.error(error.response.data.message, error);
        });
    }

    // return (
    // <div>

    // <form onSubmit={onSubmit}>
    //     <h3>Signup</h3>


    //     <div className="form-group">
    //         <label>First Name</label>
    //         <input 
    //             name='FirstName'
    //             id='firstname'
    //             type="text" 
    //             className="form-control" 
    //             placeholder="Enter First Name"
    //             onChange={onChange}
    //             required
    //         />
    //     </div>

    //     <div className="form-group">
    //         <label>Last Name</label>
    //         <input 
    //             name='LastName'
    //             id='lastname'
    //             type="text" 
    //             className="form-control" 
    //             placeholder="Enter Last Name"
    //             onChange={onChange}
    //             required
    //         />
    //     </div>
    //     <div className="form-group">
    //                 <label>Username</label>
    //         <input 
    //                     name='Username'
    //                     id='Username'
    //             type="text" 
    //             className="form-control" 
    //                     placeholder="Enter Username"
    //             onChange={onChange}
    //             required
    //         />
    //     </div>

    //     <div className="form-group">
    //         <label>Password</label>
    //         <input 
    //             className="form-control" 
    //             placeholder="Enter password" 
    //             name='Password'
    //             id='Password'
    //             type='password'
    //             onChange={onChange}
    //             required
    //         />
    //     </div>

    //     <button type="submit" className="btn btn-primary btn-block">Submit</button>
    //     <p className="forgot-password text-right">
    //                 Already registered <a href="/login">sign in?</a>
    //     </p>

    // </form>
    // </div>
        
    // );

    const classes = useStyles();

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign up
        </Typography>
        <form className={classes.form} onSubmit={onSubmit}>
          <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
              <TextField
                autoComplete="fname"
                name="firstName"
                variant="outlined"
                required
                fullWidth
                id="firstName"
                label="First Name"
                onChange={onChange}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="lastName"
                label="Last Name"
                name="lastName"
                autoComplete="lname"
                onChange={onChange}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="username"
                label="Username"
                name="username"
                autoComplete="username"
                onChange={onChange}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                autoComplete="current-password"
                onChange={onChange}
              />
            </Grid>
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            Sign Up
          </Button>
          <Grid container justify="flex-end">
            <Grid item>
              <Link href="#" variant="body2">
                Already have an account? Sign in
              </Link>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
}

export default Signup;
