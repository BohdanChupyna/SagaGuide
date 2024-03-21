import * as React from 'react';
import Drawer from '@mui/material/Drawer';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import {Box, useMediaQuery} from "@mui/material";
import { useTheme } from '@mui/material/styles';
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {AppDispatch} from "../../redux/store";
import Typography from "@mui/material/Typography";
import {AppBarHeader} from "../sgNavBar/SgNavBar";
import SgRuleBookSkillsTab from "./SgRuleBookSkillsTab";
import {SgRuleBookTabAriaLabels} from "./SgRuleBookTabAriaLabels";
import SgRuleBookTraitsTab from "./SgRuleBookTraitsTab";
import SgRuleBookTechniquesTab from "./SgRuleBookTechniquesTab";
import {selectIsRuleBookOpen} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import SgRuleBookEquipmentTab from "./SgRuleBookEquipmentTab";



const SgRuleBookTab = () =>
{
    const isRuleBookOpen = useAppSelector(selectIsRuleBookOpen);
    const dispatch = useAppDispatch();

    const theme = useTheme();
    const isTabFixedSize = useMediaQuery(theme.breakpoints.up("sm"));

    const [currentTab, setCurrentTab] = React.useState(0);
    const handleChangeTab = (event: React.SyntheticEvent, newValue: number) => {
        setCurrentTab(newValue);
    };
    
    return RuleBookTab(isTabFixedSize, isRuleBookOpen, dispatch, currentTab, handleChangeTab);
}

interface TabPanelProps {
    children?: React.ReactNode;
    tabPanelIndex: number;
    currentIndex: number;
}

function TabPanel(props: TabPanelProps) {
    const { children, currentIndex, tabPanelIndex, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={currentIndex !== tabPanelIndex}
            id={`rulebook-tab-${tabPanelIndex}`}
            aria-labelledby={`rulebook-tabpanel-${tabPanelIndex}`}
            aria-label={""}
            {...other}
        >
            {currentIndex === tabPanelIndex && (
                <Box sx={{ m: 1 }}>
                    {children}
                </Box>
            )}
        </div>
    );
}

export const ruleBookTabWidth = 360;

const RuleBookTab = (isTabFixedSize: boolean, isRuleBookOpen: boolean, dispatch: AppDispatch, currentTabIndex: number, handleChangeTab: (event: React.SyntheticEvent, newValue: number) => void) =>
{
    return (
        <Drawer
            anchor={"right"}
            variant={ "persistent"}
            open={isRuleBookOpen}
            PaperProps={{
                sx: { width: isTabFixedSize ? ruleBookTabWidth : "100%" },
            }}
        >
            <AppBarHeader/>
            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                <Tabs 
                    value={currentTabIndex}
                    onChange={handleChangeTab}
                    variant="scrollable"
                    scrollButtons="auto"
                    aria-label="basic tabs example">
                    <Tab label="Skills" aria-label={SgRuleBookTabAriaLabels.SkillsTabPanelButton}/>
                    <Tab label="Techniques" aria-label={SgRuleBookTabAriaLabels.TechniquesTabPanelButton} />
                    <Tab label="Traits" aria-label={SgRuleBookTabAriaLabels.TraitsTabPanelButton}  />
                    <Tab label="Equip" aria-label={SgRuleBookTabAriaLabels.EquipmentsTabPanelButton}   />
                </Tabs>
            </Box>
            <TabPanel currentIndex={currentTabIndex} tabPanelIndex={0}>
                <SgRuleBookSkillsTab/>
            </TabPanel>
            <TabPanel currentIndex={currentTabIndex} tabPanelIndex={1}>
                <SgRuleBookTechniquesTab/>
            </TabPanel>
            <TabPanel currentIndex={currentTabIndex} tabPanelIndex={2}>
                <SgRuleBookTraitsTab/>
            </TabPanel>
            <TabPanel currentIndex={currentTabIndex} tabPanelIndex={3}>
                <SgRuleBookEquipmentTab/>
            </TabPanel>
        </Drawer>
    )
}

export default SgRuleBookTab;