import React from 'react'
import { makeStyles } from '@material-ui/core/styles';

import Routes from '../routes'

const useStyles = makeStyles(theme => ({
    content: {
        flexGrow: 1,
        height: '100vh',
        overflow: 'auto',
    },
    appBarSpacer: theme.mixins.toolbar,
    container: {
        textAlign: 'center',
        paddingTop: theme.spacing(7),
        paddingBottom: theme.spacing(7),
        margin: theme.spacing(5),
        maxWidth: 'lg'
    }
}));

function Content() {

    const classes = useStyles();

    return (
        <main className={classes.content}>
            <div className={classes.appBarSpacer} />
            <div className={classes.container}>
                <Routes />
            </div>
        </main>
    )
}

export default Content
