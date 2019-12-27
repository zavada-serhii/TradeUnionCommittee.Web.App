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
          <Route path="/" component={CounterContainer} />
        </Switch>
      </Container>
    </Router>
  )
}

export default Routes
