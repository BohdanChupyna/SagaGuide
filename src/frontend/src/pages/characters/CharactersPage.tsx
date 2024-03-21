import Grid from '@mui/material/Grid';
import {useTheme} from "@mui/material/styles";
import {Stack, useMediaQuery} from "@mui/material";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {AppBarHeader, PageContent } from "../../components/sgNavBar/SgNavBar";
import React from "react";
import Typography from "@mui/material/Typography";

import {selectIsNavBarOpen} from "../../redux/slices/navBar/navBarSlice";
import {useGetCharactersInfoQuery} from "../../redux/slices/api/apiSlice";
import {SgCharacterInfoPoster} from "../../components/sgCharacterInfoPoster/SgCharacterInfoPoster";
import SgFooter from "../../components/sgFooter/SgFooter";
import {pageContentMaxWidth} from "../../components/sgTheme";



const CharactersPage = () => {
    const theme = useTheme();
    const isSmallScreen = useMediaQuery(theme.breakpoints.down("sm"));
    const isNavBarOpen = useAppSelector(selectIsNavBarOpen);
    const dispatch = useAppDispatch();
    
    const {
        data,
        isLoading,
        isSuccess,
        isError,
        error
    } = useGetCharactersInfoQuery([]);
    
    let pageContentDisplay: string = "none";

    if (isLoading) {
        pageContentDisplay = "none";
    } else if (isSuccess) {
        pageContentDisplay = "block";
    } else if (isError) {
    }

    
    
    return (
        <PageContent container id="character-page-root" open={isNavBarOpen} justifyContent="center">
            <Grid container justifyContent="center" spacing={theme.spacing(1)} maxWidth={pageContentMaxWidth}> 
                <Grid item xs={12}>
                    <AppBarHeader />
                </Grid>
    
                <Grid item xs={12} display={isLoading ? "block" : "none"}>
                    {isLoading && (<Typography> loading character...</Typography>)}
                </Grid>
    
                <Grid item xs={12} padding={theme.spacing(1)} display={pageContentDisplay}>
                    <Stack direction="row" flexWrap="wrap" justifyContent="center">
                        {isSuccess && data.map(characterInfo => (
                            SgCharacterInfoPoster(characterInfo, theme)
                        ))}
                    </Stack>
                </Grid>
    
                <Grid item xs={12} >
                    <SgFooter/>
                </Grid>
            </Grid>
        </PageContent>
    );
};
export default CharactersPage;