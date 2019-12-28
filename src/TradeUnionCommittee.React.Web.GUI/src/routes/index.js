import React from 'react'
import CounterContainer from '../containers/CounterContainer'
import Navigation from '../components/Navigation'
import { Router, Route, Switch } from 'react-router-dom'
import { createBrowserHistory } from 'history'
import styled from '@emotion/styled'

const Container = styled.div`
  text-align: center;
`
export const history = createBrowserHistory()

function Routes() {
  return (
    <Router history={history}>
      <Container>
        <Navigation />
        <Switch>
          <Route path="/create-employee" component={CounterContainer} />
          <Route path="/position" render={() => <div>Here will be page for Position</div>} />
          <Route path="/social-position" render={() => <div>Here will be page for social-position</div>} />
          <Route path="/privileges" render={() => <div>Here will be page for privileges</div>} />
          <Route path="/award" render={() => <div>Here will be page for award</div>} />
          <Route path="/material-aid" render={() => <div>Here will be page for material-aid</div>} />
          <Route path="/hobby" render={() => <div>Here will be page for hobby</div>} />
          <Route path="/travel" render={() => <div>Here will be page for travel</div>} />
          <Route path="/wellness" render={() => <div>Here will be page for wellness</div>} />
          <Route path="/tour" render={() => <div>Here will be page for tour</div>} />
          <Route path="/activities" render={() => <div>Here will be page for activities</div>} />
          <Route path="/cultural-activities" render={() => <div>Here will be page for cultural-activities</div>} />
          <Route path="/subdivisions" render={() => <div>Here will be page for subdivisions</div>} />
          <Route path="/departmental-housing" render={() => <div>Here will be page for departmental-housing</div>} />
          <Route path="/dormitory" render={() => <div>Here will be page for dormitory</div>} />
          <Route path="/users" render={() => <div>Here will be page for users</div>} />
          <Route path="/dashboard" render={() => <div>Here will be page for dashboard</div>} />
          <Route path="/action-log" render={() => <div>Here will be page for action-log</div>} />
          <Route path="/search" render={() => <div>Here will be page for search</div>} />
          <Route path="/logout" render={() => <div>Here will be page for logout</div>} />
        </Switch>
      </Container>
    </Router>
  )
}

export default Routes
