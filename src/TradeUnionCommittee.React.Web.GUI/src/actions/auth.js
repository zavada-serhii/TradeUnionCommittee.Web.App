import axios from 'axios';
import jwtDecode from 'jwt-decode';
import ActionTypes from '../constants/actionTypes'
import { TOKEN, REFRESH_TOKEN } from '../constants/api'
import setAuthorizationToken from '../utils/setAuthorizationToken';

const CLIENT_TYPE = "WEB-APPLICATION";

export const setCurrentUser = user => ({
  type: ActionTypes.SET_CURRENT_USER,
  payload: user
})

export function logout() {

  localStorage.removeItem('access_token');
  localStorage.removeItem('refresh_token');
  localStorage.removeItem('token_type');

  return dispatch => {
    setAuthorizationToken(false, false);
    dispatch(setCurrentUser({}));
  }
}

export function token(model) {
  return async dispatch => {
    const response = await axios.post(TOKEN, { ...model, clientType: CLIENT_TYPE });
    return saveToken(response, dispatch);
  }
}

export async function refreshToken(dispatch) {
  const model = { clientType: CLIENT_TYPE, refreshToken: localStorage.getItem('refresh_token') }
  const response = await axios.post(REFRESH_TOKEN, model);
  return saveToken(response, dispatch);
}

function saveToken(response, dispatch) {

  const access_token = response.data.access_token;
  const token_type = response.data.token_type;

  localStorage.setItem('access_token', access_token);
  localStorage.setItem('refresh_token', response.data.refresh_token);
  localStorage.setItem('token_type', token_type);

  setAuthorizationToken(access_token, token_type);
  dispatch(setCurrentUser(jwtDecode(access_token)));
}
