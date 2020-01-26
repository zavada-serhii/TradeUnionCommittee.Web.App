import React from 'react'
import { Route, Switch } from 'react-router-dom';

import CheckoutContainer from '../components/Checkout.Component/Checkout'
import NotFound from '../components/NotFound.Component/NotFound'

function Routes() {
  return (
    <Switch>
      <Route exact path="/app" render={() => <div>Home Page</div>} />
      <Route path="/app/create-employee" component={CheckoutContainer} />
      <Route path="/app/position" render={() => <div>Here will be page for Position</div>} />
      <Route path="/app/social-position" render={() => <div>Here will be page for social-position</div>} />
      <Route path="/app/privileges" render={() => <div>Here will be page for privileges</div>} />
      <Route path="/app/award" render={() => <div>Here will be page for award</div>} />
      <Route path="/app/material-aid" render={() => <div>Here will be page for material-aid</div>} />
      <Route path="/app/hobby" render={() => <div>Here will be page for hobby</div>} />
      <Route path="/app/travel" render={() => <div>Here will be page for travel</div>} />
      <Route path="/app/wellness" render={() => <div>Here will be page for wellness</div>} />
      <Route path="/app/tour" render={() => <div>Here will be page for tour</div>} />
      <Route path="/app/activities" render={() => <div>Here will be page for activities</div>} />
      <Route path="/app/cultural-activities" render={() => <div>Here will be page for cultural-activities</div>} />
      <Route path="/app/subdivisions" render={() => <div>Here will be page for subdivisions</div>} />
      <Route path="/app/departmental-housing" render={() => <div>Here will be page for departmental-housing</div>} />
      <Route path="/app/dormitory" render={() => <div>Here will be page for dormitory</div>} />
      <Route path="/app/users" render={() => <div>Here will be page for users</div>} />
      <Route path="/app/dashboard" render={() => <div>Here will be page for dashboard</div>} />
      <Route path="/app/action-log" render={() => <div>Here will be page for action-log</div>} />
      <Route path="/app/search" render={() => <div>Here will be page for search</div>} />
      <Route path="/app/logout" render={() => <div>Here will be page for logout</div>} />
      <Route path="*" component={NotFound} />
    </Switch>
  )
}

export default Routes
