import * as React from 'react';
import ProfileSection from "./SgProfileSection";
import avatarSection from "./SgAvatarSection";
import StateSection from "./SgStateSection";
import {Divider, Theme, useMediaQuery} from "@mui/material";
import SgTraitsSection from "./SgTraitsSection";
import skillsSection from "./SgSkillsSection";
import Grid from "@mui/material/Grid";
import {useAppSelector} from "../../redux/hooks";
import {useTheme} from "@mui/material/styles";
import {selectIsRuleBookOpen} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import {selectIsNavBarOpen} from "../../redux/slices/navBar/navBarSlice";
import {AppBarHeader} from "../sgNavBar/SgNavBar";
import SgPointsStatisticSection from "./SgPointsStatisticSection";
import SgAttributesSection from "./SgAttributesSection";
import SgEquipmentSection from './SgEquipmentSection';
import SgDefenceSection from "./SgDefenceSection";

export default function SgCharacterSheet() 
{
    const theme = useTheme();
    const isExtraSmallScreen = useMediaQuery(theme.breakpoints.only("xs"));
    const isSmallScreen = useMediaQuery(theme.breakpoints.only("sm"));
    const isMediumScreen = useMediaQuery(theme.breakpoints.only("md"));
    
    const isRuleBookOpen = useAppSelector(selectIsRuleBookOpen);
    const isNavBarOpen = useAppSelector(selectIsNavBarOpen);
    
    const isShowExtraSmallViewDeviders = isExtraSmallScreen || (isSmallScreen && isRuleBookOpen) || (isMediumScreen && isRuleBookOpen && isNavBarOpen);
    const shouldUseExtraSmallView = isRuleBookOpen && isNavBarOpen;
    
    return (
        <Grid container spacing={theme.spacing(1)}>
            <Grid item xs={12}>
                <AppBarHeader />
            </Grid>
            <Grid item xs={12}>
                {SgPointsStatisticSection()}
            </Grid>
            <Grid item xs={12} sm={isRuleBookOpen ? 12 : 4} md={shouldUseExtraSmallView ? 12 : 3} lg={3}>
                {avatarSection()}
            </Grid>
            <Grid item xs={12} sm={isRuleBookOpen ? 12 : 8} md={shouldUseExtraSmallView ? 12 : 9} lg={9}>
                {ProfileSection()}
            </Grid>
            <Grid item xs={12}>
                {Devider(theme)}
            </Grid>
            <Grid item xs={12} sm={isRuleBookOpen ? 12 : 6} md={shouldUseExtraSmallView ? 12 : 6} lg={6}>
                {SgAttributesSection(theme)}
            </Grid>
            {isShowExtraSmallViewDeviders && (<Grid item xs={12}>
                {Devider(theme)}
            </Grid>)}
            <Grid item xs={12} sm={isRuleBookOpen ? 12 : 6} md={shouldUseExtraSmallView ? 12 : 6} lg={6}>
                {StateSection()}
            </Grid>
            <Grid item xs={12}>
                {Devider(theme)}
            </Grid>
            <Grid item xs={12}>
                {SgDefenceSection()}
            </Grid>
            <Grid item xs={12}>
                {Devider(theme)}
            </Grid>
            <Grid item xs={12} sm={isRuleBookOpen ? 12 : 6} md={shouldUseExtraSmallView ? 12 : 6} lg={6}>
                {SgTraitsSection()}
            </Grid>
            {isShowExtraSmallViewDeviders && (<Grid item xs={12}>
                {Devider(theme)}
            </Grid>)}
            <Grid item xs={12} sm={isRuleBookOpen ? 12 : 6} md={shouldUseExtraSmallView ? 12 : 6} lg={6}>
                {skillsSection()}
            </Grid>
            <Grid item xs={12}>
                {Devider(theme)}
            </Grid>
            <Grid item xs={12}>
                {SgEquipmentSection()}
            </Grid>
        </Grid>
    );
}

function Devider(theme: Theme)
{
    return (
        <Divider sx={{marginTop: theme.spacing(1), marginBottom: theme.spacing(1)}}/>
    );
}

