import React from 'react';
import { connect } from 'react-redux';

import Content from '../components/Content.Component/Content'

class ContentContainer extends React.Component {

    render() {
        return (
           <Content />
        );
    }
}

export default connect(null, null)(ContentContainer);
