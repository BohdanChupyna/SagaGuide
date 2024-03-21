import React, {PropsWithChildren} from 'react'
import {render} from '@testing-library/react'
import type {RenderOptions} from '@testing-library/react'
import type {PreloadedState} from '@reduxjs/toolkit'
import {Provider} from 'react-redux'
import {AppStore, sagaGuideStore, RootState, setupStore} from "../redux/store";
import {RouterProvider} from "react-router-dom";
import {CreateMemoryRouterNavigatedToSpecificPage, sgBrowserRouter} from "../routerSettings";
import {frodoCharacter} from "./characterTestData";
import {mockAuthenticatedStatus} from "../setupTests";
import {SnackbarProvider} from "notistack";
import {ThemeProvider} from "@mui/material/styles";
import SagaGuideTheme from "../components/sgTheme";


// This type interface extends the default options for render from RTL, as well
// as allows the user to specify other things such as initialState, store.
export interface ExtendedRenderOptions extends Omit<RenderOptions, 'queries'> {
    preloadedState?: PreloadedState<RootState>
    store?: AppStore
}

export function renderWithProviders(
    ui: React.ReactElement,
    {
        // @ts-ignore
        preloadedState = {},
        // Automatically create a store instance if no store was passed in
        store = setupStore(preloadedState),
        ...renderOptions
    }: ExtendedRenderOptions = {}
) 
{
    function Wrapper({children}: PropsWithChildren<{}>): JSX.Element {
        return (
            <React.StrictMode>
                <Provider store={store}>
                    <SnackbarProvider>
                        <ThemeProvider theme={SagaGuideTheme()}>
                            {children}
                        </ThemeProvider>
                    </SnackbarProvider>
                </Provider>
            </React.StrictMode>
        )
    }

    return {store, ...render(ui, {wrapper: Wrapper, ...renderOptions})}
}

export const FrodoCharacterPage = () => {
    mockAuthenticatedStatus.isAuthenticated = true;
    return CustomPage(`/character/${frodoCharacter.id}`);
}

export const CustomPage = (pageUrl: string) => {
    return (
        <RouterProvider router={CreateMemoryRouterNavigatedToSpecificPage(pageUrl)} />
    );
}