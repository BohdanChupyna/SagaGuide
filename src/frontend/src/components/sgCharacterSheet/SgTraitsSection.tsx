import * as React from 'react';
import {
    Button,
    Dialog, DialogActions,
    DialogContent,
    DialogContentText,
    Divider,
    Grid,
    IconButton,
    Tooltip,
} from "@mui/material";
import SortByAlphaRoundedIcon from '@mui/icons-material/SortByAlphaRounded';
import ExpandRoundedIcon from '@mui/icons-material/ExpandRounded';
import CreateNewFolderRoundedIcon from '@mui/icons-material/CreateNewFolderRounded';
import {SgTypographyPrimaryBody1, SgTypographyWithLeftPadding, SgTypographyWithRightPadding} from '../SgTypography';
import {useTheme} from "@mui/material/styles";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import Box from "@mui/material/Box";
import RemoveCircleOutlineRoundedIcon from "@mui/icons-material/RemoveCircleOutlineRounded";
import {ICharacterTrait} from "../../domain/interfaces/character/ICharacter";
import {removeCharacterTraitById, selectCharacter} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {
    getCharacterTraitNameWithOptionalSpeciality,
    getCharacterTraitSpentPoints
} from "../../domain/interfaces/character/characterDomainLogicHelper";
import {SgCharacterSheetAriaLabels} from "./SgCharacterSheetAriaLabels";

export default function SgTraitsSection()
{
    const [showRemoveTraitDialog, setShowRemoveTraitDialog] = React.useState<boolean>(false);
    const [selectedCharacterTrait, setSelectedCharacterTrait] = React.useState<ICharacterTrait|null>(null);

    
    const theme = useTheme();
    const dispatch = useAppDispatch();
    const character = useAppSelector(selectCharacter);
    
    if(character === null)
    {
        return (<Box/>);
    }

    const handleRemoveTraitDialogClose = () => {
        setShowRemoveTraitDialog(false);
    };
    
    return(
        <Grid container border={"1px solid"}>
            <Grid item xs={12}>
                <SgTypographyPrimaryBody1 textAlign={"center"}>Traits</SgTypographyPrimaryBody1>
            </Grid>
            <Grid item xs={12} md={0}>
                <Divider sx={{backgroundColor: "#000"}}/>
            </Grid>
            {character.traits!.length > 0 && character.traits!.map((trait, index, list) => (
                <Grid item container xs={12} alignItems={"center"} aria-label={SgCharacterSheetAriaLabels.getTraitRowLabel(trait)}>
                    <Grid item xs={6}>
                        <SgTypographyWithLeftPadding aria-label={SgCharacterSheetAriaLabels.getTraitNameLabel(trait)}>
                            {getCharacterTraitNameWithOptionalSpeciality(trait)}
                        </SgTypographyWithLeftPadding>
                    </Grid>
                    <Grid item xs={5}>
                        <SgTypographyWithRightPadding sx={{textAlign: 'right'}} aria-label={SgCharacterSheetAriaLabels.getTraitCostLabel(trait)}>
                            {`[${getCharacterTraitSpentPoints(trait)}]`}
                        </SgTypographyWithRightPadding>
                    </Grid>
                    <Grid item xs={1} justifyContent="center">
                        <Tooltip title={`Press to remove trait from character.`}>
                            <IconButton
                                size="medium"
                                aria-label="remove-skill-button"
                                aria-haspopup="true"
                                onClick={() => {
                                    setSelectedCharacterTrait(trait);
                                    setShowRemoveTraitDialog(true);
                                }}
                                color="inherit"
                            >
                                <RemoveCircleOutlineRoundedIcon />
                            </IconButton>
                        </Tooltip>
                    </Grid>
                    {(index + 1 < list.length ) && (
                        <Grid item xs={12} md={0}>
                            <Divider sx={{backgroundColor: "#000"}}/>
                        </Grid>
                    )}
                </Grid>
            ))}
            <Dialog
                open={showRemoveTraitDialog}
                aria-labelledby="remove-trait-dialog"
                aria-describedby="remove-trait-dialog"
                onClose={handleRemoveTraitDialogClose}
            >
                <DialogContent>
                    <DialogContentText id="remove-trait-dialog-description">
                        Remove "{getCharacterTraitNameWithOptionalSpeciality(selectedCharacterTrait)}" trait?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    {/*in debug build autofocus for buttons doesn't work check: https://stackoverflow.com/questions/75644447/autofocus-not-working-on-open-form-dialog-with-button-component-in-material-ui-v */}
                    <Button onClick={handleRemoveTraitDialogClose} autoFocus={true}>Cancel</Button>
                    <Button onClick={() => {
                        setShowRemoveTraitDialog(false);
                        dispatch(removeCharacterTraitById(selectedCharacterTrait!.id));
                    }}
                    >OK</Button>
                </DialogActions>
            </Dialog>
        </Grid>
    );
}

function Menu()
{
    return(
        <Grid container justifyContent="space-around" alignItems="center">
            <Grid item xs={8} container alignItems="center">
                <SgTypographyWithLeftPadding align="center">
                    Traits
                </SgTypographyWithLeftPadding>
            </Grid>
            
            <Grid item xs={1}>
                <Tooltip title="Create new group" followCursor>
                    <IconButton aria-label="CreateNewFolder">
                        <CreateNewFolderRoundedIcon fontSize="small"/>
                    </IconButton>
                </Tooltip>
            </Grid>

            <Grid item xs={1}>
                <Tooltip title="Expand/Colapse groups" followCursor>
                    <IconButton aria-label="Expand">
                        <ExpandRoundedIcon fontSize="small"/>
                    </IconButton>
                </Tooltip>
            </Grid>
            
            <Grid item xs={1}>
                <Tooltip title="Sort alphabeticaly" followCursor>
                    <IconButton aria-label="SortByAlpha">
                        <SortByAlphaRoundedIcon fontSize="small"/>
                    </IconButton>
                </Tooltip>
            </Grid>
        </Grid>
    ); 
}