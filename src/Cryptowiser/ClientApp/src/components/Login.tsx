import axios from "axios";
import React from "react";
import { useForm } from "./useForm";


function Login() {

    // defining the initial state for the form
    const initialState = {
        Username: "",
        Password: "",
    };

    // getting the event handlers from our custom hook
    const { onChange, onSubmit, values } = useForm(
        loginUserCallback,
        initialState
    );

    // a submit function that will execute upon form submission
    async function loginUserCallback() {
        // send "values" to api
        
        await axios.post(
            'api/user/authenticate',
            values
        )
        .then(response => {
                // store the user in localStorage
                localStorage.setItem('user', JSON.stringify(response.data));
                console.log(response.data);
                window.location.replace("/");
            })
        .catch(error => {
            if (error.response != undefined)
            {
                console.error(error.response.data.message, error);
            }
        });
    }

    return (
    <div>

    <form onSubmit={onSubmit}>
        <h3>Login</h3>

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

        {

        }

        <button type="submit" className="btn btn-primary btn-block">Submit</button>

    </form>
    </div>
        
    );
}

export default Login;
