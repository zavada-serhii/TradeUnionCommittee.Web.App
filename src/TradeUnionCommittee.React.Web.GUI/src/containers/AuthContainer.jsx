import React from 'react';
import { connect } from 'react-redux';

import Auth from '../components/Auth.Component/Auth'
import { setInputValue } from '../actions/Auth/actions'

class AuthContainer extends React.Component {

    render() {

        const { email, password, rememberMe, setInputValue } = this.props;

        return (
           <Auth
                email={email}
                password={password}
                rememberMe={rememberMe} 
                setInputValue={setInputValue} />
        );
    }
}

const putStateToProps = state => {
    return {
        email: state.auth.email,
        password: state.auth.password,
        rememberMe: state.auth.rememberMe,
    };
}

const putActionsToProps = {
    setInputValue
}

export default connect(putStateToProps, putActionsToProps)(AuthContainer);
