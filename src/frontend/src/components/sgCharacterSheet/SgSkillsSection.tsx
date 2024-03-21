import * as React from 'react';
import {
    Button,
    Dialog, DialogActions, DialogContent, DialogContentText,
    Divider,
    Grid,
    IconButton,
    TextField,
    Tooltip,
} from "@mui/material";
import Typography from "@mui/material/Typography";
import {SgTypographyPrimaryBody1, SgTypographyWithLeftPadding} from "../SgTypography";
import {useTheme} from "@mui/material/styles";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {
    changeCharacterSkillPoints, changeCharacterTechniquePoints,
    removeCharacterSkillById, removeCharacterTechniqueById,
    selectCharacter
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import Box from "@mui/material/Box";
import {
    getCharacterSkillLevel, getCharacterTechniqueDefaultName,
    getCharacterTechniqueLevel, getCharacterTechniqueNameWithOptionalSpeciality,
    getSkillDifficultyAbbreviation,
    getCharacterSkillNameWithOptionalSpeciality, getCharacterTechniqueRelatedSkill
} from "../../domain/interfaces/character/characterDomainLogicHelper";
import RemoveCircleOutlineRoundedIcon from '@mui/icons-material/RemoveCircleOutlineRounded';
import {ICharacterSkill, ICharacterTechnique} from "../../domain/interfaces/character/ICharacter";
import {SgCharacterSheetAriaLabels} from "./SgCharacterSheetAriaLabels";
import {getBookReferencesAsString} from "../../domain/interfaces/IBookReference";
import {getAttributeAbbreviation} from "../../domain/interfaces/attribute/IAttribute";
import {getSkillDefaultAsString} from "../../domain/interfaces/skill/ISkill";
import {IsUndefinedOrNull} from "../../domain/commonUtils";


export default function SgSkillsSection()
{
    const [showRemoveSkillDialog, setShowRemoveSkillDialog] = React.useState<boolean>(false);
    const [selectedCharacterSkill, setSelectedCharacterSkill] = React.useState<ICharacterSkill|null>(null);
    const [selectedCharacterTechnique, setSelectedCharacterTechnique] = React.useState<ICharacterTechnique|null>(null);
    
    const theme = useTheme();
    const dispatch = useAppDispatch();
    const character = useAppSelector(selectCharacter);

    const updateSkillLevelCallback = (skillId: string, value: number) => dispatch(changeCharacterSkillPoints([skillId, value]));
    const updateTechniqueLevelCallback = (techniqueId: string, value: number) => dispatch(changeCharacterTechniquePoints([techniqueId, value]));
    if(character === null)
    {
        return (<Box/>);
    }

    const handleRemoveSkillDialogClose = () => {
        setShowRemoveSkillDialog(false);
    };
    
    return(
        <Grid container border={"1px solid"}>
            <Grid item xs={12}>
                <SgTypographyPrimaryBody1 textAlign={"center"}> Skills/Techniques</SgTypographyPrimaryBody1>
            </Grid>
            <Grid item xs={12} md={0}>
                <Divider sx={{backgroundColor: "#000"}}/>
            </Grid>
            {character.skills.length > 0 && character.skills.map((skill, index, list) => (
                <Grid item container xs={12} md={0} alignItems={"center"} aria-label={SgCharacterSheetAriaLabels.getSkillRowLabel(skill.skill)}>
                    <Grid item xs={5}>
                        <Tooltip title={`For detailed description check ${getBookReferencesAsString(skill.skill.bookReferences)}.`}>
                            <SgTypographyWithLeftPadding aria-label={`${skill.skill.name} name`}>
                                {getCharacterSkillNameWithOptionalSpeciality(skill)}
                            </SgTypographyWithLeftPadding>
                        </Tooltip>
                    </Grid>
                    <Grid item xs={2}>
                        <Tooltip title={`Skill depends on ${skill.skill.attributeType} with ${skill.skill.difficultyLevel} difficulty.`}>
                            <Typography aria-label={`${skill.skill.name} defaults`}>
                                {getAttributeAbbreviation(skill.skill.attributeType)}/{getSkillDifficultyAbbreviation(skill.skill.difficultyLevel)}
                            </Typography>
                        </Tooltip>
                    </Grid>
                    <Grid item xs={1}>
                        <Tooltip title={`Skill level.`}>
                            <Typography aria-label={`${skill.skill.name} level`}>
                                {getCharacterSkillLevel(character, skill)}
                            </Typography>
                        </Tooltip>
                    </Grid>
                    <Grid item xs={3}>
                        <Tooltip title={`Count of spent points.`}>
                            <TextField
                                type="number"
                                id={skill.skill.name.toLowerCase().replace(" ","-") + "-input"}
                                InputLabelProps={{ shrink: true }}
                                variant="outlined"
                                sx={{
                                    padding: theme.spacing(1),
                                }}
                                value={skill.spentPoints}
                                onChange={(e) => updateSkillLevelCallback(skill.id, Number(e.target.value))}
                                inputProps={{
                                    min: 1,
                                    "aria-label": `${skill.skill.name} input`,
                                }}
                            />
                        </Tooltip>
                    </Grid>
                    <Grid item xs={1} justifyContent="center">
                        <Tooltip title={`Press to remove the skill from character.`}>
                            <IconButton
                                size="medium"
                                aria-label={SgCharacterSheetAriaLabels.getSkillRemoveButtonLabel(skill.skill)}
                                aria-haspopup="true"
                                onClick={() => {
                                    setSelectedCharacterSkill(skill);
                                    setShowRemoveSkillDialog(true);
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

            {(character.techniques.length > 0) && (
                <Grid item xs={12} md={0}>
                    <Divider sx={{backgroundColor: "#000"}}/>
                </Grid>
            )}
            
            {character.techniques.length > 0 && character.techniques.map((technique, index, list) => (
                <Grid item container xs={12} md={0} alignItems={"center"} aria-label={SgCharacterSheetAriaLabels.getTechniqueRowLabel(technique)}>
                    <Grid item xs={5}>
                        <Tooltip title={getCharacterTechniqueRelatedSkill(character, technique.technique) 
                            ? `For detailed description check ${getBookReferencesAsString(technique.technique.bookReferences)}.`
                            : `Skill ${technique.technique.default.name} ${technique.technique.default.specialization ? `with ${technique.technique.default.specialization} specialization` : ''} is required !`}>
                            <SgTypographyWithLeftPadding
                                aria-label={`${getCharacterTechniqueNameWithOptionalSpeciality(technique)} name`}
                                color={getCharacterTechniqueRelatedSkill(character, technique.technique) ? theme.palette.text.primary : "red"}>
                                    {getCharacterTechniqueNameWithOptionalSpeciality(technique)}
                            </SgTypographyWithLeftPadding>
                        </Tooltip>
                    </Grid>
                    <Grid item xs={2}>
                        <Tooltip title={`Technique depends on ${getSkillDefaultAsString(technique.technique.default)} with ${technique.technique.difficultyLevel} difficulty.`}>
                            <Typography aria-label={`${getCharacterTechniqueNameWithOptionalSpeciality(technique)} defaults`}>
                                {getCharacterTechniqueDefaultName(technique)}/{getSkillDifficultyAbbreviation(technique.technique.difficultyLevel)}
                            </Typography>
                        </Tooltip>
                    </Grid>
                    <Grid item xs={1}>
                        <Tooltip title={`Technique level.`}>
                            <Typography aria-label={`${getCharacterTechniqueNameWithOptionalSpeciality(technique)} level`}>
                                {getCharacterTechniqueLevel(character, technique)}
                            </Typography>
                        </Tooltip>
                    </Grid>
                    <Grid item xs={3}>
                        <Tooltip title={`Count of spent points.`}>
                            <TextField
                                type="number"
                                id={getCharacterTechniqueNameWithOptionalSpeciality(technique).toLowerCase().replace(" ","-") + "-input"}
                                InputLabelProps={{ shrink: true }}
                                variant="outlined"
                                sx={{
                                    padding: theme.spacing(1),
                                }}
                                value={technique.spentPoints}
                                onChange={(e) => updateTechniqueLevelCallback(technique.id, Number(e.target.value))}
                                inputProps={{
                                    min: 1,
                                    "aria-label": `${getCharacterTechniqueNameWithOptionalSpeciality(technique)} input`,
                                }}
                            />
                        </Tooltip>
                    </Grid>
                    <Grid item xs={1} justifyContent="center">
                        <Tooltip title={`Press to remove the technique from character.`}>
                            <IconButton
                                size="medium"
                                aria-label="remove-skill-button"
                                aria-haspopup="true"
                                onClick={() => {
                                    setSelectedCharacterTechnique(technique);
                                    setShowRemoveSkillDialog(true);
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
                open={showRemoveSkillDialog}
                aria-labelledby="remove-skill-dialog"
                aria-describedby="remove-skill-dialog"
                onClose={handleRemoveSkillDialogClose}
            >
                <DialogContent>
                    <DialogContentText id="remove-skill-dialog-description">
                        Remove "{selectedCharacterSkill?.skill.name ?? selectedCharacterTechnique?.technique.name}" ?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    {/*in debug build autofocus for buttons doesn't work check: https://stackoverflow.com/questions/75644447/autofocus-not-working-on-open-form-dialog-with-button-component-in-material-ui-v */}
                    <Button onClick={handleRemoveSkillDialogClose} autoFocus={true}>Cancel</Button>
                    <Button
                        aria-label={SgCharacterSheetAriaLabels.CharacterSkillsRemoveDialogOkButton}    
                        onClick={() => {
                        setShowRemoveSkillDialog(false);
                        if(!IsUndefinedOrNull(selectedCharacterSkill)) 
                        {
                            dispatch(removeCharacterSkillById(selectedCharacterSkill!.id));
                            setSelectedCharacterSkill(null);
                        }
                        else 
                        {
                            dispatch(removeCharacterTechniqueById(selectedCharacterTechnique!.id));
                            setSelectedCharacterTechnique(null);
                        }
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
                    Skills/Techniques
                </SgTypographyWithLeftPadding>
            </Grid>

            {/*<Grid item xs={1}>*/}
            {/*    <Tooltip title="Create new group" followCursor>*/}
            {/*        <IconButton aria-label="CreateNewFolder">*/}
            {/*            <CreateNewFolderRoundedIcon fontSize="small"/>*/}
            {/*        </IconButton>*/}
            {/*    </Tooltip>*/}
            {/*</Grid>*/}
            
            {/*<Grid item xs={1}>*/}
            {/*    <Tooltip title="Expand/Colapse groups" followCursor>*/}
            {/*        <IconButton aria-label="Expand">*/}
            {/*            <ExpandRoundedIcon fontSize="small"/>*/}
            {/*        </IconButton>*/}
            {/*    </Tooltip>*/}
            {/*</Grid>*/}
            
            {/*<Grid item xs={1}>*/}
            {/*    <Tooltip title="Sort alphabeticaly" followCursor>*/}
            {/*        <IconButton aria-label="SortByAlpha">*/}
            {/*            <SortByAlphaRoundedIcon fontSize="small"/>*/}
            {/*        </IconButton>*/}
            {/*    </Tooltip>*/}
            {/*</Grid>*/}

        </Grid>
    );
}
