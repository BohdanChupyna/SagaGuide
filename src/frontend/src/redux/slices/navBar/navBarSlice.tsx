import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {RootState} from "../../store";



// Define a type for the slice state
export interface INavBarState {
    isNavBarOpen: boolean
}

// Define the initial state using that type
const initialState: INavBarState = {
    isNavBarOpen: false
}

export const navBarSlice = createSlice({
    name: 'NavBar',
    // `createSlice` will infer the state type from the `initialState` argument
    initialState,
    reducers: {
        openNavBar: state => {
            state.isNavBarOpen = true
        },
        closeNavBar: state => {
            state.isNavBarOpen = false
        }
    }
})

export const { openNavBar, closeNavBar } = navBarSlice.actions

// Other code such as selectors can use the imported `RootState` type
export const selectIsNavBarOpen = (state: RootState) => state.navBar.isNavBarOpen

export default navBarSlice.reducer