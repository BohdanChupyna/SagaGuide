import {useState} from "react";
import * as React from "react";
import {getSkillDefaultAsString} from "../../domain/interfaces/skill/ISkill";
import {useTheme} from "@mui/material/styles";
import {useAppDispatch} from "../../redux/hooks";
import {useGetTechniquesQuery} from "../../redux/slices/api/apiSlice";
import {
    Box, Button, Dialog, DialogActions, DialogContent, DialogContentText,
    Divider,
    LinearProgress,
    Paper, Stack,
    TextField, Theme, Tooltip,
} from "@mui/material";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import {SgRuleBookTabAriaLabels} from "./SgRuleBookTabAriaLabels";
import {getBookReferencesAsString} from "../../domain/interfaces/IBookReference";
import {ITechnique} from "../../domain/interfaces/technique/ITechnique";
import {hasSpecializationSymbol} from "../../domain/stringUtils";
import {
    addCharacterTechnique, grabCharacterSliceToastWrapper
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {ListRowProps} from "react-virtualized/dist/es/List";
import {AutoSizer, CellMeasurer, CellMeasurerCache, List} from "react-virtualized";
import {StyledLeftListItemTooltip, StyledListItemPaper} from "../sgTheme";

const cache = new CellMeasurerCache({
    fixedWidth: true,
    defaultHeight: 100
});

const SgRuleBookTechniquesTab = () =>
{
    const [searchInput, setSearchInput] = useState<string>("");
    const [showAddOptionalSpecializationDialog, setShowAddOptionalSpecializationDialog] = React.useState<boolean>(false);
    const [userTechniqueNameSpecializationDialogInput, setUserTechniqueNameSpecializationDialogInput] = React.useState<string|null>("");
    const [userDefaultNameSpecializationDialogInput, setUserDefaultNameSpecializationDialogInput] = React.useState<string|null>("");
    const [selectedTechnique, setSelectedTechnique] = React.useState<ITechnique|null>(null);
    
    const theme = useTheme();
    const dispatch = useAppDispatch();
    const addCharacterTechniqueCallback = (technique: ITechnique) => {
        if(!hasSpecializationSymbol(technique.name) && !hasSpecializationSymbol(technique.default.name))
        {
            dispatch(grabCharacterSliceToastWrapper(addCharacterTechnique([technique, null, null])));
            return;
        }

        setSelectedTechnique(technique);
        setUserTechniqueNameSpecializationDialogInput(null);
        setUserDefaultNameSpecializationDialogInput(null);
        setShowAddOptionalSpecializationDialog(true);
    }

    const {
        data,
        isLoading,
        isSuccess,
        isError,
        error
    } = useGetTechniquesQuery();

    if (isLoading) {
        return <LinearProgress />;
    }
    if (isError && error) {
        return (<Box>Error fetching data: {error.toString()}</Box>);
    }

    let filteredTechniques = filterTechniques(data!, searchInput);

    const renderRow = (props: ListRowProps) => {
        const { index, key, style, parent } = props;
        let technique = filteredTechniques[index];
        return (
            <CellMeasurer
                key={key}
                cache={cache}
                parent={parent}
                columnIndex={0}
                rowIndex={index}
                style={style}>
                <Box style={style}>
                    <StyledLeftListItemTooltip title={'Double-click to add to the character.'}>
                        <StyledListItemPaper onDoubleClick={() => addCharacterTechniqueCallback(technique)}
                               sx={{
                                   width: "100%",
                                   border: 'none',
                                   boxShadow: 'none',
                               }}
                               aria-label={SgRuleBookTabAriaLabels.getTechniquePaperLabel(technique)}>
    
                            <Stack aria-label={`${technique.name} section`}>
                                <Typography aria-label={`${technique.name}-name`}>
                                    {technique.name}
                                </Typography>
                                
                                <Stack direction="row">
                                    <Typography variant="body2" aria-label={`${technique.name} default section`} width={"auto"}>
                                        {getSkillDefaultAsString(technique.default)}
                                    </Typography>
    
                                    <Box sx={{ flexGrow: 1 }} />
    
                                    <Typography variant="body2" textAlign="end" aria-label={`${technique.name} sourceBook`}>
                                        {getBookReferencesAsString(technique.bookReferences)}
                                    </Typography>
                                </Stack>
                                
                                {(index + 1 < filteredTechniques.length ) && (
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
                Techniques:
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
                                rowCount={filteredTechniques.length}
                                overscanRowCount={3} />
                        )
                    }
                </AutoSizer>
            </Box>
                
            <Dialog
                open={showAddOptionalSpecializationDialog}
                aria-label={SgRuleBookTabAriaLabels.TechniqueDialog}
                onClose={() => setShowAddOptionalSpecializationDialog(false)}
            >
                <DialogContent>
                    <Stack>
                        <Typography textAlign={"center"}>
                            {selectedTechnique?.name}
                        </Typography>
                        
                        <Divider sx={{ backgroundColor: theme.palette.primary.main }} />
                        
                        {hasSpecializationSymbol(selectedTechnique?.name) && (<DialogContentText id="optional-skill-specialization-dialog-description">
                            <Typography variant={"body2"}>
                                Technique "{selectedTechnique?.name}" has specialisation. Please specify it below.
                            </Typography>
                        </DialogContentText>)}
                        {hasSpecializationSymbol(selectedTechnique?.name) && (<TextField
                            autoFocus
                            margin="dense"
                            id="name"
                            label="Specialization"
                            inputProps={{
                                'aria-label': SgRuleBookTabAriaLabels.TechniqueDialogNameInput,
                            }}
                            type="text"
                            fullWidth
                            variant="standard"
                            onChange={(e) => setUserTechniqueNameSpecializationDialogInput(e.target.value)}
                        />)}

                        {hasSpecializationSymbol(selectedTechnique?.default.name) && (<DialogContentText id="optional-skill-specialization-dialog-description">
                            <Typography variant={"body2"}>
                                Technique has custom default skill "{selectedTechnique?.default.name}". Please specify it below.
                            </Typography>
                        </DialogContentText>)}
                        {hasSpecializationSymbol(selectedTechnique?.default.name) && (<TextField
                            autoFocus
                            margin="dense"
                            id="name"
                            label="Custom default"
                            inputProps={{
                                'aria-label': SgRuleBookTabAriaLabels.TechniqueDialogDefaultNameInput,
                            }}
                            type="text"
                            fullWidth
                            variant="standard"
                            onChange={(e) => setUserDefaultNameSpecializationDialogInput(e.target.value)}
                        />)}
                    </Stack>
                </DialogContent>
                <DialogActions>
                    {/*in debug build autofocus for buttons doesn't work check: https://stackoverflow.com/questions/75644447/autofocus-not-working-on-open-form-dialog-with-button-component-in-material-ui-v */}
                    <Button
                        aria-label={SgRuleBookTabAriaLabels.TechniqueDialogCancelButton}
                        onClick={() => setShowAddOptionalSpecializationDialog(false)} autoFocus={true}>Cancel</Button>
                    <Button
                        aria-label={SgRuleBookTabAriaLabels.TechniqueDialogOkButton}
                        onClick={() => {
                        setShowAddOptionalSpecializationDialog(false);
                            dispatch(grabCharacterSliceToastWrapper(
                                addCharacterTechnique([selectedTechnique!, userTechniqueNameSpecializationDialogInput, userDefaultNameSpecializationDialogInput])));
                    }}
                    >OK</Button>
                </DialogActions>
            </Dialog>
        </Stack>
    );
}

function filterTechniques(techniques: ITechnique[], searchInput: string): ITechnique[]
{
    return techniques.filter((technique) => {
        if (searchInput === "") {
            return technique;
        }
        else {
            return technique.name.toLowerCase().includes(searchInput.toLowerCase())
        }
    })
}

function Devider(theme: Theme)
{
    return (
        <Divider sx={{marginTop: theme.spacing(1), marginBottom: theme.spacing(1)}}/>
    );
}

export default SgRuleBookTechniquesTab;