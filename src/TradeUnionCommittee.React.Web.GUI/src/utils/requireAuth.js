import React from 'react';
import { connect } from 'react-redux';
import { withRouter } from "react-router-dom";
import { APP, AUTH } from '../constants/routes'

export default function (ComposedComponent) {

    class Authenticate extends React.Component {

        componentWillMount() {
            if (!this.props.isAuthenticated) {
                this.props.history.push(AUTH)
            }
        }

        componentWillUpdate(nextProps) {
            if (!nextProps.isAuthenticated) {
                this.props.history.push(APP)
            }
        }

        render() {
            return (
                <ComposedComponent {...this.props} />
            );
        }
    }

    const putStateToProps = state => {
        return { isAuthenticated: state.auth.isAuthenticated };
    }

    return connect(putStateToProps)(withRouter(Authenticate));
}
