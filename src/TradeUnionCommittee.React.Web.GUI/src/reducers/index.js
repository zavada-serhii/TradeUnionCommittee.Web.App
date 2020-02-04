import { combineReducers } from 'redux'
import authReducer from './Auth/reducers'
import positionReducer from './Position/reducers'

const rootReducer = combineReducers({
  auth: authReducer,
  position: positionReducer
})

export default rootReducer
