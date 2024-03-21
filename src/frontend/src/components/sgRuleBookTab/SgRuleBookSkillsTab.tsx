import * as React from 'react';
import {
    Box, Button, Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    Divider,
    LinearProgress,
    Paper, Stack,
    TextField,
    Theme,
    Tooltip
} from "@mui/material";
import {useTheme} from "@mui/material/styles";
import {useGetSkillsQuery} from "../../redux/slices/api/apiSlice";
import Typography from "@mui/material/Typography";
import {getSkillDifficultyAbbreviation} from "../../domain/interfaces/character/characterDomainLogicHelper";
import {useAppDispatch} from "../../redux/hooks";
import {getSkillDefaultsAsString, getSkillNameWithSpecialization, ISkill} from "../../domain/interfaces/skill/ISkill";
import {useState} from "react";
import {SgRuleBookTabAriaLabels} from "./SgRuleBookTabAriaLabels";
import {getBookReferencesAsString} from "../../domain/interfaces/IBookReference";
import {getAttributeAbbreviation} from "../../domain/interfaces/attribute/IAttribute";
import {
    addCharacterSkill, grabCharacterSliceToastWrapper,
} from '../../redux/slices/currentCharacter/currentCharacterSlice';
import {hasSpecializationSymbol} from "../../domain/stringUtils";
import {AutoSizer, CellMeasurer, CellMeasurerCache, List} from "react-virtualized";
import {ListRowProps} from "react-virtualized/dist/es/List";
import {IsUndefinedOrNull} from "../../domain/commonUtils";
import {StyledLeftListItemTooltip, StyledListItemPaper} from "../sgTheme";


const cache = new CellMeasurerCache({
    fixedWidth: true,
    defaultHeight: 100
});

const SgRuleBookSkillsTab = () =>
{
    const [searchInput, setSearchInput] = useState("");
    const [showAddOptionalSpecializationDialog, setShowAddOptionalSpecializationDialog] = React.useState<boolean>(false);
    const [userOptionalSpecializationDialogInput, setUserOptionalSpecializationDialogInput] = React.useState<string>("");
    const [selectedSkill, setSelectedSkill] = React.useState<ISkill|null>(null);
    
    const theme = useTheme();
    const dispatch = useAppDispatch();
    const addCharacterSkillCallback = (skill: ISkill) => {
        if(!hasSpecializationSymbol(skill.specialization))
        {
            dispatch(grabCharacterSliceToastWrapper(addCharacterSkill([skill, ""])));
            return;
        }
        
        setSelectedSkill(skill);
        setShowAddOptionalSpecializationDialog(true);
    }

    const {
        data,
        isLoading,
        isSuccess,
        isError,
        error
    } = useGetSkillsQuery();

    if (isLoading) {
        return <LinearProgress />;
    }
    if (isError && error) {
        return (<Box>Error fetching data: {error.toString()}</Box>);
    }
    
    let filteredSkills = filterSkills(data!, searchInput);

    const renderRow = (props: ListRowProps) => {
        const { index, key, style, parent } = props;
        let skill = filteredSkills[index];
        return (
            <CellMeasurer
                key={key}
                cache={cache}
                parent={parent}
                columnIndex={0}
                rowIndex={index}
                style={style}>
                <Box style={style}>
                    <StyledLeftListItemTooltip title={`Double-click to add to the character. Skill defaults from: ${getSkillDefaultsAsString(skill.defaults)}`}>
                        <StyledListItemPaper onDoubleClick={() => addCharacterSkillCallback(skill)}
                               sx={{
                                   width: "100%",
                                   border: 'none',
                                   boxShadow: 'none',
                               }}
                               aria-label={SgRuleBookTabAriaLabels.getSkillPaperLabel(skill)}>
                            <Stack aria-label={`${skill.name} section`}>
                                <Typography aria-label={`${skill.name}-name`}>
                                    {getSkillNameWithSpecialization(skill)}
                                </Typography>
                                
                                <Stack direction="row">
                                    <Typography variant="body2" aria-label={`${skill.name} attributeType and difficultyLevel`}>
                                        {getAttributeAbbreviation(skill.attributeType)}/{getSkillDifficultyAbbreviation(skill.difficultyLevel)}
                                    </Typography>
    
                                    <Box sx={{ flexGrow: 1 }} />
    
                                    <Typography variant="body2" textAlign="end" aria-label={`${skill.name} sourceBook`}>
                                        {getBookReferencesAsString(skill.bookReferences)}
                                    </Typography>
                                </Stack>
                                
                                {(index + 1 < filteredSkills.length ) && (
                                    <Divider sx={{backgroundColor: "#000"}}/>
                                )}
    
                            </Stack>
                        </StyledListItemPaper>
                    </StyledLeftListItemTooltip>
                </Box>
            </CellMeasurer>
        );
    };
    
    return (
        <Stack>
            <TextField
                type="search"
                fullWidth={true}
                variant="outlined"
                placeholder="Search…"
                onChange={(e) => setSearchInput(e.target.value)}
                inputProps={{
                    "aria-label": `search`,
                }}
            />
       
            {Devider(theme)}
        
            <Typography>
                Skills:
            </Typography>
     
            <Divider sx={{backgroundColor: "#000"}}/>
     
            <Box style={{ flex: '1 1 auto', height: 'calc(100vh - 230px)'}} id={"AutoSizerBox"}>
                <AutoSizer id={"AutoSizer"}>
                    {
                        ({ width, height }) => (<List
                                width={width}
                                height={height}
                                deferredMeasurementCache={cache}
                                rowHeight={cache.rowHeight}
                                rowRenderer={renderRow}
                                rowCount={filteredSkills.length}
                                overscanRowCount={3} />
                        )
                    }
                </AutoSizer>
            </Box>
            
            <Dialog
                open={showAddOptionalSpecializationDialog}
                aria-labelledby="optional-skill-specialization-dialog"
                aria-describedby="optional-skill-specialization-dialog"
                onClose={() => setShowAddOptionalSpecializationDialog(false)}
            >
                <DialogContent>
                    <DialogContentText id="optional-skill-specialization-dialog-description">
                        Skill "{selectedSkill?.name}" has optional speciality. Please specify it below.
                    </DialogContentText>
                    <TextField
                        autoFocus
                        margin="dense"
                        id="name"
                        label="Speciality"
                        type="text"
                        fullWidth
                        variant="standard"
                        onChange={(e) => setUserOptionalSpecializationDialogInput(e.target.value)}
                    />
                </DialogContent>
                <DialogActions>
                    {/*in debug build autofocus for buttons doesn't work check: https://stackoverflow.com/questions/75644447/autofocus-not-working-on-open-form-dialog-with-button-component-in-material-ui-v */}
                    <Button onClick={() => setShowAddOptionalSpecializationDialog(false)} autoFocus={true}>Cancel</Button>
                    <Button onClick={() => {
                        setShowAddOptionalSpecializationDialog(false);
                        dispatch(grabCharacterSliceToastWrapper(addCharacterSkill([selectedSkill!, userOptionalSpecializationDialogInput])));
                    }}
                    >OK</Button>
                </DialogActions>
            </Dialog>
        </Stack>
    );
}

function filterSkills(skills: ISkill[], searchInput: string): ISkill[]
{
    return skills.filter((skill) => {
        if (searchInput === "") {
            return skill;
        }
        else {
            return skill.name.toLowerCase().includes(searchInput.toLowerCase())
        }
    })
}

function Devider(theme: Theme)
{
    return (
        <Divider sx={{marginTop: theme.spacing(1), marginBottom: theme.spacing(1)}}/>
    );
}

export default SgRuleBookSkillsTab;