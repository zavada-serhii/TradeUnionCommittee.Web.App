// https://auth0.com/blog/creating-a-splash-screen-for-your-react-apps/ 

import React, { Component } from 'react';
import './SplashScreen.css';

function SplashScreen(WrappedComponent) {
  document.body.style.backgroundImage = 'radial-gradient(circle farthest-corner at center, #3C4B57 0%, #1C262B 100%)'
  return class extends Component {
    constructor(props) {
      super(props);
      this.state = {
        loading: true,
      };
    }

    async componentDidMount() {
      try {

        // -----------------------------------------------------------------
        // -- Emulation authorization (Check access token ...)
        // -----------------------------------------------------------------

        // await auth0Client.loadSession();
        await this.sleeper(1000);

        // -----------------------------------------------------------------

        setTimeout(() => {
          this.setState({
            loading: false,
          });
        }, 1500)
      } catch (err) {
        console.log(err);
        this.setState({
          loading: false,
        });
      }
    }

    render() {
      // while checking user session, show "loading" message
      if (this.state.loading) return this.LoadingMessage();
      
      document.body.style.backgroundImage = null;
      // otherwise, show the desired route
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

    sleeper(ms) {
      return new Promise(resolve => setTimeout(() => resolve(), ms));
    }
  };
}

export default SplashScreen;
