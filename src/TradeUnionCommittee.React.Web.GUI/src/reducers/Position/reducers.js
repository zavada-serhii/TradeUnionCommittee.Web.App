import ActionTypes from '../../constants/actionTypes'

const initialState = {
    positions: [],
    position: {}
};

export default function positionReducer(state = initialState, action) {
    switch (action.type) {
        case ActionTypes.SET_ALL_POSITIONS:
            return { ...initialState, positions: action.payload }
        case ActionTypes.SET_POSITION:
            return { ...initialState, position: action.payload }
        default: return state;
    }
}
