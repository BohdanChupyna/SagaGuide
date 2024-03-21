import {IsUndefinedOrNull} from "../domain/commonUtils";
import ITrait from "../domain/interfaces/trait/ITrait";

export function adaptNameForAriaLabel(name: string)
{
    return name.toLowerCase().replace(/\s+/g, "-");
}

export function TagsArrayToString(tags: Array<string>): string
{
    if(IsUndefinedOrNull(tags) || tags.length === 0)
        return "";

    return [...tags].sort().toString().replaceAll(",", ", ");
}