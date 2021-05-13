import React, { Component, useContext } from 'react';
//import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Dashboard } from './components/Dashboard';
import { BrowserRouter as Router, Switch, Route, Link, Redirect } from "react-router-dom";
import './custom.css'

//import Login from "./components/login.component";
import Login from "./components/Login";
import SignUp from "./components/Signup";
import { FetchUsers } from './repository/User.Repository';
import { IUser } from './datasources/IUser';
import axios from 'axios';
import { authHeader } from '../src/_helpers/auth-header';

interface IAppState {
  isAuth: boolean;
}
interface IProps {
}


export default class App extends Component<IProps, IAppState> {
  static displayName = App.name;

  async getAuthenticationStatus() : Promise<boolean> {
    let isAuthenticated: boolean = false;
    const headers = authHeader();
    const res = await axios.get('api/user/authenticate', { headers })
    .then(response => {
      debugger
      isAuthenticated = true;
    })
    .catch(error => {
      debugger
      console.log('Invalid Token! Redirecting to Login.');
      isAuthenticated = false;
    });
    await res;
    
    this.setState({isAuth: isAuthenticated})
    return isAuthenticated;
 }

  constructor(props: IProps) {
    debugger
    super(props);
      this.state={isAuth:false};
  }

  async componentDidMount() {  
}

  render () {

    this.getAuthenticationStatus();
    
    let isAuth = this.state.isAuth
    return (
      <div >    
      <Layout>
        <Route exact path='/' component={Home} />
        <Switch>
          <PrivateRoute
              path='/dashboard'
              redirect={Login}
              component={Dashboard}
              isAuthenticated={isAuth}
          />
          <PrivateRoute
              path='/users'
              redirect={Login}
              component={FetchUsers}
              isAuthenticated={isAuth}
          />
          <Route path="/login" component={Login} />
          <Route path="/signup" component={SignUp} />
          
        </Switch>
      </Layout>
      </div>
    );
  }
}



const PrivateRoute = ({component, redirect, isAuthenticated, ...rest}: any) => {
  debugger
    const routeComponent = (props: any) => (
      
        (isAuthenticated)
            ? React.createElement(component, props)
            : React.createElement(redirect, props)
    );
    return <Route {...rest} render={routeComponent}/>;
};


function getUser() {
  let user01 = {} as IUser;
  if (localStorage.getItem("user") === null) {
  } else {
    user01 = JSON.parse(localStorage.getItem('user')?? "");
  }
  return user01;
}

