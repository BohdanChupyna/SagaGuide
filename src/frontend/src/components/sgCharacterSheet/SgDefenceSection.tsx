import * as React from 'react';
import Box from '@mui/material/Box';

import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {
    selectCharacter
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import Grid from "@mui/material/Grid";
import {
    Stack, Theme,
    useTheme
} from "@mui/material";
import {
    SgTypographyPrimaryBody1, SgTypographySecondaryBody1, SgTypographySecondaryBody2,
    SgTypographySecondarySubtitle2,
} from "../SgTypography";
import {ICharacterEquipment} from "../../domain/interfaces/character/ICharacter";

import {IsUndefinedOrNull} from "../../domain/commonUtils";
import {IDamageReductionBonusFeature, isIDamageReductionBonusFeature} from "../../domain/interfaces/feature/features";
import {
    getCharacterBlock,
    getCharacterDodge,
    getCharacterParry,
    IActiveDefenceData
} from "../../domain/interfaces/character/characterActiveDefenceHelper";
import {adaptNameForAriaLabel} from "../ariaLabelUtils";
import { SgCharacterSheetAriaLabels } from './SgCharacterSheetAriaLabels';

interface IBodyPartData
{
    partName: string,
    rollValue: string,
    defenceValue: number,
    attackPenalty: string
}

enum CharacterBodyPartNameEnum
{
    skull = "skull",
    face = "face",
    eyes = "eyes",
    neck = "neck",
    torso = "torso",
    vitals = "vitals",
    arm = "arm",
    hand = "hand",
    leg = "leg",
    groin = "groin",
    foot = "foot",
}


const SgDefenceSection = ()  =>
{
    let characterBody: { [key: string]: IBodyPartData } = {
        [CharacterBodyPartNameEnum.skull]: {
            partName: "Skull",
            rollValue: "3-4",
            defenceValue: 2,
            attackPenalty: "-7"
        },
        [CharacterBodyPartNameEnum.face]: {
            partName: "Face",
            rollValue: "5",
            defenceValue: 0,
            attackPenalty: "-5"
        },
        [CharacterBodyPartNameEnum.eyes]: {
            partName: "Eyes",
            rollValue: "",
            defenceValue: 0,
            attackPenalty: "-9"
        },
        [CharacterBodyPartNameEnum.neck]: {
            partName: "Neck",
            rollValue: "17-18",
            defenceValue: 0,
            attackPenalty: "-5"
        },
        [CharacterBodyPartNameEnum.torso]: {
            partName: "Torso",
            rollValue: "9-10",
            defenceValue: 0,
            attackPenalty: "0"
        },
        [CharacterBodyPartNameEnum.vitals]: {
            partName: "Vitals",
            rollValue: "17-18",
            defenceValue: 0,
            attackPenalty: "-3"
        },
        [CharacterBodyPartNameEnum.arm]: {
            partName: "Arms",
            rollValue: "R8, L12",
            defenceValue: 0,
            attackPenalty: "-5"
        },
        [CharacterBodyPartNameEnum.hand]: {
            partName: "Hand",
            rollValue: "15",
            defenceValue: 0,
            attackPenalty: "-4"
        },
        [CharacterBodyPartNameEnum.groin]: {
            partName: "Groin",
            rollValue: "11",
            defenceValue: 0,
            attackPenalty: "-3"
        },
        [CharacterBodyPartNameEnum.leg]: {
            partName: "Legs",
            rollValue: "R6-7, L13-14",
            defenceValue: 0,
            attackPenalty: "-2"
        },
        [CharacterBodyPartNameEnum.foot]: {
            partName: "Foot",
            rollValue: "16",
            defenceValue: 0,
            attackPenalty: "-4"
        },
    };
    
    const theme = useTheme();
    const dispatch = useAppDispatch();
    const character = useAppSelector(selectCharacter);
    
    if(IsUndefinedOrNull(character))
    {
        return (<Box/>);
    }
    
    let drFeatures: IDamageReductionBonusFeature[]  = character?.equipments.filter(eq => eq.isEquipped)
        .map((eq) => eq.equipment.features.filter(f => isIDamageReductionBonusFeature(f)))
        .flat() as unknown as IDamageReductionBonusFeature[];
    
    drFeatures.forEach(feature =>
    {
       characterBody[feature.location].defenceValue += feature.amount; 
    });
    
    return(
        <Grid container>
            <Grid item xs={12}>
                <Stack direction={"row"} alignItems={"center"} justifyContent={"center"} flexWrap={"wrap"}>
                    {activeDefenceBlock(theme, getCharacterParry(character!))}
                    {activeDefenceBlock(theme, getCharacterDodge(character!))}
                    {activeDefenceBlock(theme, getCharacterBlock(character!))}
                </Stack>
            </Grid>
            <Grid item xs={12}>
                <Stack direction={"row"} alignItems={"center"} justifyContent={"center"} flexWrap={"wrap"}>
                    {Object.values(characterBody).map(partData => (
                        bodyPartBlock(theme, partData)
                    ))}
                </Stack>
            </Grid>
        </Grid>
    );
}

function bodyPartBlock(theme: Theme, partData: IBodyPartData)
{
    return(
        <Stack direction={"column"} border={"1px solid"}>
            <SgTypographyPrimaryBody1 textAlign={"center"}>{partData.partName}</SgTypographyPrimaryBody1>
            <Stack direction={"row"} spacing={theme.spacing(1)} paddingLeft={theme.spacing(1)} paddingRight={theme.spacing(1)}>
                <SgTypographySecondarySubtitle2>{partData.rollValue}</SgTypographySecondarySubtitle2>
                <Box flex={"1"}></Box>
                <SgTypographyPrimaryBody1 fontWeight="bold">{partData.defenceValue}</SgTypographyPrimaryBody1>
                <Box flex={"1"}></Box>
                <SgTypographySecondarySubtitle2>{partData.attackPenalty}</SgTypographySecondarySubtitle2>
            </Stack>
        </Stack>
    );
}

function activeDefenceBlock(theme: Theme, defence: IActiveDefenceData)
{
    let labels = SgCharacterSheetAriaLabels.getActiveDefenceDataLabels(defence);
    return (
        <Stack direction={"column"} border={"1px solid"}>
            <Stack direction={"row"} paddingLeft={theme.spacing(1)} paddingRight={theme.spacing(1)}>
                <SgTypographyPrimaryBody1 textAlign={"center"} aria-label={labels.defenceName}>
                    {defence.defenceName} 
                </SgTypographyPrimaryBody1>
                <SgTypographySecondaryBody1 textAlign={"center"} aria-label={labels.defenceProviderName} paddingLeft={theme.spacing(1)}>
                    {`${defence.defenceProviderName}`}
                </SgTypographySecondaryBody1>
            </Stack>
            <SgTypographyPrimaryBody1 fontWeight="bold" textAlign={"center"} aria-label={labels.value}>
                {Number.isNaN(defence.value) ? "No" : defence.value.toString()}
            </SgTypographyPrimaryBody1>
        </Stack>
    );
}

export default SgDefenceSection;