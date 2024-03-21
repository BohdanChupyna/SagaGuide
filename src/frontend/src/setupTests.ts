// jest-dom adds custom jest matchers for asserting on DOM nodes.
// allows you to do things like:
// expect(element).toHaveTextContent(/react/i)
// learn more: https://github.com/testing-library/jest-dom
import '@testing-library/jest-dom';
import {SigninRedirectArgs} from "oidc-client-ts";
import {Router as RemixRouter} from "@remix-run/router/dist/router";
import {useNavigate} from "react-router-dom";
import {NavigateFunction} from "react-router/dist/lib/hooks";

process.env.REACT_APP_KEYCLOAC_AUTHORITY="http://localhost:8080/realms/gmspace";
process.env.REACT_APP_KEYCLOAC_CLIENT_ID="gmspacefrontend";
process.env.REACT_APP_KEYCLOAC_REDIRECT_URI="http://localhost:3000/home";
process.env.REACT_APP_BACKEND_URI="http://localhost:5258/api/v1";
process.env.REACT_APP_UNREGISTERED_CHARACTER_REDIRECT="http://localhost:3000/character";
process.env.REACT_APP_GOOGLE_ANALYTICS_ID="";

Object.defineProperties(window.HTMLElement.prototype, {
    offsetLeft: {
        get: function () {
            return parseFloat(window.getComputedStyle(this).marginLeft) || 0;
        }
    },
    offsetTop: {
        get: function () {
            return parseFloat(window.getComputedStyle(this).marginTop) || 0;
        }
    },
    offsetHeight: {
        get: function () {
            return parseFloat(window.getComputedStyle(this).height) || 800;
        }
    },
    offsetWidth: {
        get: function () {
            return parseFloat(window.getComputedStyle(this).width) || 800;
        }
    }
});

jest.setTimeout(10*1000);

jest.mock('react-oidc-context', () => ({
    // The result of this mock can be altered by changing `mockAuthenticatedStatus` in your test
    useAuth() {
        const { isLoading, isAuthenticated } = getMockAuthStatus();
        return {
            isLoading,
            isAuthenticated,
            signinRedirect: jest.fn(),
            removeUser: jest.fn(),
            settings: {},
        };
    },
}));

export interface IMockAuthenticatedStatus
{
    isLoading: boolean,
    isAuthenticated: boolean,
}

export const mockAuthenticatedStatus: IMockAuthenticatedStatus = {
    isLoading: false,
    isAuthenticated: false,
};

export const getMockAuthStatus = () => {
    return mockAuthenticatedStatus;
};

