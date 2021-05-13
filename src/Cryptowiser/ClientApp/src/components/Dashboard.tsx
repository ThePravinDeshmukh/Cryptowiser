
import React, { Component } from 'react';
import { IProps } from '../datasources/IProps';
import Autocom from './Autocom';

interface IDashboardState {
  currentCount: number;
}

export class Dashboard extends Component<IProps, IDashboardState> {
  static displayName = Dashboard.name;

  constructor(props: IProps) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
  }

  incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount + 1
    });
  }

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
