import React from 'react'
import { Route, Switch, Redirect } from 'react-router-dom';

import App from '../components/App'
import AuthContainer from '../containers/AuthContainer'
import NotFound from '../components/NotFound.Component/NotFound'
import SplashScreen from '../components/SplashScreen.Component/SplashScreen'
import requireAuth from '../utils/requireAuth';

import { ROOT, APP, AUTH, NOT_FOUND } from '../constants/routes'

function Root() {
    return (
        <Switch>
            <Redirect exact from={ROOT} to={APP} />
            <Route path={APP} component={requireAuth(App)} />
            <Route path={AUTH} component={AuthContainer} />
            <Route path={NOT_FOUND} component={NotFound} />
        </Switch>
    )
}

export default SplashScreen(Root);
