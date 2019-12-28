import React from 'react'
import CounterContainer from '../containers/CounterContainer'
import Navigation from '../components/Navigation'
import { createBrowserHistory } from 'history'
import styled from '@emotion/styled'
import { Router } from 'react-router-dom'

const Container = styled.div`
  text-align: center;
`
export const history = createBrowserHistory()

function Routes() {
  return (
    <Router history={history}>
      <Container>
        <Navigation />
      </Container>
    </Router>
  )
}

export default Routes
