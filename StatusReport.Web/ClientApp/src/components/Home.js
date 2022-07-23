import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
            <div>
                <strong>Enter your Id</strong>
                <input type="text" />
                <input type="button" name="Submit" value="Submit"/>
            </div>
      </div>
    );
  }
}
