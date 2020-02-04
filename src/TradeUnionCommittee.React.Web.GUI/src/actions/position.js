import axios from 'axios';
import ActionTypes from '../constants/actionTypes'
import { GET_ALL_POSITIONS } from '../constants/api'

export const setAllPositions = positions => ({
    type: ActionTypes.SET_ALL_POSITIONS,
    payload: positions
})

export function getAllPositions() {
    return dispatch => axios.get(GET_ALL_POSITIONS).then(result => {
        dispatch(setAllPositions(result.data));
    })
}