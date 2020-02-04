import React from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import { withStyles } from '@material-ui/core/styles';
import { withRouter } from "react-router-dom";

import { APP } from '../../constants/routes'
import CopyrightContainer from '../../containers/CopyrightContainer'

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

        this.state = {
            email: '',
            password: ''
        };

        this.onTextChange = this.onTextChange.bind(this);
        this.onFormSubmit = this.onFormSubmit.bind(this);
    }

    onTextChange(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    onFormSubmit(event) {
        event.preventDefault()

        this.props.token(this.state).then(
            (result) => this.props.history.push(APP),
            (error) => alert(`Wrong login or pawword`)
        );
    }

    render() {

        const { classes } = this.props;
        const { email, password } = this.state;

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
                            onChange={this.onTextChange} />
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
                            onChange={this.onTextChange} />
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
                    <CopyrightContainer />
                </Box>
            </Container>
        );
    }
}

export default withStyles(useStyles)(withRouter(Auth))
