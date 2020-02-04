import React from 'react';
import { connect } from 'react-redux';

import NotFound from '../components/NotFound.Component/NotFound'

class NotFoundContainer extends React.Component {

    render() {
        return (
           <NotFound />
        );
    }
}

export default connect(null, null)(NotFoundContainer);
