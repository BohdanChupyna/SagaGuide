import * as React from "react";
import {TextField, Theme} from "@mui/material";
import Typography from "@mui/material/Typography";
import Grid from '@mui/material/Grid';
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {changeCharacterAttribute, selectCharacter} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {AttributeTypeEnum, getAttributeAbbreviation} from "../../domain/interfaces/attribute/IAttribute";
import {
    getCharacterAttribute,
    getCharacterAttributeValue
} from "../../domain/interfaces/character/characterDomainLogicHelper";
import {ICharacter} from "../../domain/interfaces/character/ICharacter";

export default function SgAttributesSection(theme: Theme)
{
    const dispatch = useAppDispatch();
    const character = useAppSelector(selectCharacter);
    const updateAtrributeCallback = (attributeType: AttributeTypeEnum, value: number) => dispatch(changeCharacterAttribute([attributeType, value]));
    
    if(character === null)
    {
        return (<Grid container/>);
    }
    
    return (
        <Grid container>
            <Grid item xs={6} sx={{display: "flex", justifyContent: "space-between", flexDirection: "column"}}>
                {AttributeInput(theme, character, AttributeTypeEnum.Strength, updateAtrributeCallback)}
                {AttributeInput(theme, character, AttributeTypeEnum.Dexterity, updateAtrributeCallback)}
                {AttributeInput(theme, character, AttributeTypeEnum.Intelligence, updateAtrributeCallback)}
                {AttributeInput(theme, character, AttributeTypeEnum.Health, updateAtrributeCallback)}
            </Grid>
            <Grid item xs={6}>
                {AttributeInput(theme, character, AttributeTypeEnum.HitPoints, updateAtrributeCallback)}
                {AttributeInput(theme, character, AttributeTypeEnum.BasicMove, updateAtrributeCallback)}
                {AttributeInput(theme, character, AttributeTypeEnum.BasicSpeed, updateAtrributeCallback)}
                {AttributeInput(theme, character, AttributeTypeEnum.Will, updateAtrributeCallback)}
                {AttributeInput(theme, character, AttributeTypeEnum.Perception, updateAtrributeCallback)}
                {AttributeInput(theme, character, AttributeTypeEnum.FatiguePoints, updateAtrributeCallback)}
            </Grid>
        </Grid>
    );
}

function AttributeInput(theme: Theme, character: ICharacter, attributeType: AttributeTypeEnum, callback: Function)
{
    let label = getAttributeAbbreviation(attributeType);
    let attribute = getCharacterAttribute(character, attributeType);
    
    return (
        <Grid container alignItems="center" aria-label={`${attributeType} attribute section`}>
            <Grid item xs={3}>
                <Typography variant="body2" align="right">
                    {label}
                </Typography>
            </Grid>
            <Grid item xs={6}>
                <TextField
                    type="number"
                    id={label.toLowerCase().replace(" ","-") + "-input"}
                    sx={{
                        paddingLeft: theme.spacing(1),
                        paddingRight: theme.spacing(1),
                    }}
                    variant="outlined"
                    value={getCharacterAttributeValue(character, attributeType)}
                    onChange={(e) => callback(attributeType, Number(e.target.value))}
                    inputProps={{
                        min: 0,
                        step: attribute.attribute.valueIncreasePerLevel,
                        "aria-label": `${attributeType} input`,
                    }}
                   
                    // onInput = {(e) =>
                    // { var InputElement = (e.target as HTMLInputElement); InputElement.value = Math.max(0, parseInt(InputElement.value) ).toString().slice(0,3); }}
                />
            </Grid>
            <Grid item xs={3}>
                <Typography variant="body2" aria-label={`${attributeType} spent points`}>
                    {`[${attribute.spentPoints.toString()}]`}
                </Typography>
            </Grid>
        </Grid>
    );
}
