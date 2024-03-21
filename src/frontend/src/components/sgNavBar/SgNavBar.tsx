import * as React from 'react';
import Drawer from '@mui/material/Drawer';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import {NavigateFunction, useNavigate} from "react-router-dom";
import Grid from "@mui/material/Grid";
import {useMediaQuery} from "@mui/material";
import { styled, useTheme } from '@mui/material/styles';
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {AppDispatch} from "../../redux/store";
import {closeNavBar, selectIsNavBarOpen} from "../../redux/slices/navBar/navBarSlice";
import {sgNavbarItems, IMainNavbarItem} from './sgNavBarItems';
import {useAuth} from "react-oidc-context";



const SgNavBar = () =>
{
    const isNavBarOpen = useAppSelector(selectIsNavBarOpen);
    const dispatch = useAppDispatch();
    
    const theme = useTheme();
    const onlySmallScreen = useMediaQuery(theme.breakpoints.down("md"));
    const navigate = useNavigate();
    const auth = useAuth();
    
    let navbarItems: Array<IMainNavbarItem> = sgNavbarItems;
    
    if(!auth.isAuthenticated)
    {
        navbarItems = navbarItems.filter(item => !item.needAuth);
    }
    
    if(onlySmallScreen)
    {
        return SmallNavbar(navbarItems, isNavBarOpen, dispatch, navigate);
    }
    
    return BigNavbar(navbarItems, isNavBarOpen, dispatch, navigate);
}

const SmallNavbar = (navbarItems: Array<IMainNavbarItem>, isNavBarOpen: boolean, dispatch: AppDispatch, navigate: NavigateFunction) =>
{
    let onClickCallBack = (item: IMainNavbarItem) => {
        navigate(item.route);
        dispatch(closeNavBar());
    };
    
    return (
        <Drawer
            anchor={"top"}
            variant={ "temporary"}
            open={isNavBarOpen}
            onClose={() => dispatch(closeNavBar())}
        >
            <Toolbar />
            <Divider />
            <List>
                {navbarItems.map((item, index) => (
                    NavBarListItem(item, index, onClickCallBack)
                ))}
            </List>
        </Drawer>
    )
}

const drawerWidth = 240;

export const AppBarHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
    justifyContent: 'flex-end',
}));

export const PageContent = styled(Grid, { shouldForwardProp: (prop) => prop !== 'open' })<{
    open?: boolean;
}>(({ theme, open }) => 
    {
        let pageContentMarginLeft = drawerWidth - parseInt(theme.spacing(1));
        
    return ({
    flexGrow: 1,
    padding: 0,
    margin: theme.spacing(1),
    transition: theme.transitions.create('margin', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    
    
    [theme.breakpoints.up("md")]: {
        marginLeft: `-${pageContentMarginLeft}px`,
        //maxWidth: "1000px",

        ...(open && {
            transition: theme.transitions.create('margin', {
                easing: theme.transitions.easing.easeOut,
                duration: theme.transitions.duration.enteringScreen,
            }),
            marginLeft: theme.spacing(1),
        }),
    },
})});


const BigNavbar = (navbarItems: Array<IMainNavbarItem>, isNavBarOpen: boolean, dispatch: AppDispatch, navigate: NavigateFunction) =>
{
    let onClickCallBack = (item: IMainNavbarItem) => navigate(item.route);
    return (
        <Drawer
            sx={{
                width: drawerWidth,
                flexShrink: 0,
                '& .MuiDrawer-paper': {
                    width: drawerWidth,
                    boxSizing: 'border-box',
                },
            }}
            variant="persistent"
            anchor="left"
            open={isNavBarOpen}
        >
            <AppBarHeader/>
            <List>
                {navbarItems.map((item, index) => (
                    NavBarListItem(item, index, onClickCallBack)
                ))}
            </List>
        </Drawer>
    )
}

const NavBarListItem = (item: IMainNavbarItem, index: number, onClickCallBack: Function) =>
{
    return (<ListItem key={item.id}>
        <ListItemButton onClick={() => onClickCallBack(item)}
                        aria-label={item.ariaLabel}>
            <ListItemIcon>
                {item.icon}
            </ListItemIcon>
            <ListItemText primary={item.label} />
        </ListItemButton>
    </ListItem>);
}

export default SgNavBar;