import axios from 'axios';
import React, { Component } from 'react';
import { IProps } from '../datasources/IProps';
import { IUser } from '../datasources/IUser';
import { authHeader } from '../_helpers/auth-header';


interface IUserState {
  users: IUser[]
  loading: boolean;
  errorMessage: string;
}

export class Users extends Component<IProps, IUserState> {
  static displayName = Users.name;

  constructor(props: IProps) {
    super(props);
    this.state = { users: [], loading: true, errorMessage: "" };
  }

  componentDidMount() {
    this.populateUserData();
  }

  static renderUsersTable(users: IUser[]) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Username</th>
          </tr>
        </thead>
        <tbody>
          {users.map(user =>
            <tr key={user.id}>
              <td>{user.id}</td>
              <td>{user.firstName}</td>
              <td>{user.lastName}</td>
              <td>{user.username}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Users.renderUsersTable(this.state.users);

    return (
      <div>
        <h1 id="tabelLabel" >Users</h1>
        { contents }
      </div>
    );
  }

  async populateUserData() {
    const headers = authHeader();

    axios.get('api/user/all', { headers })
    .then(response => {      
      this.setState({ users: response.data, loading: false });
    })
    .catch(error => {      
        this.setState({ errorMessage: error.message });
        console.error('There was an error!', error);
    });
  }
  
}
