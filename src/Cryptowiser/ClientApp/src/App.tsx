import React, { Component } from 'react';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";
import './custom.css'
import Login from "./components/Login";
import SignUp from "./components/Signup";
import { Users } from './repository/User.Repository';
import axios from 'axios';
import { authHeader } from '../src/_helpers/auth-header';

interface IAppState {
  isAuth: boolean;
  loading :boolean;
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
      
      isAuthenticated = true;
    })
    .catch(error => {
      
      console.log('Invalid Token! Redirecting to Login.');
      isAuthenticated = false;
    });
    await res;
    
    this.setState({isAuth: isAuthenticated})
    this.setState({loading: false})
    return isAuthenticated;
 }

  constructor(props: IProps) {
    
    super(props);
      this.state={isAuth:false, loading: true};
  }

  async componentDidMount() {
    await this.getAuthenticationStatus();
}

  render () {
    
    let isAuth = this.state.isAuth
    return (
      <div >
          <Layout>
            {!this.state.loading && (<Switch>
              <Route path="/login" component={Login} />
              <Route path="/signup" component={SignUp} />
              <PrivateRoute
                  path='/users'
                  redirect={ Login }
                  component={ Users }
                  isAuthenticated={isAuth}
              />
              <PrivateRoute
                  path='/'
                  redirect={Login}
                  component={Home}
                  isAuthenticated={isAuth}
              />
            </Switch>)}
          </Layout>
      </div>
    );
  }
}



const PrivateRoute = ({component, redirect, isAuthenticated, ...rest}: any) => {
  
    const routeComponent = (props: any) => (
      
        (isAuthenticated)
            ? React.createElement(component, props)
            : React.createElement(redirect, props)
    );
    return <Route {...rest} render={routeComponent}/>;
};
