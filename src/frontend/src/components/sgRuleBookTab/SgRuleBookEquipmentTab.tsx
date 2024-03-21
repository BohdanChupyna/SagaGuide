import {useState} from "react";
import * as React from "react";
import {useTheme} from "@mui/material/styles";
import {useAppDispatch} from "../../redux/hooks";
import {useGetEquipmentQuery} from "../../redux/slices/api/apiSlice";
import {
    Box, colors,
    Divider, FormControl, InputLabel,
    LinearProgress, MenuItem,
    Paper, Select, Slider, Stack,
    TextField, Theme, Tooltip
} from "@mui/material";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import {SgRuleBookTabAriaLabels} from "./SgRuleBookTabAriaLabels";

import {
    addCharacterEquipment, grabCharacterSliceToastWrapper,
} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {getBookReferencesAsString} from "../../domain/interfaces/IBookReference";
import {
    damageToString,
    getEquipmentLegalityClassValue,
    getEquipmentTechLevelValue,
    getEquipmentWeightValue,
    IEquipment, isIMeleeAttack, isIRangedAttack
} from "../../domain/interfaces/equipment/IEquipment";
import {IsUndefinedOrNull} from "../../domain/commonUtils";
import {TagsArrayToString} from "../ariaLabelUtils";
import { List, AutoSizer, CellMeasurer, CellMeasurerCache } from 'react-virtualized';
import {ListRowProps} from "react-virtualized/dist/es/List";
import {StyledLeftListItemTooltip, StyledListItemPaper} from "../sgTheme";

const cache = new CellMeasurerCache({
    fixedWidth: true,
    defaultHeight: 100
});

const SgRuleBookEquipmentTab = () =>
{
    const [searchInput, setSearchInput] = useState("");
   
    const [selectedTag, setSelectedTag] = React.useState<string|undefined>(undefined);
    //const [selectedEquipment, setSelectedEquipment] = React.useState<IEquipment|null>(null);
    // const [minCost, setMinCost] = React.useState<number|null>(null);
    // const [maxCost, setMaxCost] = React.useState<number|null>(null);
    const [tlSliderValues, setTlSliderValues] = React.useState<number[]>([0, 12]);
    const [lcSliderValues, setLcSliderValues] = React.useState<number[]>([0, 4]);
    const theme = useTheme();
    const dispatch = useAppDispatch();
    
    const {
        data,
        isLoading,
        isSuccess,
        isError,
        error
    } = useGetEquipmentQuery();

    if (isLoading) {
        return <LinearProgress />;
    }
    if (isError && error) {
        return (<Box>Error fetching data: {error.toString()}</Box>);
    }

    let availableTags: Array<string> = Array.from(collectTags(data!));

    const filterOptions: IFilterOptions =
    {
        searchInput: searchInput,
        selectedTag: selectedTag,
        minCost: null,
        maxCost: null,
        minTl: tlSliderValues[0],
        maxTl: tlSliderValues[1],
        minLc: lcSliderValues[0],
        maxLc:lcSliderValues[1],
        minWeight: null,
        maxWeight: null,
    };
    
    let filteredEquipment = filterEquipment(data!, filterOptions);

    let tlMarks: any[] = [];
    for(let i = 0; i < 13; ++i)
    {
        tlMarks.push(
            {
                value: i,
                label: i.toString(10),
            }
        )
    }

    let lcMarks: any[] = [];
    for(let i = 0; i < 5; ++i)
    {
        lcMarks.push(
            {
                value: i,
                label: i.toString(10),
            }
        )
    }

    const renderRow = (props: ListRowProps) => {
        const { index, key, style, parent } = props;
        let equipment = filteredEquipment[index];
        return (
            <CellMeasurer
                key={key}
                cache={cache}
                parent={parent}
                columnIndex={0}
                rowIndex={index}
                style={style}>
                    <Grid container style={style}>
                        <Grid item xs={12} >
                            <StyledLeftListItemTooltip title={'Double-click to add to the character.'}>
                                <StyledListItemPaper onDoubleClick={() => dispatch(grabCharacterSliceToastWrapper(addCharacterEquipment(equipment)))}
                                       sx={{
                                           width: "100%",
                                           border: 'none',
                                           boxShadow: 'none',
                                       }}
                                       aria-label={SgRuleBookTabAriaLabels.getEquipmentPaperLabel(equipment)}>
    
                                    <Grid container alignItems={"center"} aria-label={`${equipment.name} section`}>
    
                                        <Grid item xs={9}>
                                            <Typography aria-label={`${equipment.name}-name`}>
                                                {equipment.name}
                                            </Typography>
                                        </Grid>
                                        <Grid item xs={3}>
                                            <Typography variant="body2" aria-label={`${equipment.name} TL and LC`} textAlign={"end"}>
                                                TL:{getEquipmentTechLevelValue(equipment)}, LC:{getEquipmentLegalityClassValue(equipment)}
                                            </Typography>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Typography variant="body2" aria-label={`${equipment.name} base cost`}>
                                                {equipment.cost}$, {getEquipmentWeightValue(equipment)} lb
                                            </Typography>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Typography variant="body2" aria-label={`${equipment.name} sourceBook`} textAlign={"end"}>
                                                {getBookReferencesAsString(equipment.bookReferences)}
                                            </Typography>
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Typography variant="body2" aria-label={`${equipment.name} tags`} textAlign={"end"}>
                                                {TagsArrayToString(equipment.tags)}
                                            </Typography>
                                        </Grid>
    
                                        {(equipment.attacks.length > 0) && equipment.attacks.map((attack) => (
                                            <Grid item xs={12}>
                                                <Stack direction={"column"}>
                                                    <Divider sx={{backgroundColor: colors.grey}}/>
    
                                                    {isIMeleeAttack(attack) && (
                                                        <Stack direction={"row"} flexWrap={"wrap"} spacing={theme.spacing(1)}>
                                                            <Typography variant={"subtitle2"}>dmg {damageToString(attack.damage)},</Typography>
                                                            <Typography variant={"subtitle2"}>st {attack.minimumStrength},</Typography>
                                                            <Typography variant={"subtitle2"}>reach {attack.reach},</Typography>
                                                            <Typography variant={"subtitle2"}>parry {attack.parry},</Typography>
                                                            <Typography variant={"subtitle2"}>block {attack.block}</Typography>
                                                        </Stack>
                                                    )}
    
                                                    {isIRangedAttack(attack) && (
                                                        <Stack direction={"row"} flexWrap={"wrap"} spacing={theme.spacing(1)}>
                                                            <Typography variant={"subtitle2"}>dmg {damageToString(attack.damage)},</Typography>
                                                            <Typography variant={"subtitle2"}>st {attack.minimumStrength},</Typography>
                                                            <Typography variant={"subtitle2"}>acc {attack.accuracy},</Typography>
                                                            <Typography variant={"subtitle2"}>range {attack.range},</Typography>
                                                            <Typography variant={"subtitle2"}>rof {attack.rateOfFire},</Typography>
                                                            <Typography variant={"subtitle2"}>shots {attack.shots},</Typography>
                                                            <Typography variant={"subtitle2"}>bulk {attack.bulk}</Typography>
                                                        </Stack>
                                                    )}
    
                                                </Stack>
                                            </Grid>
                                        ))}
    
    
                                        {(index + 1 < filteredEquipment.length ) && (
                                            <Grid item xs={12}>
                                                <Divider sx={{backgroundColor: "#000"}}/>
                                            </Grid>
                                        )}
    
                                    </Grid>
                                </StyledListItemPaper>
                            </StyledLeftListItemTooltip>
                        </Grid>
                    </Grid>
            </CellMeasurer>
        );
    };
    
    return (
        <Stack id={'equipment-tab'}>
            <Stack direction="row" spacing={theme.spacing(1)}>
                <TextField
                    type="search"
                    fullWidth={true}
                    variant="outlined"
                    placeholder="Search…"
                    onChange={(e) => setSearchInput(e.target.value)}
                    inputProps={{
                        "aria-label": SgRuleBookTabAriaLabels.SearchInput,
                    }}
                />
                <FormControl fullWidth>
                    <InputLabel id="demo-simple-select-label">Equip type</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={selectedTag}
                        label="Equip type"
                        onChange={e => setSelectedTag(e.target.value)}
                    >
                        <MenuItem value={undefined}>All</MenuItem>
                        {availableTags!.map(tag => (
                            <MenuItem value={tag}>{tag}</MenuItem>
                        ))}
                    </Select>
                </FormControl>
            </Stack>

            {/*<Stack direction="row" spacing={theme.spacing(1)} maxWidth={"50%"}>*/}
            {/*        <Typography>*/}
            {/*            Cost:*/}
            {/*        </Typography>*/}
            {/*        <TextField*/}
            {/*            type="number"*/}
            {/*            fullWidth={true}*/}
            {/*            variant="outlined"*/}
            {/*            placeholder="min"*/}
            {/*            onChange={(e) => setMinCost(parseFloat(e.target.value))}*/}
            {/*            inputProps={{*/}
            {/*                "aria-label": `min`,*/}
            {/*                min: 0,*/}
            {/*                step: 1,*/}
            {/*            }}*/}
            {/*            // style={{*/}
            {/*            //     width: `80px`,*/}
            {/*            // }}*/}
            {/*        />*/}
            {/*        <TextField*/}
            {/*            type="number"*/}
            {/*            fullWidth={true}*/}
            {/*            variant="outlined"*/}
            {/*            placeholder="max"*/}
            {/*            onChange={(e) => setMaxCost(parseFloat(e.target.value))}*/}
            {/*            inputProps={{*/}
            {/*                "aria-label": `max`,*/}
            {/*                min: 0,*/}
            {/*                step: 1,*/}
            {/*            }}*/}
            {/*        />*/}
            {/*</Stack>*/}

            {/*<Stack direction="row" spacing={theme.spacing(1)}>*/}
            {/*    */}
            {/*</Stack>*/}

            <Stack direction="row" spacing={theme.spacing(1)} height={"50px"} marginTop={theme.spacing(1)}>
                <Typography>
                    TL:
                </Typography>
                <Slider
                    getAriaLabel={() => "Tech level"}
                    step={1}
                    value={tlSliderValues}
                    onChange={(event, newValue) => setTlSliderValues(newValue as number[])}
                    valueLabelDisplay="auto"
                    marks={tlMarks}
                    min={0}
                    max={12}
                    style={{
                        marginLeft: theme.spacing(2),
                        marginRight: theme.spacing(1),
                    }}
                />
            </Stack>

            <Stack direction="row" spacing={theme.spacing(1)} height={"50px"} marginTop={theme.spacing(1)}>
                <Typography>
                    LC:
                </Typography>
                <Slider
                    getAriaLabel={() => "Legality class"}
                    step={1}
                    value={lcSliderValues}
                    onChange={(event, newValue) => setLcSliderValues(newValue as number[])}
                    valueLabelDisplay="auto"
                    marks={lcMarks}
                    min={0}
                    max={4}
                    style={{
                        marginLeft: theme.spacing(2),
                        marginRight: theme.spacing(1),
                    }}
                />
            </Stack>
            
            {Devider(theme)}

            <Typography>
                Equipments:
            </Typography>

            <Divider sx={{backgroundColor: "#000"}}/>

            <Box style={{ flex: '1 1 auto', height: 'calc(100vh - 370px)'}} id={"AutoSizerBox"}>
                <AutoSizer id={"AutoSizer"}>
                    {
                        ({ width, height }) => (<List
                                width={width}
                                height={height}
                                deferredMeasurementCache={cache}
                                rowHeight={cache.rowHeight}
                                rowRenderer={renderRow}
                                rowCount={filteredEquipment.length}
                                overscanRowCount={3} />
                        )
                    }
                </AutoSizer>
            </Box>
        </Stack>
    );
}

//

interface IFilterOptions
{
    searchInput: string|null,
    selectedTag: string|undefined,
    minCost: number|null,
    maxCost: number|null,
    minTl: number|null,
    maxTl: number|null,
    minLc: number|null,
    maxLc: number|null,
    minWeight: number|null,
    maxWeight: number|null,
}

function collectTags(equipments: IEquipment[]): Set<string>
{
    let result = new Set<string>();
    if(equipments.length == 0)
        return result;
    
    equipments.every(equipment => equipment.tags.every(tag => result.add(tag)));

    const sortedArray = Array.from(result);
    sortedArray.sort((a, b) => a.localeCompare(b))
    
    return new Set(sortedArray);
}

function filterEquipment(equipments: IEquipment[], options: IFilterOptions,): IEquipment[]
{
    return equipments.filter((equipment) => {
        let doesSatisfy: boolean;
        
        if (IsUndefinedOrNull(options.searchInput) || options.searchInput === "") {
            doesSatisfy = true;
        }
        else {
            doesSatisfy = equipment.name.toLowerCase().includes(options.searchInput!.toLowerCase())
        }
        if(!doesSatisfy) return false;
        
        if(IsUndefinedOrNull(options.selectedTag) || options.selectedTag === "")
        {
            doesSatisfy = true;
        }
        else {
            doesSatisfy = equipment.tags.some(tag => tag === options.selectedTag);
        }
        if(!doesSatisfy) return false;
        
        if(!IsUndefinedOrNull(options.minCost) && !IsUndefinedOrNull(equipment.cost))
        {
            doesSatisfy = equipment.cost! >= options.minCost!;    
        }
        if(!doesSatisfy) return false;

        if(!IsUndefinedOrNull(options.maxCost) && !IsUndefinedOrNull(equipment.cost))
        {
            doesSatisfy = equipment.cost! <= options.maxCost!;
        }
        if(!doesSatisfy) return false;

        if(!IsUndefinedOrNull(options.minWeight) && !IsUndefinedOrNull(equipment.weight))
        {
            doesSatisfy = getEquipmentWeightValue(equipment) >= options.minWeight!;
        }
        if(!doesSatisfy) return false;

        if(!IsUndefinedOrNull(options.maxWeight) && !IsUndefinedOrNull(equipment.weight))
        {
            doesSatisfy = getEquipmentWeightValue(equipment) <= options.maxWeight!;
        }
        if(!doesSatisfy) return false;

        if(!IsUndefinedOrNull(options.minTl))
        {
            doesSatisfy = getEquipmentTechLevelValue(equipment) >= options.minTl!;
        }
        if(!doesSatisfy) return false;

        if(!IsUndefinedOrNull(options.maxTl))
        {
            doesSatisfy = getEquipmentTechLevelValue(equipment) <= options.maxTl!;
        }
        if(!doesSatisfy) return false;

        if(!IsUndefinedOrNull(options.minLc))
        {
            doesSatisfy = getEquipmentLegalityClassValue(equipment) >= options.minLc!;
        }
        if(!doesSatisfy) return false;

        if(!IsUndefinedOrNull(options.maxLc))
        {
            doesSatisfy = getEquipmentLegalityClassValue(equipment) <= options.maxLc!;
        }
        
        return doesSatisfy;
    })
}

function Devider(theme: Theme)
{
    return (
        <Divider sx={{marginTop: theme.spacing(1), marginBottom: theme.spacing(1)}}/>
    );
}

export default SgRuleBookEquipmentTab;