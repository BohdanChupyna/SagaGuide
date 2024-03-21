import * as React from 'react';
import Box from '@mui/material/Box';

import Typography from '@mui/material/Typography';

import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {
    equipCharacterEquipmentById,
    removeCharacterEquipmentById,
    selectCharacter
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import Grid from "@mui/material/Grid";
import {
    Button, Checkbox,
    colors, Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    Divider, FormControlLabel,
    IconButton,
    Stack,
    Tooltip,
    useTheme
} from "@mui/material";
import {damageToString, isIMeleeAttack, isIRangedAttack} from "../../domain/interfaces/equipment/IEquipment";
import {
    SgTypographyPrimaryBody1,
    SgTypographyPrimaryBody2, SgTypographyPrimarySubtitle2,
    SgTypographySubtitle2WithLeftPadding,
    SgTypographyWithLeftPadding,
    SgTypographyWithRightPadding
} from "../SgTypography";
import RemoveCircleOutlineRoundedIcon from "@mui/icons-material/RemoveCircleOutlineRounded";
import {ICharacterEquipment} from "../../domain/interfaces/character/ICharacter";
import {
    getBestDefaultForCharacter,
    getSkillDefaultValueForCharacter
} from "../../domain/interfaces/character/characterDomainLogicHelper";
import {getSkillDefaultAsString} from "../../domain/interfaces/skill/ISkill";
import {
    IAttributeBonusFeature,
    IDamageReductionBonusFeature,
    isIAttributeBonusFeature,
    isIDamageReductionBonusFeature
} from "../../domain/interfaces/feature/features";
import {numberToSignString} from "../../domain/stringUtils";
import {SgCharacterSheetAriaLabels} from "./SgCharacterSheetAriaLabels";
import {isNullOrUndefined} from "util";
import {IsUndefinedOrNull} from "../../domain/commonUtils";
import {SgRuleBookTabAriaLabels} from "../sgRuleBookTab/SgRuleBookTabAriaLabels";
import {ITraitModifier, TraitModifierPointsCostToString} from "../../domain/interfaces/trait/ITrait";

const SgEquipmentSection = ()  =>
{

    const [showRemoveEquipmentDialog, setShowRemoveEquipmentDialog] = React.useState<boolean>(false);
    const [selectedEquipment, setSelectedEquipment] = React.useState<ICharacterEquipment|null>(null);
    const theme = useTheme();
    const dispatch = useAppDispatch();
    const character = useAppSelector(selectCharacter);

    const handleRemoveEquipmentDialogClose = () => {
        setShowRemoveEquipmentDialog(false);
    };

    const handleIsEquippedChanged = (equipment: ICharacterEquipment, isEquipped: boolean) => {
        dispatch(equipCharacterEquipmentById([equipment.id, isEquipped]));
    };
    
    if(IsUndefinedOrNull(character))
    {
        return (<Box/>);
    }
    
    return(
        <Grid container border={"1px solid"}>
            <Grid item xs={12}>
                <SgTypographyPrimaryBody1 textAlign={"center"}>Equipment </SgTypographyPrimaryBody1>
            </Grid>
            {character!.equipments.map((equipment, index, list) => (
                <Grid item container xs={12} alignItems={"center"} aria-label={SgCharacterSheetAriaLabels.getEquipmentRowLabel(equipment)}>
                    <Grid item xs={12}>
                        <Divider sx={{backgroundColor: "#000"}}/>
                    </Grid>
                    <Grid item xs={12}>
                        <Stack direction={"row"} flexWrap={"wrap"} spacing={theme.spacing(1)} alignItems={"center"}>
                            <SgTypographyWithLeftPadding>{equipment.equipment.name}</SgTypographyWithLeftPadding>
                            
                            <Box flex={"1"}></Box>

                            <Tooltip title={`Press to ${equipment.isEquipped ? "unequip" : "equip"} the ${equipment.equipment.name}.`}>
                                <Checkbox
                                    value={equipment.id}
                                    checked={equipment.isEquipped}
                                    size={"small"}
                                    onChange={(event: React.SyntheticEvent, checked: boolean) => handleIsEquippedChanged(equipment, checked)}
                                    inputProps={{
                                        "aria-label": SgCharacterSheetAriaLabels.getEquipmentIsEquippedCheckBoxLabel(equipment),
                                    }}
                                />
                            </Tooltip>
                            
                            <SgTypographyPrimarySubtitle2>{equipment.equipment.cost}$</SgTypographyPrimarySubtitle2>
                            <SgTypographyPrimarySubtitle2>{equipment.equipment.weight?.replace(" ", "")}</SgTypographyPrimarySubtitle2>
                            
                            <Tooltip title={`Press to remove the ${equipment.equipment.name} from the character.`}>
                                <IconButton
                                    size="medium"
                                    aria-label="remove-equipment-button"
                                    aria-haspopup="true"
                                    onClick={() => {
                                        setSelectedEquipment(equipment);
                                        setShowRemoveEquipmentDialog(true);
                                    }}
                                    color="inherit"
                                >
                                    <RemoveCircleOutlineRoundedIcon />
                                </IconButton>
                            </Tooltip>
                        </Stack>
                    </Grid>

                    
                    <Grid item xs={12}>
                        <Stack direction={"row"} flexWrap={"wrap"} spacing={theme.spacing(1)} paddingLeft={theme.spacing(1)}>
                            {equipment.equipment.features.filter(f => isIAttributeBonusFeature(f)).map(feature => (
                                <SgTypographyPrimaryBody2>{(feature as IAttributeBonusFeature).attributeType}{numberToSignString(feature.amount)},</SgTypographyPrimaryBody2>
                            ))}
                        </Stack>
                    </Grid>

                    {equipment.equipment.features.filter(f => isIAttributeBonusFeature(f)).length > 0 && (
                        <Grid item xs={12}>
                            <Divider sx={{backgroundColor: colors.grey}}/>
                        </Grid>
                    )}
                    
                    <Grid item xs={12}>
                        <Stack direction={"row"} flexWrap={"wrap"} spacing={theme.spacing(1)} paddingLeft={theme.spacing(1)}>
                            {equipment.equipment.features.filter(f => isIDamageReductionBonusFeature(f)).map(feature => (
                                <SgTypographyPrimaryBody2>{(feature as IDamageReductionBonusFeature).location} DR {feature.amount},</SgTypographyPrimaryBody2>
                                ))}
                        </Stack>
                    </Grid>
                    
                    {equipment.equipment.attacks.map(attack => (
                        <Grid item xs={12}>
                            <Stack direction={"column"}>
                                <Divider sx={{backgroundColor: colors.grey}}/>
                                
                                {isIMeleeAttack(attack) && (
                                    <Stack direction={"row"} flexWrap={"wrap"} spacing={theme.spacing(1)} paddingLeft={theme.spacing(1)} paddingRight={theme.spacing(1)}>
                                        <SgTypographyPrimaryBody2>dmg {damageToString(attack.damage)},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>st {attack.minimumStrength},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>reach {attack.reach},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>parry {attack.parry},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>block {attack.block}</SgTypographyPrimaryBody2>
                        
                                        <Box flex={"1"}></Box>
                                        <SgTypographyPrimaryBody2>{getSkillDefaultAsString(getBestDefaultForCharacter(character!, attack.defaults))}</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody1 fontWeight="bold">
                                            {getSkillDefaultValueForCharacter(character!, getBestDefaultForCharacter(character!, attack.defaults))}
                                        </SgTypographyPrimaryBody1>
                                    </Stack>
                                )}

                                {isIRangedAttack(attack) && (
                                    <Stack direction={"row"} flexWrap={"wrap"} spacing={theme.spacing(1)} paddingLeft={theme.spacing(1)} paddingRight={theme.spacing(1)}>
                                        <SgTypographyPrimaryBody2>dmg {damageToString(attack.damage)},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>st {attack.minimumStrength},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>acc {attack.accuracy},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>range {attack.range},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>rof {attack.rateOfFire},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>shots {attack.shots},</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody2>bulk {attack.bulk}</SgTypographyPrimaryBody2>

                                        <Box flex={"1"}></Box>
                                        <SgTypographyPrimaryBody2>{getSkillDefaultAsString(getBestDefaultForCharacter(character!, attack.defaults))}</SgTypographyPrimaryBody2>
                                        <SgTypographyPrimaryBody1 fontWeight="bold">
                                            {getSkillDefaultValueForCharacter(character!, getBestDefaultForCharacter(character!, attack.defaults))}
                                        </SgTypographyPrimaryBody1>
                                    </Stack>
                                )}

                            </Stack>
                        </Grid>
                    ))}
                    
                </Grid>
        
            ))}

            <Dialog
                open={showRemoveEquipmentDialog}
                aria-labelledby="remove-skill-dialog"
                aria-describedby="remove-skill-dialog"
                onClose={handleRemoveEquipmentDialogClose}
            >
                <DialogContent>
                    <DialogContentText id="remove-skill-dialog-description">
                        Remove "{selectedEquipment?.equipment.name}" ?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    {/*in debug build autofocus for buttons doesn't work check: https://stackoverflow.com/questions/75644447/autofocus-not-working-on-open-form-dialog-with-button-component-in-material-ui-v */}
                    <Button onClick={handleRemoveEquipmentDialogClose} autoFocus={true}>Cancel</Button>
                    <Button
                        onClick={() => {
                        setShowRemoveEquipmentDialog(false);
                        dispatch(removeCharacterEquipmentById(selectedEquipment!.id));
                    }}
                    >OK</Button>
                </DialogActions>
            </Dialog>
        </Grid>
    );
}

export default SgEquipmentSection;