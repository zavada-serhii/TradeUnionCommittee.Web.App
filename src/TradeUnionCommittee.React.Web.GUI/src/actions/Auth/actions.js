import ActionTypes from '../../constants/actionTypes'

export const setEmailText = email => ({
    type: ActionTypes.AUTH_CHANGE_EMAIL_TEXT,
    payload: email
})

export const setPasswordText = password => ({
    type: ActionTypes.AUTH_CHANGE_PASSWORD_TEXT,
    payload: password
})

export const setRememberMeCheckbox = rememberMe => ({
    type: ActionTypes.AUTH_CHANGE_REMEMBER_ME_CHECKBOX,
    payload: rememberMe
})
