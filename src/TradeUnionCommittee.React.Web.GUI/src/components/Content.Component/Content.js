import React from 'react'
import { makeStyles } from '@material-ui/core/styles';
import Box from '@material-ui/core/Box';
import Paper from '@material-ui/core/Paper';

import Routes from '../../routes'
import Copyright from '../Copyright.Component/Copyright'

const useStyles = makeStyles(theme => ({
    content: {
        flexGrow: 1,
        height: '100vh',
        overflow: 'auto',
    },
    appBarSpacer: theme.mixins.toolbar,
    container: {
        textAlign: 'center',
        maxWidth: 'lg'
    },
    layout: {
        width: 'auto',
        margin: theme.spacing(3),
        [theme.breakpoints.up(800 + theme.spacing(3) * 2)]: {
            margin: theme.spacing(5),
        },
    },
    paper: {
        padding: theme.spacing(2),
        [theme.breakpoints.up(800 + theme.spacing(3) * 2)]: {
            padding: theme.spacing(3),
        }
    },
}));

function Content() {

    const classes = useStyles();

    return (
        <main className={classes.content}>
            <div className={classes.appBarSpacer} />
            <div className={classes.container}>
                <React.Fragment>
                    <Box className={classes.layout}>
                        <Paper className={classes.paper}>
                            <Routes />
                        </Paper>
                        <Copyright />
                    </Box>
                </React.Fragment>
            </div>
        </main>
    )
}

export default Content
