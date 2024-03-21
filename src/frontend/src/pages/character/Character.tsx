import SgCharacterSheet from "../../components/sgCharacterSheet/SgCharacterSheet";
import Grid from '@mui/material/Grid';
import {styled, useTheme} from "@mui/material/styles";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {AppBarHeader, PageContent } from "../../components/sgNavBar/SgNavBar";
import React from "react";
import Typography from "@mui/material/Typography";

import SgRuleBookTab, {ruleBookTabWidth} from "../../components/sgRuleBookTab/SgRuleBookTab";
import {selectIsNavBarOpen} from "../../redux/slices/navBar/navBarSlice";
import {
    changeCurrentCharacter, selectCharacter, selectIsRuleBookOpen
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {useGetCharacterByIdQuery} from "../../redux/slices/api/apiSlice";
import {useParams} from "react-router-dom";
import {IsUndefinedOrNull} from "../../domain/commonUtils";
import SgFooter from "../../components/sgFooter/SgFooter";
import {pageContentMaxWidth} from "../../components/sgTheme";
import {useAuth} from "react-oidc-context";



const Character = () => {
    const {characterId} = useParams();
    const theme = useTheme();
    const isNavBarOpen = useAppSelector(selectIsNavBarOpen);
    const isRuleBookOpen = useAppSelector(selectIsRuleBookOpen);
    const character = useAppSelector(selectCharacter);
    const auth = useAuth();
    
    const dispatch = useAppDispatch();
    
    let shouldSkipGetCharacterApiCall = (!IsUndefinedOrNull(character) && character!.id === characterId) || !auth.isAuthenticated || auth.isLoading;
    const {
        data,
        isLoading,
        isSuccess,
        isError,
        error
    } = useGetCharacterByIdQuery(characterId!, {skip: shouldSkipGetCharacterApiCall});
    
    if (isSuccess) {
        dispatch(changeCurrentCharacter(data));
    }
    
    return (
        <CharacterPageContent container id="character-page-root" open={isNavBarOpen} isRuleBookOpen={isRuleBookOpen} justifyContent="center" >
            <Grid container justifyContent="center" style={{maxWidth:pageContentMaxWidth}}> 
                <Grid item xs={12}>
                    <AppBarHeader />
                </Grid>
                
                <Grid item xs={12} display={isLoading ? "block" : "none"}>
                    {isLoading && (<Typography> loading character...</Typography>)}
                </Grid>
    
                <Grid item xs={12} display={isError ? "block" : "none"}>
                    {isError && (<Typography>{error.toString()}</Typography>)}
                </Grid>
                
                <Grid item id={"Character"} display={character ? "block" : "none"} >
                    {SgCharacterSheet()}
                    <SgRuleBookTab/>
                </Grid>
    
                <Grid item xs={12}>
                    <SgFooter/>
                </Grid>
            </Grid>
        </CharacterPageContent>
    );
};

export const CharacterPageContent = styled(PageContent, { shouldForwardProp: (prop) => prop !== 'isRuleBookOpen' })<{
    isRuleBookOpen?: boolean;
}>(({ theme, isRuleBookOpen }) =>
{
    let pageContentMarginRight = ruleBookTabWidth + parseInt(theme.spacing(1));
    
    return ({
        [theme.breakpoints.up("sm")]: {
            ...(isRuleBookOpen && {
                transition: theme.transitions.create('margin', {
                    easing: theme.transitions.easing.easeOut,
                    duration: theme.transitions.duration.enteringScreen,

                }),
                marginRight: pageContentMarginRight,
            }),
        },
    })});

export default Character;