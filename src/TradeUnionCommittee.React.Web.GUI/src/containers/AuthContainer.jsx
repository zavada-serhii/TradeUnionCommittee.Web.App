import React from 'react';
import { connect } from 'react-redux';

import Auth from '../components/Auth.Component/Auth'
import { setEmailText, setPasswordText, setRememberMeCheckbox } from '../actions/Auth/actions'

class AuthContainer extends React.Component {

    render() {
        return (
           <Auth
                email={this.props.email}
                password={this.props.password}
                rememberMe={this.props.rememberMe} 
                setEmailText={this.props.setEmailText}
                setPasswordText={this.props.setPasswordText}
                setRememberMeCheckbox={this.props.setRememberMeCheckbox} />
        );
    }
}

const mapStateToProps = state => {
    return {
        email: state.auth.email,
        password: state.auth.password,
        rememberMe: state.auth.rememberMe,
    };
}

const mapDispatchToProps = {
    setEmailText,
    setPasswordText,
    setRememberMeCheckbox
}

export default connect(mapStateToProps, mapDispatchToProps)(AuthContainer);
