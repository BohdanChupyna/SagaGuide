import {IsUndefinedOrNull} from "./commonUtils";

export function numberToSignString(num: number)
{
    let result = "";
    let sign = Math.sign(num);

    switch (sign) {
        case -1:
            result += "-";
            break;
        case 0:
        case 1:
            result += "+";
    }

    result += `${Math.abs(num)}`;
    return result;
}

export function hasSpecializationSymbol(value: string|null|undefined)
{
    if(IsUndefinedOrNull(value))
        return false;
    
    return value!.includes("@");
}

export const SpecializationReplacementRegExp = /@([^@]+)@/;