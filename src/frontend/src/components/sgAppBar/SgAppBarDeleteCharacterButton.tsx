import * as React from 'react';
import {
    Box,
    Button, Dialog,
    DialogActions,
    DialogContent,
    IconButton,
    Tooltip
} from "@mui/material";
import DeleteForeverRoundedIcon from '@mui/icons-material/DeleteForeverRounded';
import {useAppDispatch, useAppSelector} from '../../redux/hooks';
import {SgAppBarAriaLabels} from "./SgAppBarAriaLabels";
import {
    changeCurrentCharacter,
    selectCharacter
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {IsUndefinedOrNull} from "../../domain/commonUtils";
import {useDeleteCharacterMutation} from "../../redux/slices/api/apiSlice";
import {IsOnCharacterPage} from "../../routerUtils";
import Typography from "@mui/material/Typography";
import {useNavigate} from "react-router-dom";


const SgAppBarDeleteCharacterButton = () =>
{
    const character = useAppSelector(selectCharacter);
    let isCharacterPage = IsOnCharacterPage();
    const dispatch = useAppDispatch();
    const navigate = useNavigate ();
    const [showDialog, setShowDialog] = React.useState<boolean>(false);
    
    const [deleteCharacter, {isLoading, isError, error}] = useDeleteCharacterMutation();

    const handleDeleteCharacterButtonClick = async () => {
        setShowDialog(true);
    };

    const handleCharacterDeletion = async () => {
        setShowDialog(false);
        await deleteCharacter(character!.id!);
        dispatch(changeCurrentCharacter(null));
        navigate(`characters/`);
    };
    
    return (
        <Box>
            <Tooltip title={"Delete character"}>
                <IconButton
                    color="inherit"
                    aria-label={SgAppBarAriaLabels.DeleteCharacterButton}
                    onClick={handleDeleteCharacterButtonClick}
                    sx={{ ...((!isCharacterPage || IsUndefinedOrNull(character)) && { display: 'none' }) }}
                >
                    <DeleteForeverRoundedIcon />
                </IconButton>
            </Tooltip>
            <Dialog
                open={showDialog}
                aria-label={SgAppBarAriaLabels.DeleteCharacterDialog}
                onClose={() => setShowDialog(false)}
            >
                <DialogContent>
                    <Typography textAlign={"center"}>
                        Delete {character?.name}?
                    </Typography>
                    <Typography textAlign={"center"}>
                        This action cannot be undone.
                    </Typography>
                </DialogContent>
                <DialogActions>
                    {/*in debug build autofocus for buttons doesn't work check: https://stackoverflow.com/questions/75644447/autofocus-not-working-on-open-form-dialog-with-button-component-in-material-ui-v */}
                    <Button
                        aria-label={SgAppBarAriaLabels.DeleteCharacterDialogCancelButton}
                        onClick={() => setShowDialog(false)} autoFocus={true}>
                            Cancel
                    </Button>
                    <Button
                        aria-label={SgAppBarAriaLabels.DeleteCharacterDialogOkButton}
                        onClick={handleCharacterDeletion}>
                            OK
                    </Button>
                </DialogActions>
            </Dialog>
        </Box>
    )
}



export default SgAppBarDeleteCharacterButton;