import {
    AnyAction,
    combineReducers,
    configureStore,
    ThunkAction
} from '@reduxjs/toolkit'

import type { PreloadedState } from '@reduxjs/toolkit'

import {currentCharacterSlice} from "./slices/currentCharacter/currentCharacterSlice";
import {apiSlice} from "./slices/api/apiSlice";
import {navBarSlice} from "./slices/navBar/navBarSlice";
import {toastsSlice} from "./slices/toasts/toastsSlice";

// Create the root reducer separately so we can extract the RootState type
const rootReducer = combineReducers({
    navBar: navBarSlice.reducer,
    currentCharacter: currentCharacterSlice.reducer,
    toasts: toastsSlice.reducer,
    [apiSlice.reducerPath]: apiSlice.reducer,
})

export const setupStore = (preloadedState?: PreloadedState<RootState>) => {
    return configureStore({
        reducer: rootReducer,
        preloadedState,
        middleware: getDefaultMiddleware =>
            getDefaultMiddleware().concat(apiSlice.middleware)
    });
}

export const sagaGuideStore: AppStore = setupStore();

export type AppStore = ReturnType<typeof setupStore>
export type RootState = ReturnType<typeof rootReducer>
export type AppDispatch = AppStore['dispatch']

export type AppThunk<ReturnType = void> = ThunkAction<void, RootState, unknown, AnyAction>;