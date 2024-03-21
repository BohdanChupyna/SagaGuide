import React, {useEffect} from 'react';
import './App.css';
import {Outlet, useLocation} from "react-router-dom";
import SgAppBar from "./components/sgAppBar/SgAppBar";
import SgSpaceNavBar from "./components/sgNavBar/SgNavBar";
import { Box } from '@mui/material';
import ReactGA from "react-ga4";

function App() {
    const location = useLocation();
    useEffect(() => {
        ReactGA.send({ hitType: "pageview", page: location.pathname + location.search, title: "Custom Title" });
    }, [location]);
    
    return (
        <Box sx={{ display: 'flex' }}>
            <SgAppBar />
            <SgSpaceNavBar />
            <Outlet />
        </Box>
    );
}

export default App;
