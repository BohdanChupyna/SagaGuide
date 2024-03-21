import * as React from 'react';
import Toolbar from '@mui/material/Toolbar';
import {AppBar, Box, IconButton, Tooltip, useMediaQuery} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import { useTheme } from '@mui/material/styles';
import Typography from "@mui/material/Typography";
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import MenuBookRoundedIcon from '@mui/icons-material/MenuBookRounded';
import {useAppDispatch, useAppSelector } from '../../redux/hooks';
import {SgAppBarAriaLabels} from "./SgAppBarAriaLabels";
import {closeNavBar, openNavBar, selectIsNavBarOpen} from "../../redux/slices/navBar/navBarSlice";
import {
    closeRuleBook,
    openRuleBook, selectCharacter,
    selectIsRuleBookOpen
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import SgAppBarSaveCharacterButton from "./SgAppBarSaveCharacterButton";
import { IsOnCharacterPage } from '../../routerUtils';
import SgAppBarDeleteCharacterButton from "./SgAppBarDeleteCharacterButton";
import SgNotificationsView from "../sgNotificationsView/SgNotoficationsView";
import SgAccountMenu from "./SgAccountMenu";
import {useAuth} from "react-oidc-context";
import UkraineFlag from "../sgIcons/ukraine-flag.png"

const SgAppBar = () =>
{
    SgNotificationsView();
    const isNavBarOpen = useAppSelector(selectIsNavBarOpen);
    const isRuleBookOpen = useAppSelector(selectIsRuleBookOpen);
    const character = useAppSelector(selectCharacter);
    const theme = useTheme();
    const onlySmallScreen = useMediaQuery(theme.breakpoints.down("sm"));
    const dispatch = useAppDispatch();
    const auth = useAuth();
    
    let isCharacterPage = IsOnCharacterPage();
    
    return (
            <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1, display: "flex" }}>
                <Toolbar>
                    <IconButton
                        color="inherit"
                        aria-label={SgAppBarAriaLabels.OpenNavBarButton}
                        onClick={() => dispatch(openNavBar())}
                        sx={{...(isNavBarOpen && { display: 'none' }) }}
                    >
                        <MenuIcon />
                    </IconButton>
                    <IconButton
                        color="inherit"
                        aria-label={SgAppBarAriaLabels.CloseNavBarButton}
                        onClick={() => dispatch(closeNavBar())}
                        sx={{...(!isNavBarOpen && { display: 'none' }) }}
                    >
                        <ChevronLeftIcon />
                    </IconButton>
                
                    <Typography variant="h6" noWrap component="div">
                        Saga Guide
                    </Typography>
                    <Box>
                        <img src={UkraineFlag} alt="UkraineFlag" width={'48px'} height={'48px'}/>
                    </Box>
                    
                    <Box sx={{ flexGrow: 1 }}>
                        {!auth.isAuthenticated && !onlySmallScreen && (
                            <Typography variant="h6" noWrap component="div" textAlign={"center"}>
                                Please sign in to use Saga Guide
                            </Typography>)}
                    </Box>
                    
                    {isCharacterPage && (
                        <SgAppBarDeleteCharacterButton/>
                    )}

                    {isCharacterPage && (
                        <SgAppBarSaveCharacterButton/>
                    )}
                    
                    {isCharacterPage && (
                        <Tooltip title={"Open rulebook"}>
                            <IconButton
                            color="inherit"
                            aria-label={SgAppBarAriaLabels.OpenRuleBookButton}
                            onClick={() => dispatch(openRuleBook())}
                            sx={{ ...(isRuleBookOpen && { display: 'none' }) }}
                            >
                                <MenuBookRoundedIcon />
                            </IconButton>
                        </Tooltip>)}
                    {isCharacterPage && (
                        <Tooltip title={"Close rulebook"}>
                            <IconButton
                            color="inherit"
                            aria-label={SgAppBarAriaLabels.CloseRuleBookButton}
                            onClick={() => dispatch(closeRuleBook())}
                            sx={{...(!isRuleBookOpen && { display: 'none' }) }}
                            >
                                <ChevronRightIcon />
                            </IconButton>
                        </Tooltip>)}

                    <SgAccountMenu/>
                    
                </Toolbar>
            </AppBar>
    )
}

export default SgAppBar;