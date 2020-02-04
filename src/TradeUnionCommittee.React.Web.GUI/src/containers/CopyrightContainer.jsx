import React from 'react';
import { connect } from 'react-redux';

import Copyright from '../components/Copyright.Component/Copyright'

class CopyrightContainer extends React.Component {

    render() {
        return (
           <Copyright />
        );
    }
}

export default connect(null, null)(CopyrightContainer);
