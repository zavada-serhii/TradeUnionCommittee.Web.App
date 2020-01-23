import React from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import { withStyles } from '@material-ui/core/styles';

import Copyright from '../Copyright.Component/Copyright'

const useStyles = theme => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
});

class Auth extends React.Component {

    constructor(props) {
        super(props);
        this.onInputTextChange = this.onInputTextChange.bind(this);
        this.onInputCheckboxChange = this.onInputCheckboxChange.bind(this);
        this.onFormSubmit = this.onFormSubmit.bind(this);
    }

    onInputTextChange(event) {
        this.props.setInputValue({key: event.target.name, value: event.target.value})
    }

    onInputCheckboxChange(event) {
        this.props.setInputValue({key: event.target.name, value: event.target.checked})
    }

    onFormSubmit(event) {
        event.preventDefault()
    }

    render() {

        const { classes, email, password, rememberMe } = this.props;

        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <div className={classes.paper}>
                    <Avatar className={classes.avatar}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Sign in
                    </Typography>
                    <form className={classes.form} onSubmit={this.onFormSubmit} noValidate>
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            label="Email Address"
                            name="email"
                            autoComplete="email"
                            autoFocus
                            value={email}
                            onChange={this.onInputTextChange} />
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            autoComplete="current-password"
                            value={password}
                            onChange={this.onInputTextChange} />
                        <FormControlLabel
                            control={<Checkbox name="rememberMe" color="primary" defaultChecked={rememberMe} onChange={this.onInputCheckboxChange} />}
                            label="Remember me" />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={classes.submit}>
                            Sign In
                        </Button>
                    </form>
                </div>
                <Box mt={8}>
                    <Copyright />
                </Box>
            </Container>
        );
    }
}

export default withStyles(useStyles)(Auth);
