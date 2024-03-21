export enum StringCriteriaComparisonTypeEnum
{
    Any = "Any",
    Is = "Is",
    IsNot = "IsNot",
    Contains = "Contains",
    DoesNotContain = "DoesNotContain",
    StartsWith = "StartsWith",
    DoesNotStartWith = "DoesNotStartWith",
    EndsWith = "EndsWith",
    DoesNotEndWith = "DoesNotEndWith",
}

export interface IStringCriteria
{
    comparison: StringCriteriaComparisonTypeEnum,
    qualifier: string,
}