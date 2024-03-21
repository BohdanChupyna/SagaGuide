import HomeRoundedIcon from '@mui/icons-material/HomeRounded';
import Groups2RoundedIcon from '@mui/icons-material/Groups2Rounded';
import PersonAddAlt1RoundedIcon from '@mui/icons-material/PersonAddAlt1Rounded';
import {ReactElement} from "react";
import {SgNavBarAriaLabels} from "./SgNavBarAriaLabels";
import {usePostCharacterMutation} from "../../redux/slices/api/apiSlice";


export interface IMainNavbarItem
{
    id: number,
    icon: ReactElement,
    label: string,
    route: string,
    ariaLabel: string,
    needAuth: boolean,
}

export const sgNavbarItems: IMainNavbarItem[] = [
    {
        id: 0,
        icon: <HomeRoundedIcon/>,
        label: 'Home',
        route: 'home',
        ariaLabel: SgNavBarAriaLabels.HomePageButton,
        needAuth: false,
    },
    {
        id: 1,
        icon: <PersonAddAlt1RoundedIcon/>,
        label: 'New character',
        route: 'character',
        ariaLabel: SgNavBarAriaLabels.NewCharacterPageButton,
        needAuth: false,
    },
    {
        id: 2,
        icon: <Groups2RoundedIcon/>,
        label: 'Characters',
        route: 'characters',
        ariaLabel: SgNavBarAriaLabels.CharactersPageButton,
        needAuth: true,
    }
]