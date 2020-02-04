import jwtDecode from 'jwt-decode';
import { refreshToken } from '../actions/auth'

export function checkTokenExpiration(store) {
    return (next) => (action) => {
        if (typeof action === 'function') {

            const access_token = localStorage.getItem('access_token');
            const refresh_token = localStorage.getItem('refresh_token');
            const token_type = localStorage.getItem('token_type');

            if (access_token && refresh_token && token_type) {
                if (jwtDecode(access_token).exp < Date.now() / 1000) {
                    return refreshToken(store.dispatch).then(() => next(action));
                }
            }
        }
        return next(action);
    };
}
