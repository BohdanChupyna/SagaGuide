import {AppDispatch, RootState} from "../../store";
import {AnyAction, ThunkAction} from "@reduxjs/toolkit";
import {v4 as uuidv4} from "uuid";


export enum ISgToastTypeEnum
{
    Success = "success",
    Error = "error",
    Warning = "warning",
    Info = "info",
}
export interface ISgToast {
    message: string,
    messageType: ISgToastTypeEnum,
    isShown: boolean,
    id: string,
    time: number,
}

export interface IToastProviderSliceState {
    toast: ISgToast
}

export function constructToast(message: string, messageType: ISgToastTypeEnum): ISgToast
{
    return  {
        time: Date.now(),
        id:  uuidv4(),
        isShown: false,
        message: message,
        messageType: messageType
    };
}

export function constructInfoToast(message: string): ISgToast
{
    return  constructToast(message, ISgToastTypeEnum.Info);
}

export function constructWarningToast(message: string): ISgToast
{
    return  constructToast(message, ISgToastTypeEnum.Warning);
}

export function constructSuccessToast(message: string): ISgToast
{
    return  constructToast(message, ISgToastTypeEnum.Success);
}

export function constructErrorToast(message: string): ISgToast
{
    return  constructToast(message, ISgToastTypeEnum.Error);
}