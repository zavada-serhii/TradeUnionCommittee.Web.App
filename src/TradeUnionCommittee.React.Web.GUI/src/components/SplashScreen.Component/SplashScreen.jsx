// https://auth0.com/blog/creating-a-splash-screen-for-your-react-apps/ 

import React from 'react';
import { connect } from 'react-redux';
import { setCurrentUser } from '../../actions/auth';
import setAuthorizationToken from '../../utils/setAuthorizationToken';
import jwtDecode from 'jwt-decode';
import './SplashScreen.css';

export default function (WrappedComponent) {

  document.body.style.backgroundImage = 'radial-gradient(circle farthest-corner at center, #3C4B57 0%, #1C262B 100%)'

  class SplashScreen extends React.Component {

    constructor(props) {
      super(props);
      this.state = { loading: true };
    }

    async componentDidMount() {
      try {
        if (localStorage.access_token && localStorage.token_type) {
          setAuthorizationToken(localStorage.access_token, localStorage.token_type);
          this.props.setCurrentUser(jwtDecode(localStorage.access_token));
        }
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
  }

  const putActionsToProps = {
    setCurrentUser
  }

  return connect(null, putActionsToProps)(SplashScreen);
}