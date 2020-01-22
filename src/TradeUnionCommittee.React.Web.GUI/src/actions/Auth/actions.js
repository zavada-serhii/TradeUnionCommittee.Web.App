import ActionTypes from '../../constants/actionTypes'

export const setInputValue = value => ({
    type: ActionTypes.AUTH_CHANGE_INPUT_VALUE,
    payload: value
})
