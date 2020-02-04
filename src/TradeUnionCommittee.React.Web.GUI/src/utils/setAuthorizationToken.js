import axios from 'axios';

export default function setAuthorizationToken(access_token, token_type) {
  if (access_token && token_type) {
    axios.defaults.headers.common['Authorization'] = `${token_type} ${access_token}`;
  } else {
    delete axios.defaults.headers.common['Authorization'];
  }
}
