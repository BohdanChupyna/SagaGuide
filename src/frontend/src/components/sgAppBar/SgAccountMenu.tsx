import {Avatar, Box, IconButton, Menu, MenuItem, Tooltip} from "@mui/material";
import * as React from "react";
import {useAuth} from "react-oidc-context";
import {Logout, AccountCircleRounded, Settings} from "@mui/icons-material";
import ListItemIcon from "@mui/material/ListItemIcon";
import {useEffect} from "react";
import {enqueueToast} from "../../redux/slices/toasts/toastsSlice";
import {constructErrorToast, constructInfoToast} from "../../redux/slices/toasts/IToastProviderSlice";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {selectCharacter} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import { unregisteredCharacterLocalStorageKey } from "../constants";
import {SgAppBarAriaLabels} from "./SgAppBarAriaLabels";

const SgAccountMenu = () => {
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const unregisteredCharacter = useAppSelector(selectCharacter);
    const open = Boolean(anchorEl);
    const auth = useAuth();
    const dispatch = useAppDispatch();
    
    useEffect(() => {
        if(auth.error)
        {
            dispatch(enqueueToast(constructErrorToast(`Failed to sing in. Reason: ${auth.error.message}`)));
        }
    }, [auth.error])
    
    switch (auth.activeNavigator) {
        case "signinSilent":
            return <div>Signing you in...</div>;
        case "signoutRedirect":
            return <div>Signing you out...</div>;
    }

    const handleAuthenticate = async () => {
        if(unregisteredCharacter)
        {
            window.localStorage.setItem(unregisteredCharacterLocalStorageKey, JSON.stringify(unregisteredCharacter));
            await auth.signinRedirect({redirect_uri: `${process.env.REACT_APP_UNREGISTERED_CHARACTER_REDIRECT as string}`});
            return;
        }

       await auth.signinRedirect();
    };
    
    if (auth.isLoading) {
        return <div>Loading...</div>;
    }
    
    if (!auth.isAuthenticated) {
        return (
            <Tooltip title="Sign in">
                <IconButton aria-label={SgAppBarAriaLabels.AccountSignInButton} onClick={handleAuthenticate}>
                    <AccountCircleRounded style={{color: 'white'}}/>
                </IconButton>
            </Tooltip>
        );
    }

    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    const handleSignOut = () => {
        auth.signoutRedirect();
        handleClose();
    };
    
    const handleSettings = () =>
    {
        let authority = process.env.REACT_APP_KEYCLOAC_AUTHORITY as string;
        window.location.href = `${authority}/account/?`;
    }

    return (
        <React.Fragment>
            <Box sx={{display: 'flex', alignItems: 'center', textAlign: 'center'}}>
                <Tooltip
                    title={`${auth.user?.profile.preferred_username ? auth.user?.profile.preferred_username : "user"} account settings`}>
                    <IconButton
                        onClick={handleClick}
                        size="small"
                        sx={{ml: 2}}
                        aria-controls={open ? 'account-menu' : undefined}
                        aria-haspopup="true"
                        aria-expanded={open ? 'true' : undefined}
                    >
                        <Avatar>{auth.user?.profile.preferred_username?.substring(0, 2)}</Avatar>
                    </IconButton>
                </Tooltip>
            </Box>
            <Menu
                anchorEl={anchorEl}
                id="account-menu"
                open={open}
                onClose={handleClose}
                onClick={handleClose}
                PaperProps={{
                    elevation: 0,
                    sx: {
                        overflow: 'visible',
                        filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                        mt: 1.5,
                        '& .MuiAvatar-root': {
                            // width: 32,
                            // height: 32,
                            ml: -0.5,
                            mr: 1,
                        },
                        '&::before': {
                            content: '""',
                            display: 'block',
                            position: 'absolute',
                            top: 0,
                            right: 14,
                            width: 10,
                            height: 10,
                            bgcolor: 'background.paper',
                            transform: 'translateY(-50%) rotate(45deg)',
                            zIndex: 0,
                        },
                    },
                }}
                transformOrigin={{horizontal: 'right', vertical: 'top'}}
                anchorOrigin={{horizontal: 'right', vertical: 'bottom'}}
            >
                <MenuItem onClick={handleSettings}>
                    <ListItemIcon>
                        <Settings fontSize="small"/>
                    </ListItemIcon>
                    Settings
                </MenuItem>
                <MenuItem onClick={handleSignOut}>
                    <ListItemIcon>
                        <Logout fontSize="small"/>
                    </ListItemIcon>
                    Sign out
                </MenuItem>
            </Menu>
        </React.Fragment>
    );
}

export default SgAccountMenu;