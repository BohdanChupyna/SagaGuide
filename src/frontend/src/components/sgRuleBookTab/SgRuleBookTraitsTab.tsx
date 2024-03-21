import {useState} from "react";
import * as React from "react";
import {useTheme} from "@mui/material/styles";
import {useAppDispatch} from "../../redux/hooks";
import {useGetTraitsQuery} from "../../redux/slices/api/apiSlice";
import {
    Box, Button, Checkbox,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    Divider, FormControl, FormControlLabel, FormLabel,
    LinearProgress,
    Paper, Radio, RadioGroup, Stack,
    TextField, Theme, Tooltip
} from "@mui/material";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import {SgRuleBookTabAriaLabels} from "./SgRuleBookTabAriaLabels";
import ITrait, {
    ITraitModifier,
    ITraitModifierGroup,
    sortTraitModifiersByPointsCost,
    TraitModifierPointsCostToString, TraitTags
} from "../../domain/interfaces/trait/ITrait";
import {
    addCharacterTrait, grabCharacterSliceToastWrapper
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {hasSpecializationSymbol} from "../../domain/stringUtils";
import {getBookReferencesAsString} from "../../domain/interfaces/IBookReference";
import {TagsArrayToString} from "../ariaLabelUtils";
import {AutoSizer, CellMeasurer, CellMeasurerCache, List} from "react-virtualized";
import {ListRowProps} from "react-virtualized/dist/es/List";
import {StyledLeftListItemTooltip, StyledListItemPaper} from "../sgTheme";

const cache = new CellMeasurerCache({
    fixedWidth: true,
    defaultHeight: 100
});

const SgRuleBookTraitsTab = () =>
{
    const [searchInput, setSearchInput] = useState<string>("");
    const [showTraitModifiersDialog, setShowTraitModifiersDialog] = React.useState<boolean>(false);
    const [userOptionalSpecializationDialogInput, setUserOptionalSpecializationDialogInput] = React.useState<string>("");
    const [selectedTags, setSelectedTags] = React.useState<string[]>([]);
    const [selectedTrait, setSelectedTrait] = React.useState<ITrait|null>(null);
    const [selectedTraitModifiers, setSelectedTraitModifiers] = React.useState<ITraitModifier[]>([]);
    const [selectedGroupTraitModifiers, setSelectedGroupTraitModifiers] = React.useState<Map<string, ITraitModifier>>(new Map());
    
    const theme = useTheme();
    const dispatch = useAppDispatch();
    const addCharacterTraitCallback = (trait: ITrait) => {
        if(!hasSpecializationSymbol(trait.name) && trait.modifierGroups.length === 0 && trait.modifiers.length === 0)
        {
            dispatch(grabCharacterSliceToastWrapper(addCharacterTrait([trait, "", []])));
            return;
        }

        setSelectedTrait(trait);
        setShowTraitModifiersDialog(true);
    }

    const handleTraitTagFilterChange = (tag: string) => {
        if(selectedTags.includes(tag))
            setSelectedTags(selectedTags.filter(x => x !== tag));
        else
            setSelectedTags([...selectedTags, tag]);
    };

    const handleIsTraitTagFilterSelected = (tag: string) => {
        return selectedTags.includes(tag);
    };
    
    const handleTraitModifierChanged = (modifier: ITraitModifier, checked: boolean) =>
    {
        if(checked)
            setSelectedTraitModifiers([...selectedTraitModifiers, modifier]);
        else
            setSelectedTraitModifiers(selectedTraitModifiers.filter(x => x.id !== modifier.id));
    };

    const handleGroupTraitModifierChanged = (group: ITraitModifierGroup ,modifierId: string) =>
    {
        if(selectedGroupTraitModifiers.has(group.id))
        {
            let newMap = new Map(selectedGroupTraitModifiers);
            newMap.delete(group.id);
            setSelectedGroupTraitModifiers(newMap);
        }
        else
        {
            let modifier = group.modifiers.find(x => x.id === modifierId)!;
            setSelectedGroupTraitModifiers(new Map(selectedGroupTraitModifiers).set(group.id, modifier));
        }
            
    };
    
    const {
        data,
        isLoading,
        isSuccess,
        isError,
        error
    } = useGetTraitsQuery();

    if (isLoading) {
        return <LinearProgress />;
    }
    if (isError && error) {
        return (<Box>Error fetching data: {error.toString()}</Box>);
    }

    const filterOptions: IFilterOptions = 
        {
            searchInput: searchInput,
            selectedTags: selectedTags,
        }
    
    let filteredTraits = filterTraits(data!, filterOptions);

    const renderRow = (props: ListRowProps) => {
        const { index, key, style, parent } = props;
        let trait = filteredTraits[index];
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
                        <StyledListItemPaper onDoubleClick={() => addCharacterTraitCallback(trait)}
                               sx={{
                                   width: "100%",
                                   border: 'none',
                                   boxShadow: 'none',
                               }}
                               aria-label={SgRuleBookTabAriaLabels.getTraitPaperLabel(trait)}>
    
                            <Grid container alignItems={"center"} aria-label={`${trait.name} section`}>
    
                                <Grid item xs={6}>
                                    <Typography aria-label={`${trait.name}-name`}>
                                        {trait.name}
                                    </Typography>
                                </Grid>
                                <Grid item xs={6}>
                                    <Typography variant="body2" aria-label={`${trait.name} tags`}>
                                        {TagsArrayToString(trait.tags)}
                                    </Typography>
                                </Grid>
                                <Grid item xs={6}>
                                    <Typography variant="body2" aria-label={`${trait.name} base cost`}>
                                        [{trait.basePointsCost}]
                                    </Typography>
                                </Grid>
                                <Grid item xs={6}>
                                    <Typography variant="body2" textAlign="start" aria-label={`${trait.name} sourceBook`}>
                                        {getBookReferencesAsString(trait.bookReferences)}
                                    </Typography>
                                </Grid>
    
    
                                {(index + 1 < filteredTraits.length ) && (
                                    <Grid item xs={12}>
                                        <Divider sx={{backgroundColor: "#000"}}/>
                                    </Grid>
                                )}
    
                            </Grid>
                        </StyledListItemPaper>
                    </StyledLeftListItemTooltip>
                </Box>
            </CellMeasurer>
        );
    };
    
    return (
        <Stack>
            <Stack direction="row" spacing={1}>
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
            </Stack>
            
            <Stack direction="row" spacing={1} justifyContent="space-evenly" alignItems="center" marginTop={theme.spacing(1)} flexWrap={"wrap"}>
                {filterOption(theme, TraitTags.Advantage, handleIsTraitTagFilterSelected, handleTraitTagFilterChange)}
                {filterOption(theme, TraitTags.Disadvantage, handleIsTraitTagFilterSelected, handleTraitTagFilterChange)}
                {filterOption(theme, TraitTags.Mental, handleIsTraitTagFilterSelected, handleTraitTagFilterChange)}
                {filterOption(theme, TraitTags.Physical, handleIsTraitTagFilterSelected, handleTraitTagFilterChange)}
                {filterOption(theme, TraitTags.Social, handleIsTraitTagFilterSelected, handleTraitTagFilterChange)}
                {filterOption(theme, TraitTags.Exotic, handleIsTraitTagFilterSelected, handleTraitTagFilterChange)}
                {filterOption(theme, TraitTags.Supernatural, handleIsTraitTagFilterSelected, handleTraitTagFilterChange)}
            </Stack>
            
            {Devider(theme)}
            
            <Typography>
                Traits:
            </Typography>
            
            <Divider sx={{backgroundColor: "#000"}}/>

            <Box style={{ flex: '1 1 auto', height: 'calc(100vh - 280px)'}} id={"AutoSizerBox"}>
                <AutoSizer id={"AutoSizer"}>
                    {
                        ({ width, height }) => (<List
                                width={width}
                                height={height}
                                deferredMeasurementCache={cache}
                                rowHeight={cache.rowHeight}
                                rowRenderer={renderRow}
                                rowCount={filteredTraits.length}
                                overscanRowCount={3} />
                        )
                    }
                </AutoSizer>
            </Box>
            
            <Dialog
                open={showTraitModifiersDialog}
                aria-label={SgRuleBookTabAriaLabels.TraitDialog}
                onClose={() => setShowTraitModifiersDialog(false)}
            >
                <DialogContent>
                    <Stack>
                        <Typography textAlign={"center"}>
                            {selectedTrait?.name}
                        </Typography>
                        <Divider sx={{ backgroundColor: theme.palette.primary.main }} />
                        {hasSpecializationSymbol(selectedTrait?.name) && (<DialogContentText id="optional-skill-specialization-dialog-description">
                            <Typography variant={"body2"}>
                                Skill "{selectedTrait?.name}" has optional speciality. Please specify it below.
                            </Typography>
                        </DialogContentText>)}
                        {hasSpecializationSymbol(selectedTrait?.name) && (<TextField
                            autoFocus
                            margin="dense"
                            id="name"
                            label="Specialization"
                            type="text"
                            inputProps={{
                                'aria-label': SgRuleBookTabAriaLabels.TraitDialogSpecializationInput,
                            }}
                            fullWidth
                            variant="standard"
                            onChange={(e) => setUserOptionalSpecializationDialogInput(e.target.value)}
                        />)}

                        {selectedTrait !== null && selectedTrait.modifierGroups.length > 0 && selectedTrait.modifierGroups.map((traitGroup) => (
                            <FormControl>
                                <FormLabel id="demo-controlled-radio-buttons-group">{traitGroup.name}</FormLabel>
                                <RadioGroup
                                    aria-labelledby="demo-controlled-radio-buttons-group"
                                    name="controlled-radio-buttons-group"
                                    value={selectedGroupTraitModifiers.get(traitGroup.id)}
                                    onChange={(event: React.ChangeEvent<HTMLInputElement>, value: string) => handleGroupTraitModifierChanged(traitGroup, value)}
                                >
                                    {sortTraitModifiersByPointsCost(traitGroup.modifiers).map((modifier) => (
                                        <FormControlLabel 
                                            value={modifier.id}
                                            control={<Radio />}
                                            aria-label={SgRuleBookTabAriaLabels.getTraitModifierLabel(modifier)}
                                            label={
                                                <Typography variant="body2">
                                                    {`${modifier.name} (${TraitModifierPointsCostToString(modifier)})`}
                                                </Typography>}
                                        />
                                        ))}
                                </RadioGroup>
                            </FormControl>
                        ))}
                        
                        {selectedTrait !== null && selectedTrait!.modifiers.length > 0 && sortTraitModifiersByPointsCost(selectedTrait.modifiers).map((modifier) => (
                            <FormControlLabel
                                value={modifier.id}
                                control={<Checkbox />}
                                aria-label={SgRuleBookTabAriaLabels.getTraitModifierLabel(modifier)}
                                label={
                                    <Typography variant="body2">
                                        {`${modifier.name} (${TraitModifierPointsCostToString(modifier)})`}
                                    </Typography>}
                                labelPlacement="end"
                                onChange={(event: React.SyntheticEvent, checked: boolean) => handleTraitModifierChanged(modifier, checked)}
                            />
                        ))}
                    </Stack>
                </DialogContent>
                <DialogActions>
                    {/*in debug build autofocus for buttons doesn't work check: https://stackoverflow.com/questions/75644447/autofocus-not-working-on-open-form-dialog-with-button-component-in-material-ui-v */}
                    <Button
                        aria-label={SgRuleBookTabAriaLabels.TraitDialogCancelButton}    
                        onClick={() => setShowTraitModifiersDialog(false)} autoFocus={true}>Cancel</Button>
                    <Button
                        aria-label={SgRuleBookTabAriaLabels.TraitDialogOkButton}
                        onClick={() => {
                        setShowTraitModifiersDialog(false);
                        const groupModifiers = Array.from(selectedGroupTraitModifiers.values());
                        let modifiers = [...selectedTraitModifiers, ...groupModifiers];
                        dispatch(grabCharacterSliceToastWrapper(addCharacterTrait([selectedTrait!, userOptionalSpecializationDialogInput, modifiers])));
                        
                        setSelectedTrait(null);
                        setSelectedGroupTraitModifiers(new Map());
                        setSelectedTraitModifiers([]);
                    }}
                    >OK</Button>
                </DialogActions>
            </Dialog>
        </Stack>
    );
}

interface IFilterOptions
{
    searchInput: string,
    selectedTags: Array<string>,
}

function filterTraits(traits: ITrait[], filterOptions: IFilterOptions): ITrait[]
{
    let result = traits.filter((trait) => {
        if (filterOptions.searchInput === "") {
            return trait;
        }
        else {
            return trait.name.toLowerCase().includes(filterOptions.searchInput.toLowerCase())
        }
    })

    if(filterOptions.selectedTags.length > 0)
        result = result.filter((trait) => filterOptions.selectedTags.every(element => trait.tags.includes(element)));
    
    return result;
}

function Devider(theme: Theme)
{
    return (
        <Divider sx={{marginTop: theme.spacing(1), marginBottom: theme.spacing(1)}}/>
    );
}

// enum FilterOptionState
// {
//     Advantage = "Advantage",
//     Disadvantage = "Disadvantage",
//     Feature = "Feature",
// }

function filterOption(theme: Theme, tag: string, isSelectedCallBack: Function, onClickCallBack: Function)
{
    return(
        <Paper sx={{
            border: 'none',
            boxShadow: 'none',
        }}
        onClick={() => onClickCallBack(tag)}>
            <Typography variant="body2" fontWeight={isSelectedCallBack(tag) ? "bold" : "normal"}>
                {tag}
            </Typography>
        </Paper>
    );
}

export default SgRuleBookTraitsTab;