import React from 'react';
import { connect } from 'react-redux';

import CreatePosition from '../../components/Position.Component/CreatePosition'

class CreatePositionContainer extends React.Component {

    render() {
        return (
            <CreatePosition />
        );
    }
}

export default connect(null, null)(CreatePositionContainer);
