import { createMuiTheme, CssBaseline, ThemeProvider, useMediaQuery } from '@material-ui/core';
import { purple } from '@material-ui/core/colors';
import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {

    const darkTheme = createMuiTheme({
      palette: {
        type: 'light',
      },
    });

    return (
      <div>
        <ThemeProvider theme={darkTheme}>
          <CssBaseline/>
        </ThemeProvider>
          <NavMenu />
            <Container>
              {this.props.children}
            </Container>
      </div>
    );
  }
}
