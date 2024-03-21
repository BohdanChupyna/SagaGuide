import Grid from '@mui/material/Grid';
import {AppBarHeader, PageContent } from "../../components/sgNavBar/SgNavBar";
import React, {useEffect, useRef} from "react";
import Typography from "@mui/material/Typography";
import { useNavigate } from 'react-router-dom';
import {
    useGetCharacterByIdQuery,
    useGetUnregisteredCharacterQuery,
    usePostCharacterMutation
} from "../../redux/slices/api/apiSlice";
import {useAuth} from "react-oidc-context";
import {useAppDispatch} from "../../redux/hooks";
import {changeCurrentCharacter} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {unregisteredCharacterLocalStorageKey} from "../../components/constants";
import {ICharacter} from "../../domain/interfaces/character/ICharacter";

const CharacterCreationPage = () => {
    const navigate = useNavigate ();
    const auth = useAuth();
    const dispatch = useAppDispatch();
    //const [isPostCallMade, setIsPostCallMade] = React.useState<boolean>(false);
    const isPostCallMade = useRef(false);
    
    const [
        postNewCharacter, // This is the mutation trigger
        postNewCharacterResult, // This is the destructured mutation result
    ] = usePostCharacterMutation();

    // Add ability to create unregistered character without sign in.
    const {
        data,
        isLoading,
        isSuccess: getUnregisteredCharacterIsSuccess,
        isError,
        error
    } = useGetUnregisteredCharacterQuery(null, {skip: auth.isAuthenticated && !auth.isLoading});
    
    if (!auth.isAuthenticated && !auth.isLoading && getUnregisteredCharacterIsSuccess) {
        dispatch(changeCurrentCharacter(data));
        let path = `/character/${data.id}`;
        navigate(path, {relative:"path", replace:true});
    }

    useEffect(() =>
    {
        if(auth.isAuthenticated && !auth.isLoading && !isPostCallMade.current)
        {
            let savedUnregisteredCharacter = window.localStorage.getItem(unregisteredCharacterLocalStorageKey);
            if(savedUnregisteredCharacter)
            {
                window.localStorage.removeItem(unregisteredCharacterLocalStorageKey);
                postNewCharacter(JSON.parse(savedUnregisteredCharacter) as ICharacter);
                return
            }
            else
            {
                postNewCharacter();    
            }
            isPostCallMade.current = true;
        }
    }, [auth.isAuthenticated, auth.isLoading, isPostCallMade]);
    
   if(postNewCharacterResult.isSuccess)
    {
        let path = `/character/${postNewCharacterResult.data.id}`;
        navigate(path, {relative:"path", replace:true});
    }

    return (
        <PageContent container id="character-creation-page-root"  justifyContent="center">
            <Grid item xs={12}>
                <AppBarHeader />
            </Grid>

            <Grid item xs={12} display={postNewCharacterResult.isLoading ? "block" : "none"}>
                {(postNewCharacterResult.isLoading || isLoading) && (<Typography> loading character...</Typography>)}
            </Grid>

            <Grid item xs={12} display={postNewCharacterResult.isError ? "block" : "none"}>
                {(postNewCharacterResult.isError || isError) && (<Typography>{postNewCharacterResult.error?.toString() || error?.toString()}</Typography>)}
            </Grid>

            <Grid item xs={12} display={postNewCharacterResult.isSuccess ? "block" : "none"}>
                {(postNewCharacterResult.isSuccess || getUnregisteredCharacterIsSuccess) && (<Typography>Success</Typography>)}
            </Grid>
            
        </PageContent>
    );
};
export default CharacterCreationPage;