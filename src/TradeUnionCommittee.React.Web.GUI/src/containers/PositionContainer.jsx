import React from 'react';
import { connect } from 'react-redux';

import Position from '../components/Position.Component/Position'

class PositionContainer extends React.Component {

    render() {
        return (
           <Position /> 
        );
    }
}

export default connect(null, null)(PositionContainer);
