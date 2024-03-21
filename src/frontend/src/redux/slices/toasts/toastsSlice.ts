import {AnyAction, createSlice, PayloadAction, ThunkAction} from '@reduxjs/toolkit'
import {AppThunk, RootState} from "../../store";
import {v4 as uuidv4} from "uuid";
import {constructToast, ISgToast, ISgToastTypeEnum} from "./IToastProviderSlice";


// Define a type for the slice state
export interface ISgToastsState {
   toasts: ISgToast[]
}

// Define the initial state using that type
const initialState: ISgToastsState = {
    toasts: []
}

export const toastsSlice = createSlice({
    name: 'sgToastsSlice',
    // `createSlice` will infer the state type from the `initialState` argument
    initialState,
    reducers: {
        createToast: (state, action: PayloadAction<[string, ISgToastTypeEnum]>) => {
            const [message, messageType] = action.payload;
            _createToastImpl(state, message, messageType);
        },
        enqueueToast: (state, action: PayloadAction<ISgToast>) => {
            state.toasts = [
                ...state.toasts,
                action.payload
            ];
        },
        removeToast: (state, action: PayloadAction<string>) => {
            state.toasts = state.toasts.filter(n => n.id !== action.payload)
        },
        hideToast: (state, action: PayloadAction<string|null>) => {
            let hideAll = action.payload === null;
            state.toasts = state.toasts.map(notification => (
                (hideAll || notification.id === action.payload)
                    ? { ...notification, isShown: true }
                    : { ...notification }));
        },
    }
})

export function _createToastImpl(state: ISgToastsState, message:string, messageType: ISgToastTypeEnum)
{
    let toast = constructToast(message, messageType);

    state.toasts = [
        ...state.toasts,
        toast
    ];
}

export const {
    createToast,
    removeToast,
    hideToast,
    enqueueToast,
} = toastsSlice.actions;

export const selectToasts = (state: RootState) => state.toasts.toasts;

export function grabToastWrapper(action: AnyAction, toastSelector: (state: RootState) => ISgToast|null, cleanUpAction: AnyAction): AppThunk {
    return (dispatch, getState) => {
        

        dispatch(action)
        const state = getState();
        let toast = toastSelector(state);
        if(toast)
        {
            dispatch(enqueueToast(toast));
            dispatch(cleanUpAction);
        }
    }
}