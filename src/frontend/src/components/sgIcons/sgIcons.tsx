import React from 'react';
import SvgIcon from '@mui/material/SvgIcon';
import { ReactComponent as DiscordSvg } from './discord-icon.svg';
import { ReactComponent as RedditSvg } from './reddit-logo.svg';

export const SgDiscordIcon = () => {
    
    return(
        <SvgIcon component={DiscordSvg} inheritViewBox/>
    )
}

export const SgRedditIcon = () => {

    return(
        <SvgIcon component={RedditSvg} inheritViewBox/>
    )
}
