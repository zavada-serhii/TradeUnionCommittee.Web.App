import React from 'react';
import { connect } from 'react-redux';

import Auth from '../components/Auth.Component/Auth'
import { setInputValue } from '../actions/Auth/actions'

class AuthContainer extends React.Component {

    render() {
        return (
           <Auth
                email={this.props.email}
                password={this.props.password}
                rememberMe={this.props.rememberMe} 
                setInputValue={this.props.setInputValue} />
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
    setInputValue
}

export default connect(mapStateToProps, mapDispatchToProps)(AuthContainer);
