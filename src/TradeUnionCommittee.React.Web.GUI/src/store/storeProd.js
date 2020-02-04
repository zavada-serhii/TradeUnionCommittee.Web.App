import { createStore, applyMiddleware } from 'redux'
import ReduxThunk from 'redux-thunk'
import rootReducer from '../reducers'
import { checkTokenExpiration } from '../middlewares/checkTokenExpiration'

const middlewares = [checkTokenExpiration, ReduxThunk]
const enhancer = [applyMiddleware(...middlewares)]

export default function configureStore(initialState = {}) {
  return createStore(rootReducer, initialState, ...enhancer)
}
