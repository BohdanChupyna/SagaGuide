using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SagaGuide.Infrastructure.JsonConverters;

namespace SagaGuide.Infrastructure.EntityFramework;

public class ListJsonConverter<T> : ValueConverter<List<T>, string>
{
    public ListJsonConverter()
        : base(
            v => JsonConverterWrapper.Serialize(v, null),
            v => JsonConverterWrapper.Deserialize<List<T>>(v, null)!)
    {
    }
}

public class ListComparer<T> : ValueComparer<List<T>>
{
    public ListComparer()
        : base((c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v != null ? v.GetHashCode() : 0)),
            c => c.ToList())
    {
    }
}


public class DmsJsonConverter<T> : ValueConverter<T, string>
{
    public DmsJsonConverter()
        : base(
            v => JsonConverterWrapper.Serialize(v, null),
            v => JsonConverterWrapper.Deserialize<T>(v, null)!)
    {
    }
}  

public class DmsJsonComparer<T> : ValueComparer<T>
{
    public DmsJsonComparer()
        : base((c1, c2) => c1 != null && c2 != null &&c1.Equals(c2),
            c => c != null ? c.GetHashCode() : 0)
    {
    }
}