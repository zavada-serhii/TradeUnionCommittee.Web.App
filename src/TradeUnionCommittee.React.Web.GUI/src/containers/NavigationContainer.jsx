import React from 'react';
import { connect } from 'react-redux';

import Navigation from '../components/Navigation.Component/Navigation'
import { logout } from '../actions/auth'

class NavigationContainer extends React.Component {

    render() {
        const { logout } = this.props;
        return (
           <Navigation logout={logout}/>
        );
    }
}

const putActionsToProps = {
    logout
}

export default connect(null, putActionsToProps)(NavigationContainer);
