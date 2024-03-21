import {TextField, Theme} from "@mui/material";
import InputAdornment from "@mui/material/InputAdornment";
import * as React from "react";
import Grid from "@mui/material/Grid";
import { useTheme } from '@mui/material/styles';
import {useAppDispatch, useAppSelector} from "../../redux/hooks";

import {SgCharacterSheetAriaLabels} from "./SgCharacterSheetAriaLabels";
import {
    changeCharacterAge,
    changeCharacterCampaign,
    changeCharacterGender,
    changeCharacterHandedness,
    changeCharacterHeight,
    changeCharacterName,
    changeCharacterPlayer, changeCharacterRace, changeCharacterReligion, changeCharacterSize,
    changeCharacterTechLevel, changeCharacterTitle,
    changeCharacterWeight,
    selectCharacter
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {IsUndefinedOrNull} from "../../domain/commonUtils";


export default function SgProfileSection()
{
    const theme = useTheme();
    const numberGrid = [6, 4, 3];
    const dispatch = useAppDispatch();

    const character = useAppSelector(selectCharacter);
    
    if(IsUndefinedOrNull(character))
    {
        return (<Grid container/>);
    }
    
    return (
        <Grid container sx={{marginTop: theme.spacing(1)}} spacing={theme.spacing(1)}>
            {ProfileInput(theme, "Name", SgCharacterSheetAriaLabels.CharacterName, character!.name, (value:string) => dispatch(changeCharacterName(value))) }
            {ProfileInput(theme, "Player", "", character!.player, (value:string) => dispatch(changeCharacterPlayer(value)))}
            {ProfileInput(theme, "Campaign", "", character!.campaign, (value:string) => dispatch(changeCharacterCampaign(value)))}
            {ProfileInput(theme, "Title", "", character!.title, (value:string) => dispatch(changeCharacterTitle(value)))}
            {ProfileInput(theme, "Handedness", "", character!.handedness, (value:string) => dispatch(changeCharacterHandedness(value)))}
            {ProfileInput(theme, "Gender", "", character!.gender, (value:string) => dispatch(changeCharacterGender(value)))}
            {ProfileInput(theme, "Race", "", character!.race, (value:string) => dispatch(changeCharacterRace(value)))}
            {ProfileInput(theme, "Religion", "", character!.religion, (value:string) => dispatch(changeCharacterReligion(value)))}
            
            {ProfileInput(theme, "Height", "", character!.height.toString(), (value:number) => dispatch(changeCharacterHeight(value)), "number", "cm", numberGrid)}
            {ProfileInput(theme, "Weight", "", character!.weight.toString(), (value:number) => dispatch(changeCharacterWeight(value)),  "number", "kg", numberGrid)}
            {ProfileInput(theme, "Size", "", character!.size.toString(), (value:number) => dispatch(changeCharacterSize(value)), "number", "", numberGrid,)}
            {ProfileInput(theme, "Tech Level", "", character!.techLevel.toString(), (value:number) => dispatch(changeCharacterTechLevel(value)), "number", "", numberGrid)}
            {ProfileInput(theme, "Age", "", character!.age.toString(), (value:number) => dispatch(changeCharacterAge(value)), "number", "", numberGrid,)}
        </Grid>
    );
}

function ProfileInput(theme: Theme, label: string, ariaLabel:string, value: string, callback: Function, type: string = "text", adornment: string = "", gridSize:number[] = [6, 4, 3])
{
    return (
        <Grid item xs={gridSize[0]} sm={gridSize[1]} md={gridSize[2]} sx={{marginBottom: theme.spacing(1)}}>
            <TextField
                label={label}
                type={type}
                id={label.toLowerCase().replace(" ","-") + "-input"}
                inputProps={{
                    endAdornment: <InputAdornment position="end">{adornment}</InputAdornment>,
                    "aria-label": ariaLabel,
                }}
                InputLabelProps={{ shrink: true }}
                variant="standard"
                fullWidth={true}
                value={value}
                onChange={(e) => callback(e.target.value)}
            />
        </Grid>
    );
}
