// https://auth0.com/blog/creating-a-splash-screen-for-your-react-apps/ 

import React, {Component} from 'react';
import './index.css';

function LoadingMessage() {
  return (
    <div className="splash-screen">
      Wait a moment while we load your app.
      <div className="loading-dot">.</div>
    </div>
  );
}

function SplashScreen(WrappedComponent) {
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
        await sleeper(1000);

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
      if (this.state.loading) return LoadingMessage();

      // otherwise, show the desired route
      return <WrappedComponent {...this.props} />;
    }
  };
}

function sleeper(ms) {
  return new Promise(resolve => setTimeout(() => resolve(), ms));
}

export default SplashScreen;
