import ActionTypes from '../../constants/actionTypes'
import isEmpty from 'lodash/isEmpty';

const initialState = {
  isAuthenticated: false,
  user: {}
};

export default function authReducer(state = initialState, action) {
  switch (action.type) {
    case ActionTypes.SET_CURRENT_USER:
      return {
        isAuthenticated: !isEmpty(action.payload),
        user: action.payload
      };
    default: return state;
  }
}
