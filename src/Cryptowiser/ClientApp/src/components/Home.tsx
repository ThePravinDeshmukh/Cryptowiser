import React, { Component } from 'react';
import { IProps } from '../datasources/IProps';
import Autocom from './Autocom';

interface IHomeState {
  currentCount: number;
}

export class Home extends Component<IProps, IHomeState> {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h2>Dashboard</h2>
        <br/>
        <Autocom/> 
      </div>
    );
  }
}
