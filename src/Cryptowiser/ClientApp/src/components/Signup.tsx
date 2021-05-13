import axios from "axios";
import React, { useState } from "react";
import { Redirect } from "react-router";
import { useForm } from "./useForm";

function Signup() {
    // defining the initial state for the form
    const initialState = {
        Username: "",
        Password: "",
        FirstName: "",
        LastName: "",
    };

    const [user, setUser] = useState();

    // getting the event handlers from our custom hook
    const { onChange, onSubmit, values } = useForm(
        signupUserCallback,
        initialState
    );

    // a submit function that will execute upon form submission
    async function signupUserCallback() {
        // send "values" to api
        debugger
        await axios.post(
            'api/user/register',
            values
        )
        .then(response => {
                console.log(response.data);
                window.location.replace("/login");
            })
        .catch(error => {
            debugger
            console.error(error.response.data.message, error);
        });
    }

    return (
    <div>

    <form onSubmit={onSubmit}>
        <h3>Signup</h3>


        <div className="form-group">
            <label>First Name</label>
            <input 
                name='FirstName'
                id='firstname'
                type="text" 
                className="form-control" 
                placeholder="Enter First Name"
                onChange={onChange}
                required
            />
        </div>

        <div className="form-group">
            <label>Last Name</label>
            <input 
                name='LastName'
                id='lastname'
                type="text" 
                className="form-control" 
                placeholder="Enter Last Name"
                onChange={onChange}
                required
            />
        </div>
        <div className="form-group">
                    <label>Username</label>
            <input 
                        name='Username'
                        id='Username'
                type="text" 
                className="form-control" 
                        placeholder="Enter Username"
                onChange={onChange}
                required
            />
        </div>

        <div className="form-group">
            <label>Password</label>
            <input 
                className="form-control" 
                placeholder="Enter password" 
                name='Password'
                id='Password'
                type='password'
                onChange={onChange}
                required
            />
        </div>

        <button type="submit" className="btn btn-primary btn-block">Submit</button>
        <p className="forgot-password text-right">
                    Already registered <a href="/login">sign in?</a>
        </p>

    </form>
    </div>
        
    );
}

export default Signup;
