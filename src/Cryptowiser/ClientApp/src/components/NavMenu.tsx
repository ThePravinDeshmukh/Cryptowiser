import React, { Component } from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  Container} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { IProps } from '../datasources/IProps';
import { IUser } from '../datasources/IUser';
import { LoggerInUser } from './loggedInUser';
import { unstable_renderSubtreeIntoContainer } from 'react-dom';

interface INavMenuState{
  collapsed: boolean;
}

export class NavMenu extends Component<IProps, INavMenuState> {
  static displayName = NavMenu.name;

  constructor (props: IProps) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    
    return (
      <header>
         <div>
          <Navbar color="light" light expand="md" className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3">
          <Container>
            <NavbarBrand href="/">Cryptowiser</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} />
            <Collapse isOpen={!this.state.collapsed} navbar>
              <Nav className="mr-auto" navbar>
                <NavItem>
                  <NavLink href="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink href="/users">Users</NavLink>
                </NavItem>
              </Nav>
              <ShowLogin />
            </Collapse>
          </Container>
          </Navbar>
        </div>
      </header>
    );
  }
}

function ShowLogin() {
  
  var user = LoggerInUser();
  if (user.token === undefined) return (
    
    <ul className="navbar-nav flex-grow">
      <NavItem className="nav-item">
        <NavLink tag={Link} className="nav-link" to="/login">Login</NavLink>
      </NavItem>
      <NavItem className="nav-item">
        <NavLink tag={Link} className="nav-link" active to="/signup">Signup</NavLink>
      </NavItem>
    </ul>
  );

  function  logout() {
    localStorage.clear();
    window.location.href = '/login';
}

  var user: IUser = JSON.parse(localStorage.getItem('user') ?? "");
  return (    
    <Nav className="mr-right" navbar>
    <UncontrolledDropdown nav inNavbar>
    <DropdownToggle nav caret>
      Signed in as: { user.lastName }, { user.firstName }
    </DropdownToggle>
    <DropdownMenu right>
      <DropdownItem divider />
      <DropdownItem>
        <NavLink tag={Link} className="nav-link" to="#" onClick={e=> logout()}>Logout</NavLink>
      </DropdownItem>
    </DropdownMenu>
  </UncontrolledDropdown>
  </Nav>
  );
}