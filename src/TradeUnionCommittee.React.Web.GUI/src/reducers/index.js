import { combineReducers } from 'redux'
import counterReducer from './Counter/reducers'
import authReducer from './Auth/reducers'

const rootReducer = combineReducers({
  counter: counterReducer,
  auth: authReducer
})

export default rootReducer
