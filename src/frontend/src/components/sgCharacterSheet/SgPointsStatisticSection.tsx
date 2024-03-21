import * as React from 'react';
import {useAppDispatch, useAppSelector} from '../../redux/hooks';
import {changeCharacterTotalPoints, selectCharacter} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {AppBar, Paper, Stack, TextField} from "@mui/material";
import Grid from "@mui/material/Grid";
import {SgCharacterSheetAriaLabels} from "./SgCharacterSheetAriaLabels";
import {useTheme} from "@mui/material/styles";
import {SgTypographyPrimaryBody2, SgTypographySecondaryBody2} from "../SgTypography";
import Toolbar from "@mui/material/Toolbar";
import {pageContentMaxWidth} from "../sgTheme";
import {getCharacterPointsStatistic} from "../../domain/interfaces/character/characterDomainLogicHelper";
import {IsUndefinedOrNull} from "../../domain/commonUtils";

const SgPointsStatisticSection = () =>
{
    const dispatch = useAppDispatch();
    const character = useAppSelector(selectCharacter);
    const theme = useTheme();
    
    if(IsUndefinedOrNull(character))
    {
        return (<Grid container/>);
    }

    const boldTextStyles = {
        fontWeight: 'bold',
    };

    let pointsStatistic = getCharacterPointsStatistic(character!);
    
    return (
        <AppBar position="fixed" style={{ top: '48px', border:'none', boxShadow:'none'}} sx={{ zIndex: (theme) => theme.zIndex.appBar, display: "flex" }} color="transparent" >
            <Toolbar style={{ justifyContent: 'center' }}>
                <Paper variant="outlined" square color={theme.palette.background.paper} style={{borderRadius: '10px', borderTop:'none', maxWidth:pageContentMaxWidth}}>
                    <Stack direction="row" justifyContent="center" alignContent="center" alignItems="center" spacing={theme.spacing(1)} flexWrap={"wrap"}>
                        
                        <SgTypographyPrimaryBody2 paddingLeft={theme.spacing(1)}>
                            Total points:
                        </SgTypographyPrimaryBody2>
                        <TextField
                            type="number"
                            InputLabelProps={{ shrink: true }}
                            variant="standard"
                            value={pointsStatistic.totalPoints}
                            onChange={(e) =>dispatch(changeCharacterTotalPoints(Number(e.target.value)))}
                            inputProps={{
                                min: 0,
                                step: 1,
                                "aria-label": SgCharacterSheetAriaLabels.CharacterTotalPoints,
                                style: boldTextStyles,
                                
                            }}
                            style={{
                                width: `${(pointsStatistic.totalPoints?.toString()?.length * theme.typography.fontSize) + 10}px`,
                            }}
                        />
                        
                        <SgTypographyPrimaryBody2  aria-label={SgCharacterSheetAriaLabels.CharacterTotalRemainingPoints}>
                            Remaining Points:{pointsStatistic.remainingPoints}
                        </SgTypographyPrimaryBody2>
        
                        <SgTypographySecondaryBody2   aria-label={SgCharacterSheetAriaLabels.CharacterTotalAttributesPoints}>
                            Attributes:{pointsStatistic.attributes}
                        </SgTypographySecondaryBody2>
        
                        <SgTypographySecondaryBody2   aria-label={SgCharacterSheetAriaLabels.CharacterTotalSkillsPoints}>
                            Skills:{pointsStatistic.skills}
                        </SgTypographySecondaryBody2>
                        
                        <SgTypographySecondaryBody2   aria-label={SgCharacterSheetAriaLabels.CharacterTotalTechniquesPoints}>
                            Techniques:{pointsStatistic.techniques}
                        </SgTypographySecondaryBody2>
        
                        <SgTypographySecondaryBody2   aria-label={SgCharacterSheetAriaLabels.CharacterTotalAdvantagesPoints}>
                            Advantages:{pointsStatistic.advantages}
                        </SgTypographySecondaryBody2>
        
                        <SgTypographySecondaryBody2 paddingRight={theme.spacing(1)}  aria-label={SgCharacterSheetAriaLabels.CharacterTotalDisadvantagesPoints}>
                            Disadvantages:{pointsStatistic.disadvantages}
                        </SgTypographySecondaryBody2>
                        
                    </Stack>
                </Paper>
            </Toolbar>
        </AppBar>
    )
}




export default SgPointsStatisticSection;