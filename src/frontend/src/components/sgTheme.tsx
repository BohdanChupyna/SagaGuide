import {createTheme, styled} from '@mui/material/styles';
import {Paper, responsiveFontSizes, Tooltip, TooltipProps} from "@mui/material";

const sgTheme = () => {
    let theme = createTheme({
        palette: {
            mode: 'light',
        },
        spacing: 8,
        breakpoints: {
            values: {
                xs: 0,
                sm: 720,
                md: 1020,
                lg: 1320,
                xl: 1536,
            },
        },
    });

    theme = responsiveFontSizes(theme);
    return theme;
};

export const pageContentMaxWidth = "1200px";

export default sgTheme;

export const StyledListItemPaper = styled(Paper)(({ theme }) => ({
    '&:hover': {
        backgroundColor: '#E6E6E6',
        // backgroundColor: theme.palette.primary.main,
    },
}));

export const StyledLeftListItemTooltip = ({title, children }:TooltipProps ) => {
    return (<Tooltip 
        title={title}
        placement={'left'}
        enterTouchDelay={1*1000}
        leaveTouchDelay={0}
        enterDelay={1*1000}
        leaveDelay={0}>
            {children}
        </Tooltip>);
};