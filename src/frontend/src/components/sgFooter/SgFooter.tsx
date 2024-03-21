import Typography from "@mui/material/Typography";
import {Box, Divider, Link, Stack} from "@mui/material";
import {pageContentMaxWidth} from "../sgTheme";
import * as React from "react";
import {useTheme} from "@mui/material/styles";

const SgFooter = () =>
{
    const theme = useTheme();
    
    return (
        <Stack maxWidth={pageContentMaxWidth}>
            <Box height="50px"></Box>
            <Divider sx={{marginTop: theme.spacing(1), marginBottom: theme.spacing(1)}}/>
            <Typography textAlign="center">
                Copyright ©2023 by Bohdan Chupyna. All rights reserved worldwide.
            </Typography>
            <Typography textAlign="center">
                GURPS is a trademark of Steve Jackson Games, and its rules and art are copyrighted by Steve Jackson Games. All rights are reserved by Steve Jackson Games.
                This game aid is the original creation of Bohdan Chupyna and is released for free distribution, and not for resale, under the permissions granted in the
                <span> </span>
                <Link href="http://www.sjgames.com/general/online_policy.html" target="_blank" rel="noopener noreferrer">
                    Steve Jackson Games Online Policy
                </Link>
            </Typography>
        </Stack>
    );
}

export default SgFooter;