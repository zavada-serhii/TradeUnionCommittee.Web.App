export const AUTH_CHANGE_EMAIL_TEXT = 'AUTH_CHANGE_EMAIL_TEXT'
export const AUTH_CHANGE_PASSWORD_TEXT = 'AUTH_CHANGE_PASSWORD_TEXT'
export const AUTH_CHANGE_REMEMBER_ME_CHECKBOX = 'AUTH_CHANGE_REMEMBER_ME_CHECKBOX'

export const setEmailText = email => ({
    type: AUTH_CHANGE_EMAIL_TEXT,
    payload: email
})

export const setPasswordText = password => ({
    type: AUTH_CHANGE_PASSWORD_TEXT,
    payload: password
})

export const setRememberMeCheckbox = rememberMe => ({
    type: AUTH_CHANGE_REMEMBER_ME_CHECKBOX,
    payload: rememberMe
})
