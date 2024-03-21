import React from "react";
import {Stack} from "@mui/material";
import {PageContent } from "../../components/sgNavBar/SgNavBar";
import { pageContentMaxWidth } from "../../components/sgTheme";
import { SgTypographyPrimaryBody1 } from "../../components/SgTypography";
import {useRouteError} from "react-router-dom";
import {useAppSelector} from "../../redux/hooks";
import {selectIsNavBarOpen} from "../../redux/slices/navBar/navBarSlice";

const Error = () => {
    const error: any = useRouteError();
    console.error(error);
    
    const isNavBarOpen = useAppSelector(selectIsNavBarOpen);
    let errorMessage = error?.statusText || error?.message;

    return (
        <PageContent container id="error-page-root-id"  open={isNavBarOpen} justifyContent="center"  alignContent='center' justifyItems={'center'}>
            <Stack justifyContent="center" style={{maxWidth:pageContentMaxWidth}}>
                <SgTypographyPrimaryBody1 variant={'h5'} textAlign={'center'}>Oops!</SgTypographyPrimaryBody1>
                <SgTypographyPrimaryBody1 textAlign={'center'}>
                    Sorry, an unexpected error has occurred.<br/>
                    {errorMessage ? errorMessage : "Error message is empty."}
                </SgTypographyPrimaryBody1>
            </Stack>
        </PageContent>
    );
};

export default Error;