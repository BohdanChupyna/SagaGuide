import * as React from 'react';
import Typography from "@mui/material/Typography";
import {styled} from "@mui/material/styles";
import {SgCharacterSheetAriaLabels} from "./sgCharacterSheet/SgCharacterSheetAriaLabels";

export const SgTypographyWithLeftPadding = styled(Typography)(({ theme }) => ({
    paddingLeft: theme.spacing(1),
    wordBreak: "break-word",
}));

export const SgTypographyWithRightPadding = styled(Typography)(({ theme }) => ({
    paddingRight: theme.spacing(1),
    wordBreak: "break-word",
}));

export const SgTypographyPrimaryBody1 = styled(Typography)(({ theme }) => ({
    wordBreak: "break-word",
    variant: 'body1',
    color: theme.palette.text.primary
}));

export const SgTypographySecondaryBody1 = styled(Typography)(({ theme }) => ({
    wordBreak: "break-word",
    variant: 'body1',
    color: theme.palette.text.secondary
}));

export const SgTypographyPrimaryBody2 = styled(Typography)(({ theme }) => ({
    wordBreak: "break-word",
    variant: 'body2',
    fontSize: 14,
    color: theme.palette.text.primary
}));

export const SgTypographySecondaryBody2 = styled(Typography)(({ theme }) => ({
    wordBreak: "break-word",
    variant: 'body2',
    fontSize: 14,
    color: theme.palette.text.secondary,
}));

export const SgTypographySubtitle2WithLeftPadding = styled(Typography)(({ theme }) => ({
    paddingLeft: theme.spacing(1),
    wordBreak: "break-word",
    variant: "subtitle2",
}));

export const SgTypographyPrimarySubtitle2 = styled(Typography)(({ theme }) => ({
    wordBreak: "break-word",
    variant: 'subtitle2',
    color: theme.palette.text.primary
}));

export const SgTypographySecondarySubtitle2 = styled(Typography)(({ theme }) => ({
    wordBreak: "break-word",
    variant: 'subtitle2',
    fontSize: 14,
    color: theme.palette.text.secondary
}));

