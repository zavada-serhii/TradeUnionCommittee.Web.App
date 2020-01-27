import React from 'react'
import { Route, Switch, Redirect } from 'react-router-dom';

import AppContainer from '../containers/AppContainer'
import AuthContainer from '../containers/AuthContainer'
import NotFoundContainer from '../containers/NotFoundContainer'
import SplashScreen from '../components/SplashScreen.Component/SplashScreen'
import requireAuth from '../utils/requireAuth';

import { ROOT, APP, AUTH, NOT_FOUND } from '../constants/routes'

function Root() {
    return (
        <Switch>
            <Redirect exact from={ROOT} to={APP} />
            <Route path={APP} component={requireAuth(AppContainer)} />
            <Route path={AUTH} component={AuthContainer} />
            <Route path={NOT_FOUND} component={NotFoundContainer} />
        </Switch>
    )
}

export default SplashScreen(Root);
