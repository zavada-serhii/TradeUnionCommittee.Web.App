import React from 'react'
import { Route, Switch } from 'react-router-dom';

import CheckoutContainer from '../components/Checkout.Component/Checkout'
import PositionContainer from '../containers/Position.Containers/PositionContainer'
import CreatePositionContainer from '../containers/Position.Containers/CreatePositionContainer'
import NotFoundContainer from '../containers/NotFoundContainer'

import {
  NOT_FOUND,
  APP,
  APP_CREATE_EMPLOYEE,
  APP_POSITION,
  APP_CREATE_POSITION,
  APP_SOCIAL_POSITION,
  APP_PRIVILEGES,
  APP_AWARD,
  APP_MATERIAL_AID,
  APP_HOBBY,
  APP_TRAVEL,
  APP_WELLNESS,
  APP_TOUR,
  APP_ACTIVITIES,
  APP_CULTURAL_ACTIVITIES,
  APP_SUBDIVISIONS,
  APP_DEPARTMENRAL_HOUSING,
  APP_DORMIRTORY,
  APP_USERS,
  APP_DASHBOARD,
  APP_ACTION_LOG,
  APP_SEARCH
} from '../constants/routes'

function Routes() {
  return (
    <Switch>
      <Route exact path={APP} render={() => <div>Home Page</div>} />
      <Route path={APP_CREATE_EMPLOYEE} component={CheckoutContainer} />
      <Route path={APP_POSITION} component={PositionContainer} />
      <Route path={APP_CREATE_POSITION} component={CreatePositionContainer} />
      <Route path={APP_SOCIAL_POSITION} render={() => <div>Here will be page for social-position</div>} />
      <Route path={APP_PRIVILEGES} render={() => <div>Here will be page for privileges</div>} />
      <Route path={APP_AWARD} render={() => <div>Here will be page for award</div>} />
      <Route path={APP_MATERIAL_AID} render={() => <div>Here will be page for material-aid</div>} />
      <Route path={APP_HOBBY} render={() => <div>Here will be page for hobby</div>} />
      <Route path={APP_TRAVEL} render={() => <div>Here will be page for travel</div>} />
      <Route path={APP_WELLNESS} render={() => <div>Here will be page for wellness</div>} />
      <Route path={APP_TOUR} render={() => <div>Here will be page for tour</div>} />
      <Route path={APP_ACTIVITIES} render={() => <div>Here will be page for activities</div>} />
      <Route path={APP_CULTURAL_ACTIVITIES} render={() => <div>Here will be page for cultural-activities</div>} />
      <Route path={APP_SUBDIVISIONS} render={() => <div>Here will be page for subdivisions</div>} />
      <Route path={APP_DEPARTMENRAL_HOUSING} render={() => <div>Here will be page for departmental-housing</div>} />
      <Route path={APP_DORMIRTORY} render={() => <div>Here will be page for dormitory</div>} />
      <Route path={APP_USERS} render={() => <div>Here will be page for users</div>} />
      <Route path={APP_DASHBOARD} render={() => <div>Here will be page for dashboard</div>} />
      <Route path={APP_ACTION_LOG} render={() => <div>Here will be page for action-log</div>} />
      <Route path={APP_SEARCH} render={() => <div>Here will be page for search</div>} />
      <Route path={NOT_FOUND} component={NotFoundContainer} />
    </Switch>
  )
}

export default Routes
