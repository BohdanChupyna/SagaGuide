export enum NumericCriteriaComparisonTypeEnum
{
    Any = "Any",
    Equal = "Equal",
    NotEqual = "NotEqual",
    AtLeast = "AtLeast",
    AtMost = "AtMost"
}

export interface INumericCriteria
{
    comparison: NumericCriteriaComparisonTypeEnum,
    qualifier: number,
}