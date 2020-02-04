import React from 'react';
import { connect } from 'react-redux';

import Menu from '../components/Menu.Component/Menu'

class MenuContainer extends React.Component {

    render() {
        return (
           <Menu /> 
        );
    }
}

export default connect(null, null)(MenuContainer);
