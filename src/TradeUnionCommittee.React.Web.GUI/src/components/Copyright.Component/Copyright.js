import React from 'react'
import { makeStyles } from '@material-ui/core/styles';
import { Link } from 'react-router-dom'
import Typography from '@material-ui/core/Typography';

const useStyles = makeStyles(theme => ({
    root: {
        paddingTop: theme.spacing(3)
    },
}));

function Copyright() {

    const classes = useStyles();

    return (
        <div className={classes.root}>
            <Typography variant="body2" color="textSecondary" align="center">
                {'Copyright © '}
                <Link to="/">
                    Trade Union Committee React Web GUI
                </Link>
                {' '}
                {new Date().getFullYear()}
                {'.'}
            </Typography>
        </div>
    );
}

export default Copyright
