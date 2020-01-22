import ActionTypes from '../../constants/actionTypes'

const initialState = {
    email: '',
    password: '',
    rememberMe: false,
    clientType: 'WEB-APPLICATION'
}

export default function authReducer(state = initialState, action) {
    switch (action.type) {
        case ActionTypes.AUTH_CHANGE_EMAIL_TEXT:
            return { ...state, email: action.payload }
        case ActionTypes.AUTH_CHANGE_PASSWORD_TEXT:
            return { ...state, password: action.payload }
        case ActionTypes.AUTH_CHANGE_REMEMBER_ME_CHECKBOX:
            return { ...state, rememberMe: action.payload }
        default:
            return state
    }
}
