import * as React from 'react';
import {IconButton, Tooltip} from "@mui/material";
import SaveRoundedIcon from '@mui/icons-material/SaveRounded';
import {useAppDispatch, useAppSelector} from '../../redux/hooks';
import {SgAppBarAriaLabels} from "./SgAppBarAriaLabels";
import {changeCurrentCharacter, selectCharacter} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {IsUndefinedOrNull} from "../../domain/commonUtils";
import {usePutCharacterMutation} from "../../redux/slices/api/apiSlice";
import {IsOnCharacterPage} from "../../routerUtils";
import {enqueueToast} from "../../redux/slices/toasts/toastsSlice";
import {
    constructErrorToast,
    constructInfoToast,
    constructSuccessToast
} from "../../redux/slices/toasts/IToastProviderSlice";
import {isFetchBaseQueryError} from "../../redux/rtqQueryUtils";
import {useEffect} from "react";
import {ICharacter} from "../../domain/interfaces/character/ICharacter";
import deepEqual from "fast-deep-equal";
import {useAuth} from "react-oidc-context";

//deps: Array<any> = []
function useInterval(callback: Function, delay = 1000) {
    const savedCallback = React.useRef<Function>(); // to save the current "fresh" callback

    // keep callback ref up to date
    React.useEffect(() => {
        savedCallback.current = callback;
    });

    // create the interval
    React.useEffect(() => {
        // function to call the callback
        function runCallback() {
            // @ts-ignore
            savedCallback.current();
        }
        if (typeof delay === 'number') {
            // run the interval
            let interval = setInterval(runCallback, delay);
            // clean up on unmount or dependency change
            return () => clearInterval(interval);
        }
    }, [delay]);
};

const SgAppBarSaveCharacterButton = () =>
{
    const character = useAppSelector(selectCharacter, deepEqual);
    const dispatch = useAppDispatch();
    const auth = useAuth();
    let isCharacterPage = IsOnCharacterPage();
    
    const [saveCharacter, {data, isLoading, isSuccess, isError, error }] = usePutCharacterMutation();
    const [oldCharacter, setOldCharacter] = React.useState<ICharacter|null>(null);
    
    const handleSaveCharacter = async (updatedCharacter: ICharacter|null) => {
        if(!auth.isAuthenticated || auth.isLoading)
        {
            dispatch(enqueueToast(constructErrorToast(`You must sing in to save character!`)));
            return;
        }
        
        if(updatedCharacter)
        {
            await saveCharacter(updatedCharacter);
            return;
        }
        
        dispatch(enqueueToast(constructInfoToast(`No character changes detected.`)));
    };
    
    const handleSaveCharacterAutoSave = async (updatedCharacter: ICharacter|null) => {
        if(!auth.isAuthenticated || auth.isLoading)
        {
            return;
        }
        if(updatedCharacter && !deepEqual(updatedCharacter, oldCharacter))
        {
            await saveCharacter(updatedCharacter);
        }
    };

    useEffect(() => {
        if(isSuccess)
        {
            setOldCharacter(data!);
            dispatch(changeCurrentCharacter(data!));
            dispatch(enqueueToast(constructSuccessToast(`${character?.name} character is saved`)));
        }
        if(isError)
        {
            let reason = "unknown";
            if(isFetchBaseQueryError(error))
            {
                reason = `${error.status} ${"error" in error ? error.error : error.data}`;    
            }
            
             dispatch(enqueueToast(constructErrorToast(`Failed to save ${character?.name} character. Reason: ${reason}`)));
        }
    }, [isSuccess, isError, error, data])
    
    useInterval(() => handleSaveCharacterAutoSave(character), 30*1000);
    
    return (
        <Tooltip title={"Save character"}>
                <IconButton
                    color="inherit"
                    aria-label={SgAppBarAriaLabels.SaveCharacterButton}
                    onClick={() => handleSaveCharacter(character)}
                    sx={{ ...((!isCharacterPage || IsUndefinedOrNull(character)) && { display: 'none' }) }}
                >
                    <SaveRoundedIcon />
                </IconButton>
        </Tooltip>
    )
}



export default SgAppBarSaveCharacterButton;
