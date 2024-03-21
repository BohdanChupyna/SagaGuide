import {IsUndefinedOrNull} from "../commonUtils";

export default interface IBookReference
{
    pageNumber: number,
    sourceBook: string,
    magazineNumber: number|null
}

export function getBookReferencesAsString(bookReferences: IBookReference[]): string
{
    if(bookReferences === null || bookReferences === undefined)
        return "";
    
    let result = "";
    for(let i = 0; i < bookReferences.length; ++i)
    {
        result += bookReferences[i].sourceBook;
        if(!IsUndefinedOrNull(bookReferences[i].magazineNumber))
        {
            result += bookReferences[i].magazineNumber;
        }
        result += `:${bookReferences[i].pageNumber}`;

        if(i != (bookReferences.length-1))
        {
            result += ", ";
        }
    }

    return result;
}