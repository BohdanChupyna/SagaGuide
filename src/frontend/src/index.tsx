import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';
import {RouterProvider} from "react-router-dom";
import {sgBrowserRouter} from "./routerSettings";
import {AuthProvider, AuthProviderProps} from "react-oidc-context";
import {WebStorageStateStore} from "oidc-client-ts";
import ReactGA from "react-ga4";
import {sagaGuideStore} from "./redux/store";
import {Provider} from "react-redux";
import SagaGuideTheme from "./components/sgTheme";
import {SnackbarProvider} from "notistack";
import {ThemeProvider} from "@mui/material/styles";

// Initialize React Ga with your tracking ID
ReactGA.initialize(process.env.REACT_APP_GOOGLE_ANALYTICS_ID as string);

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

const oidcConfig: AuthProviderProps = {
    authority: process.env.REACT_APP_KEYCLOAC_AUTHORITY as string,
    client_id: process.env.REACT_APP_KEYCLOAC_CLIENT_ID as string,
    redirect_uri: process.env.REACT_APP_KEYCLOAC_REDIRECT_URI as string,
    automaticSilentRenew: true,
    onSigninCallback() {
        // You must provide an implementation of onSigninCallback to oidcConfig to remove the payload
        // from the URL upon successful login.
        // Otherwise if you refresh the page and the payload is still there, signinSilent - which handles renewing your token - won't work.

        console.log("onSigninCallback()");
        window.history.replaceState(
            {},
            document.title,
            window.location.pathname
        );
    },
    userStore: new WebStorageStateStore({ store: window.localStorage }),
};

root.render(
    <React.StrictMode>
        <AuthProvider {...oidcConfig}>
            <Provider store={sagaGuideStore}>
                <SnackbarProvider>
                    <ThemeProvider theme={SagaGuideTheme()}>
                        <RouterProvider router={sgBrowserRouter} />
                    </ThemeProvider>
                </SnackbarProvider>
            </Provider>
        </AuthProvider>  
    </React.StrictMode>);

reportWebVitals();


