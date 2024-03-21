export default interface IGuid
{
    id: string
}

export interface IAuditable extends IGuid
{
    createdBy:string,
    CreatedOn: string,
    ModifiedBy: string,
    ModifiedOn: string,
}