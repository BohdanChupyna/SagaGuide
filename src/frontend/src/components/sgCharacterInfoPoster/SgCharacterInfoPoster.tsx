import {Avatar, Stack, Theme, Tooltip} from "@mui/material";
import {ICharacterInfo} from "../../domain/interfaces/character/ICharacter";

import Typography from "@mui/material/Typography";
import * as React from "react";
import {useTheme} from "@mui/material/styles";
import { Link } from "react-router-dom";

export function SgCharacterInfoPoster(characterInfo: ICharacterInfo, theme: Theme)
{
    const linkStyle = {
        textDecoration: "none",
        color: theme.palette.text.primary
    };
    
    return (
        <Stack width="100%" height='100%' maxWidth='320px' maxHeight='450px' padding={theme.spacing(1)}>
           <Tooltip title={`Click to open ${characterInfo.name} character`}>
                <Link to={`/character/${characterInfo.id}`} style={linkStyle}>
                <Avatar variant="square" sx={{
                    width: '100%',
                    height: '100%',
                    maxWidth: '320px',
                    maxHeight: '320px',
                    aspectRatio: '1',
                }}>
                    {characterInfo.name}
                </Avatar>
                <Typography textAlign="center">
                    Campaign: {characterInfo.campaign}
                </Typography>
                {/*<Typography textAlign="center">*/}
                {/*    Player: {characterInfo.player}*/}
                {/*</Typography>*/}
                <Typography textAlign="center">
                    Character: {characterInfo.name} {characterInfo.title ? `"${characterInfo.title}"` : ''}
                </Typography>
                </Link>
           </Tooltip>
        </Stack>
    );
}
