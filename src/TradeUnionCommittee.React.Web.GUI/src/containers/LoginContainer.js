import React from 'react'
import { connect } from 'react-redux'

import Login from '../components/Login.Component/Login'
import { login } from '../actions/login';

class LoginContainer extends React.Component {

    state = {
        email: "",
        password: "",
        clientType: "WEB-APPLICATION"
    }

    handleChange = event => {
        this.setState({[event.target.name]: event.target.value});
    }

    handleSubmit = event => {
        event.preventDefault()
        this.props.login(this.state).then((result) => {
            if(result.isSucceeded){
                alert('Succeeded')
            }
            else {
                alert(result.errorList)
            }
        })
    }

    render() {
        return (
            <Login
                state={this.state}
                handleChange={this.handleChange}
                handleSubmit={this.handleSubmit}/>
        )
    }
}

const mapDispatchToProps = dispatch => ({
    login: userInfo => dispatch(login(userInfo))
})

export default connect(null, mapDispatchToProps)(LoginContainer);
