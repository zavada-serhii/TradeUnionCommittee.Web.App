import ActionTypes from '../../constants/actionTypes'

const initialState = {
    email: '',
    password: '',
    rememberMe: false,
    clientType: 'WEB-APPLICATION'
}

export default function authReducer(state = initialState, action) {
    switch (action.type) {
        case ActionTypes.AUTH_CHANGE_INPUT_VALUE:
            return { ...state, [action.payload.key]: action.payload.value }
        default:
            return state
    }
}
