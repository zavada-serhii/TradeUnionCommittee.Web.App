import React from 'react';
import { connect } from 'react-redux';
import { getAllPositions } from '../../actions/position'

import Position from '../../components/Position.Component/Position'

class PositionContainer extends React.Component {

    render() {
        const { positions, getAllPositions } = this.props;
        return (
            <Position positions={positions}
                      getAllPositions={getAllPositions} />
        );
    }
}

const putStateToProps = state => {
    return { positions: state.position.positions };
}

const putActionsToProps = {
    getAllPositions
}

export default connect(putStateToProps, putActionsToProps)(PositionContainer);
