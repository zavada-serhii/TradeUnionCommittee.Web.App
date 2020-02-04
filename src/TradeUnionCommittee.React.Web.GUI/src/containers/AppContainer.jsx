import React from 'react';
import { connect } from 'react-redux';

import App from '../components/App.Component/App'

class AppContainer extends React.Component {

    render() {
        return (
           <App />
        );
    }
}

export default connect(null, null)(AppContainer);
