import { combineReducers } from 'redux'
import counterReducer from './Counter/reducers'
import authReducer from './Auth/reducers'
import positionReducer from './Position/reducers'

const rootReducer = combineReducers({
  counter: counterReducer,
  auth: authReducer,
  position: positionReducer
})

export default rootReducer
