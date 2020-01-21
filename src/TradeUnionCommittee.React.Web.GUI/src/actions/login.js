
import { TOKEN } from '../constants/api'

export const login = user => {
  return dispatch => {
    return fetch(TOKEN, {
      method: "POST",
      headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
      },
      body: JSON.stringify(user)
    })
      .then(response => response.json().then((data) => {
        if (response.ok) {
          localStorage.setItem("access_token", data.access_token)
          localStorage.setItem("refresh_token", data.refresh_token)
          localStorage.setItem("expires_in", data.expires_in)
          localStorage.setItem("token_type", data.token_type)
          localStorage.setItem("user_name", data.user_name)
          localStorage.setItem("user_role", data.user_role)
          dispatch(loginUser(data))
          return { isSucceeded : true }
        }
        else {
          return { isSucceeded : false, errorList: data }
        }
      }));
  }
}

export const getProfileFetch = () => {
  return dispatch => {
    const token = localStorage.token;
    if (token) {
      return fetch("http://localhost:3000/api/v1/profile", {
        method: "GET",
        headers: {
          'Content-Type': 'application/json',
          Accept: 'application/json',
          'Authorization': `Bearer ${token}`
        }
      })
        .then(resp => resp.json())
        .then(data => {
          if (data.message) {
            // Будет ошибка если token не дествительный
            localStorage.removeItem("token")
          } else {
            dispatch(loginUser(data.user))
          }
        })
    }
  }
}

export const loggedIn = () => {
  const expirationTime = localStorage.getItem('expires_in')
  return new Date().getTime() / 1000 < parseInt(expirationTime, 10)
}

const loginUser = userObj => ({
  type: 'LOGIN_USER',
  payload: userObj
})
