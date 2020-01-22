import { AUTH_CHANGE_EMAIL_TEXT, AUTH_CHANGE_PASSWORD_TEXT, AUTH_CHANGE_REMEMBER_ME_CHECKBOX } from '../../actions/Auth/actions'

const initialState = {
    email: '',
    password: '',
    rememberMe: false,
    clientType: 'WEB-APPLICATION'
}

export default function authReducer(state = initialState, action) {
    switch (action.type) {
        case AUTH_CHANGE_EMAIL_TEXT:
            return { ...state, email: action.payload }
        case AUTH_CHANGE_PASSWORD_TEXT:
            return { ...state, password: action.payload }
        case AUTH_CHANGE_REMEMBER_ME_CHECKBOX:
            return { ...state, rememberMe: action.payload }
        default:
            return state
    }
}
