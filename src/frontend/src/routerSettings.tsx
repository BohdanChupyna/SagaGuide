import React from "react";
import App from "./App";
import {
    createBrowserRouter,
    createMemoryRouter,
    Navigate,
    RouteObject,
    useLocation,
    useNavigate
} from "react-router-dom";
import Home from "./pages/Home";
import Error from "./pages/Error/Error";
import Character from "./pages/character/Character";
import CharactersPage from "./pages/characters/CharactersPage";
import CharacterCreationPage from "./pages/characterCreation/CharacterCreation";

const routes: RouteObject[] = [
    {
        path: "/",
        element: <App/>,
        errorElement: <Navigate replace to="error" />,
        children: [
            { 
                index: true,
                path: "home",
                element: <Home /> 
            },
            {
                path: "*",
                element: <Navigate replace to="home" />,
            },
            {
                path: "",
                element: <Navigate replace to="home" />,
            },
            {
                path: "characters",
                element:  <CharactersPage />,
            },
            {
                path: "character",
                element: <CharacterCreationPage />,
            },
            {
                path: "character/:characterId",
                element: <Character />,
            },
            {
                path: "error",
                element: <Error />,
            },
        ],
    },
];

export const sgBrowserRouter = createBrowserRouter(routes);
export const sgMemoryRouter = createMemoryRouter(routes);
export function CreateMemoryRouterNavigatedToSpecificPage(pageUrl: string)
{
    return createMemoryRouter(routes, {initialEntries: [{pathname: pageUrl}]});
}