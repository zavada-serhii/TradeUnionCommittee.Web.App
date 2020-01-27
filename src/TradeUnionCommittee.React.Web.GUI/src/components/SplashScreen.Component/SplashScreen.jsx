// https://auth0.com/blog/creating-a-splash-screen-for-your-react-apps/ 

import React, { Component } from 'react';
import './SplashScreen.css';

function SplashScreen(WrappedComponent) {

  document.body.style.backgroundImage = 'radial-gradient(circle farthest-corner at center, #3C4B57 0%, #1C262B 100%)'

  return class extends Component {

    constructor(props) {
      super(props);
      this.state = { loading: true };
    }

    async componentDidMount() {
      try {
        setTimeout(() => { this.setState({ loading: false }) }, 2000)
      } catch (error) {
        this.setState({ loading: false });
      }
    }

    render() {
      if (this.state.loading) return this.LoadingMessage();

      document.body.style.backgroundImage = null;
      return <WrappedComponent {...this.props} />;
    }

    LoadingMessage() {
      return (
        <div className="loader">
          <div className="inner one"></div>
          <div className="inner two"></div>
          <div className="inner three"></div>
        </div>
      );
    }
  };
}

export default SplashScreen;
