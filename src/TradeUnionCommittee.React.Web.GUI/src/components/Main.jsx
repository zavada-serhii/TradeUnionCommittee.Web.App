import React from 'react'
import { Router } from 'react-router-dom'
import { createBrowserHistory } from 'history'
import { makeStyles } from '@material-ui/core/styles';
import CssBaseline from '@material-ui/core/CssBaseline';

import Navigation from './Navigation.Component/Navigation'
import Content from './Content.Component/Content'

export const history = createBrowserHistory()

const useStyles = makeStyles(theme => ({
    markup: {
        display: 'flex', 
        backgroundColor: '#ffffff' , 
        backgroundImage: 'linear-gradient(315deg, #ffffff 0%, #d7e1ec 74%)'
    }
}));

function Main() {

    const classes = useStyles();

    return (
        <Router history={history}>
            <div className={classes.markup}>
                <CssBaseline />
                <Navigation />
                <Content />
            </div>
        </Router>
    )
}

export default Main
