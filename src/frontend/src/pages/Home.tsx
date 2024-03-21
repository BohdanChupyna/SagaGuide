import {useGetEquipmentQuery, useGetSkillsQuery, useGetTechniquesQuery, useGetTraitsQuery} from "../redux/slices/api/apiSlice";
import {AppBarHeader, PageContent} from "../components/sgNavBar/SgNavBar";
import React from "react";
import SgFooter from "../components/sgFooter/SgFooter";
import {pageContentMaxWidth} from "../components/sgTheme";
import {useAuth} from "react-oidc-context";
import {Button, Divider, Link, Stack, Tooltip} from "@mui/material";
import {SgTypographyPrimaryBody1, SgTypographyPrimaryBody2} from "../components/SgTypography";
import CoffeeIcon from '@mui/icons-material/Coffee';
import {SgDiscordIcon, SgRedditIcon} from "../components/sgIcons/sgIcons";
import {useAppSelector} from "../redux/hooks";
import {selectIsNavBarOpen} from "../redux/slices/navBar/navBarSlice";
import {useNavigate} from "react-router-dom";



const Home = () => {
    const [skip, setSkip] = React.useState(false)
    const isNavBarOpen = useAppSelector(selectIsNavBarOpen);
    const navigate = useNavigate();
    const auth = useAuth();
    
    //Fetch cache once after sign in.
    useGetSkillsQuery(undefined, {skip: !auth.isAuthenticated || skip});
    useGetTraitsQuery(undefined, {skip: !auth.isAuthenticated || skip});
    useGetTechniquesQuery(undefined, {skip: !auth.isAuthenticated || skip});
    useGetEquipmentQuery(undefined, {skip: !auth.isAuthenticated || skip});

    if(auth.isAuthenticated && skip)
    {
        setSkip(true);
    }
    
    return (
        <PageContent container id="character-creation-page-root" open={isNavBarOpen} justifyContent="center">
            <Stack justifyContent="center" style={{maxWidth:pageContentMaxWidth}}>
                <AppBarHeader />
                <SgTypographyPrimaryBody1 variant={'h5'}>About Saga Guide</SgTypographyPrimaryBody1>
                <SgTypographyPrimaryBody1>
                    Saga Guide is designed to enhance the gaming experience for game masters and players of the GURPS Fourth Edition roleplaying game. <br />
                    Currently, Saga Guide offers an interactive character sheet, with more exciting features on the horizon.<br />
                </SgTypographyPrimaryBody1>

                {!auth.isAuthenticated && (<SgTypographyPrimaryBody1>
                    <b>You should</b><span> </span>
                    <Link onClick={() => void auth.signinRedirect()}>
                    sign in
                    </Link><span> </span>
                    <b>to use all Saga Guide possibilities.</b>
                </SgTypographyPrimaryBody1>)}

                {!auth.isAuthenticated &&  (<Tooltip title={"Test Character Sheet without sign in"}>
                    <Button variant="contained"
                            style={{backgroundColor: '#1976d2', maxWidth: '400px', alignSelf: 'center', margin: '10px'}}
                            aria-label={"test-character-sheet-without-sign-in"}
                            onClick={() => navigate("/character/", {relative:"path", replace:true})}
                    >
                        Test Character Sheet without sign in
                    </Button>
                </Tooltip>)}

                <HomePageDevider/>
                <SgTypographyPrimaryBody1 variant={'h5'}>Join Saga Guide Community</SgTypographyPrimaryBody1>
                <SgTypographyPrimaryBody1>
                    You are welcome to join the Saga Guide Discord server and Subreddit.<br />
                    There you can find guides, fill a bug report, leave a feature request, or just reach out to the developer.<br />
                    Also, you may contact developer at <span> </span>
                    {/*?subject=${encodeURIComponent(subject)}&body=${encodeURIComponent(body)}*/}
                    <Link href="mailto:sagaguideadm@gmail.com?subject=SagaGuide" target="_blank" rel="noopener noreferrer">
                        sagaguideadm@gmail.com
                    </Link><br />
                </SgTypographyPrimaryBody1>
                <Tooltip title={"Discord server"}>
                    <Button variant="contained"
                            style={{backgroundColor: '#1976d2', maxWidth: '200px', alignSelf: 'center', margin: '10px'}}
                            aria-label={"discord-server-button"}
                            href="https://discord.gg/KH8TzTMR8p"
                            target="_blank"
                            rel="noopener noreferrer"
                            startIcon={<SgDiscordIcon/>}
                    >
                        Discord
                    </Button>
                </Tooltip>
                <Tooltip title={"Subreddit"}>
                    <Button variant="contained"
                            style={{backgroundColor: '#1976d2', maxWidth: '200px', alignSelf: 'center', margin: '10px'}}
                            aria-label={"subreddit-button"}
                            href="https://www.reddit.com/r/SagaGuide/"
                            target="_blank"
                            rel="noopener noreferrer"
                            startIcon={<SgRedditIcon/>}
                    >
                        Subreddit
                    </Button>
                </Tooltip>
                
                <HomePageDevider/>
                <SgTypographyPrimaryBody1 variant={'h5'}>Support Saga Guide</SgTypographyPrimaryBody1>
                <SgTypographyPrimaryBody1>
                    Kindly think about becoming a sponsor to contribute to the ongoing development of Saga Guide.<br />
                    Even a modest sponsorship of just $1 per month can help with hosting costs and serve as motivation to keep improving Saga Guide.<br />
                    As a supporter, you'll have exclusive access to a dedicated channel on the Saga Guide Discord server. This is where you can directly communicate with me for any feature requests.<br />
                    Your suggestions as supporters mean a lot and play a big role when I'm figuring out what to tackle next.<br />
                    Become a supporter and influence the direction of Saga Guide!
                </SgTypographyPrimaryBody1>
                <Tooltip title={"By me a coffee"}>
                    <Button variant="contained"
                            style={{backgroundColor: '#1976d2', maxWidth: '200px', alignSelf: 'center', margin: '10px'}}
                            aria-label={"by-me-a-coffee"}
                            href="https://www.buymeacoffee.com/bohdanchupyna"
                            target="_blank"
                            rel="noopener noreferrer"
                            startIcon={<CoffeeIcon />}
                            >
                        Buy me a coffee
                    </Button>
                </Tooltip>
                
                <HomePageDevider/>
                <SgTypographyPrimaryBody1 variant={'h5'}>Credits</SgTypographyPrimaryBody1>
                <SgTypographyPrimaryBody1>
                    - Mykhailo Kuchuk for participation in early Saga Guide development.
                    <br />
                    
                    - <Link href="https://gurpscharactersheet.com/" target="_blank" rel="noopener noreferrer">
                        GURPS Character Sheet
                    </Link><span> </span>
                     for the inspiration. GURPS Character Sheet is a powerful desktop application for character generation.
                    <br />

                    - <Link href="https://github.com/richardwilkes/gcs_master_library" target="_blank" rel="noopener noreferrer">
                        GCS Master Library
                    </Link><span> </span>
                    community for collecting and processing GURPS rules. Without them, it would be impossible to cover all GURPS books
                    <br />
                </SgTypographyPrimaryBody1>
                
                <SgFooter/>
            </Stack>
        </PageContent>
    );
};

const HomePageDevider = () =>
{
    return (<Divider variant={'fullWidth'} style={{marginTop:"20px", marginBottom:"20px"}}/>)
}

export default Home;