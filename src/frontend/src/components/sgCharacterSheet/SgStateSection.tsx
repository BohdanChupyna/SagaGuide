import * as React from "react";
import {TextField, Theme, useMediaQuery} from "@mui/material";
import Typography from "@mui/material/Typography";
import Grid from '@mui/material/Grid';
import Box from "@mui/material/Box";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {useTheme} from "@mui/material/styles";
import {
    getCharacterAttributeValue
} from "../../domain/interfaces/character/characterDomainLogicHelper";
import {AttributeTypeEnum} from "../../domain/interfaces/attribute/IAttribute";
import {
    changeCharacterFpLose,
    changeCharacterHpLose,
    selectCharacter
} from "../../redux/slices/currentCharacter/currentCharacterSlice";

export default function SgStateSection()
{
    const theme = useTheme();
    const isSmallScreen = useMediaQuery(theme.breakpoints.down('md'));
    const dispatch = useAppDispatch();
    const character = useAppSelector(selectCharacter);
    
    if(character === null)
    {
        return (<Box/>);
    }
    
    return (
        <Grid container sx={{justifyContent: "space-between"}} height={"100%"} alignItems={"center"} spacing={theme.spacing(1)}>
            <Grid item xs={5} md={4}>
                {stateInput(theme, "HP lose", character.hpLose.toString(), (value:number) => dispatch(changeCharacterHpLose(value)))}
            </Grid>
            <Grid item xs={7} md={8}>
                {
                    hpStates(
                        getCharacterAttributeValue(character, AttributeTypeEnum.HitPoints), 
                        character.hpLose,
                        isSmallScreen
                    )
                }
            </Grid>
            <Grid item xs={5} md={4}>
                {stateInput(theme, "FP lose", character.fpLose.toString(), (value:number) => dispatch(changeCharacterFpLose(value)))}
            </Grid>
            <Grid item xs={7} md={8}>
                {
                    fpStates(
                        getCharacterAttributeValue(character, AttributeTypeEnum.FatiguePoints),
                        character.fpLose,
                        isSmallScreen
                    )
                }
            </Grid>
        </Grid>
    );
}

function stateInput(theme: Theme, label: string, value: string, callback: Function)
{
    return (
        <Grid container alignItems="center">
            <Grid item xs={5} md={5}>
                <Typography variant="body2" align="right">
                    {label}
                </Typography>
            </Grid>
            <Grid item xs={7} md={7}>
                <TextField
                    label=""
                    type="number"
                    id={label.toLowerCase().replace(" ","-") + "-input"}
                    sx={{
                        paddingLeft: theme.spacing(1),
                        paddingRight: theme.spacing(1),
                    }}
                    value={value}
                    onChange={(e) => callback(e.target.value)}
                    inputProps={{
                        min: 0,
                    }}
                    variant="outlined"
                />
            </Grid>
        </Grid>
    );
}

function hpStates(hp: number, hpLose: number, isSmallScreen: boolean)
{
    const hpTwoThird = Math.ceil((hp/3)*2);
    const hpx2 = hp*2;
    const hpx3 = hp*3;
    const hpx4 = hp*4;
    const hpx5 = hp*5;
    const hpx6 = hp*6;
    const hpx11 = hp*11;
    return (
        <Typography variant="body2" fontSize={isSmallScreen ? 12 : 14}>
            {conditionalText(hpTwoThird.toString(), () => hpLose >= hpTwoThird)}: 1/3 your HP, you are at half Move and Dodge.
            <br/>{conditionalText(hp.toString(), () => hpLose >= hp)}: In addition to the above effects, make a HT roll at the start of your next turn, at -1 per full multiple of HP below zero.
            <br/>
            {conditionalText(hpx2.toString(), () => hpLose >= hpx2)},
            {conditionalText(hpx3.toString(), () => hpLose >= hpx3)},
            {conditionalText(hpx4.toString(), () => hpLose >= hpx4)},
            {conditionalText(hpx5.toString(), () => hpLose >= hpx5)}: immediate HT roll or die. (If you fail by only 1 or 2, you’re dying, but not dead – see Mortal Wounds, p. 423).
            <br/>{conditionalText(hpx6.toString(), () => hpLose >= hpx6)}: You die immediately.
            <br/>{conditionalText(hpx11.toString(), () => hpLose >= hpx11)}: Total bodily destruction.
        </Typography>
    );
}

function fpStates(fp: number, fpLose: number, isSmallScreen: boolean)
{
    let fpTwoThird = Math.ceil((fp/3)*2);
    let fpx2 =fp*2;
    return (
        <Typography variant="body2" fontSize={isSmallScreen ? 12 : 14}>
            {conditionalText(fpTwoThird.toString(), () => fpLose >= fpTwoThird)}: 1/3 your FP, Halve your Move, Dodge, and ST (round up). This does not affect ST-based quantities, such as HP and damage.
            <br/>{conditionalText(fp.toString(), () => fpLose >= fp)}: If you suffer further fatigue, each FP you lose also causes 1 HP of injury. Make a Will roll to do anything besides talk or rest; in combat, roll before each maneuver other than Do Nothing.
            <br/>{conditionalText(fpx2.toString(), () => fpLose >= fpx2)}: You fall unconscious.
        </Typography>
    );
}

function conditionalText(text: string, predicat: () => boolean)
{
    if(predicat())
    {
     return (<strong> <span style={{ color: 'red' }}>{text} </span> </strong>);
    }
    
    return (<strong> {text} </strong>);
}