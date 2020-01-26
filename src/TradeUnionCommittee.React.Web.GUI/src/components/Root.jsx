import React from 'react'
import { Route, Switch, Redirect } from 'react-router-dom';

import App from '../components/App'
import AuthContainer from '../containers/AuthContainer'
import NotFound from '../components/NotFound.Component/NotFound'
import SplashScreen from '../components/SplashScreen.Component/SplashScreen'

function Root() {
    return (
        <Switch>
            <Redirect exact from="/" to="/app" />
            <Route path="/app" component={App} />
            <Route path="/auth" component={AuthContainer} />
            <Route path="*" component={NotFound} />
        </Switch>
    )
}

export default SplashScreen(Root);
