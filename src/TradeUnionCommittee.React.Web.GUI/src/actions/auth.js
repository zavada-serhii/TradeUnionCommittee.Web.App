import axios from 'axios';
import jwtDecode from 'jwt-decode';
import ActionTypes from '../constants/actionTypes'
import { TOKEN } from '../constants/api'
import setAuthorizationToken from '../utils/setAuthorizationToken';

export const setCurrentUser = user => ({
  type: ActionTypes.SET_CURRENT_USER,
  payload: user
})

export function logout() {
  return dispatch => {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('token_type');

    setAuthorizationToken(false, false);
    dispatch(setCurrentUser({}));
  }
}

export function token(data) {
  return dispatch => {
    return axios.post(TOKEN, data).then(result => {

      const access_token = result.data.access_token;
      const token_type = result.data.token_type;

      localStorage.setItem('access_token', access_token);
      localStorage.setItem('refresh_token', result.data.refresh_token);
      localStorage.setItem('token_type', token_type);

      setAuthorizationToken(access_token, token_type);
      dispatch(setCurrentUser(jwtDecode(access_token)));
    });
  }
}
