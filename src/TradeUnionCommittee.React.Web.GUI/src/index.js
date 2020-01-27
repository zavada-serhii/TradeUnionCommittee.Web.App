import React from 'react'
import { Router } from 'react-router-dom'
import { createBrowserHistory } from 'history'
import { render } from 'react-dom'
import { Provider } from 'react-redux'
import store from './store'
import Root from './components/Root'
import * as serviceWorker from './utils/serviceWorker'

import { setCurrentUser } from './actions/Auth/actions';
import setAuthorizationToken from './utils/setAuthorizationToken';
import jwtDecode from 'jwt-decode';

import './styles/globalStyles.css'

export const history = createBrowserHistory()

if (localStorage.access_token && localStorage.token_type) {
  setAuthorizationToken(localStorage.access_token, localStorage.token_type);
  store.dispatch(setCurrentUser(jwtDecode(localStorage.access_token)));
}

render(
  <Provider store={store}>
    <Router history={history}>
      <Root />
    </Router>
  </Provider>,
  document.getElementById('root'),
)

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister()
