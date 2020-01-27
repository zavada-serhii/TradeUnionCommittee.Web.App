import React from 'react';
import { connect } from 'react-redux';

import Auth from '../components/Auth.Component/Auth'
import { token } from '../actions/auth'

class AuthContainer extends React.Component {

    render() {
        const { token } = this.props;
        return (
           <Auth token={token}/>
        );
    }
}

// const putStateToProps = state => {
//     return {
//         email: state.auth.email,
//         password: state.auth.password,
//         rememberMe: state.auth.rememberMe,
//     };
// }

const putActionsToProps = {
    token
}

export default connect(null, putActionsToProps)(AuthContainer);
