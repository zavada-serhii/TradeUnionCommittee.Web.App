import React from 'react'
import { Router } from 'react-router-dom'
import { createBrowserHistory } from 'history'
import CssBaseline from '@material-ui/core/CssBaseline';

import Navigation from '../components/Navigation'
import Content from '../components/Content'

export const history = createBrowserHistory()

function Main() {
    return (
        <Router history={history}>
            <div style={{ display: 'flex', backgroundColor: '#ffffff' , backgroundImage: 'linear-gradient(315deg, #ffffff 0%, #d7e1ec 74%)'}}>
                <CssBaseline />
                <Navigation />
                <Content />
            </div>
        </Router>
    )
}

export default Main
