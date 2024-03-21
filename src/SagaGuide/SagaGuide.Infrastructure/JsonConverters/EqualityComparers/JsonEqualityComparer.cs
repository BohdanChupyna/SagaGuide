using System.Security.Cryptography;
using System.Text;
using SagaGuide.Core.Domain.Common;

namespace SagaGuide.Infrastructure.JsonConverters.EqualityComparers;

public class JsonEqualityComparer<T> : IEqualityComparer<T> where T : GuidEntity 
{
    public virtual bool Equals(T? x, T? y)
    {
        if(x == null || y == null)
            return false;
        
        var xId = x.Id;
        x.Id = Guid.Empty;
        var xJson = JsonConverterWrapper.Serialize(x);
        x.Id = xId;
            
        var yId = y.Id;
        y.Id = Guid.Empty;
        var yJson = JsonConverterWrapper.Serialize(y);
        y.Id = yId;
            
        return xJson.ToLower().Equals(yJson.ToLower());
    }

    public virtual int GetHashCode(T obj)
    {
        // var objId = obj.Id;
        // obj.Id = Guid.Empty;
        // var json = JsonConverterWrapper.Serialize(obj);
        // var result = CalculateHash(json);
        // obj.Id = objId;
        //
        // return result;
        return 1;
    }

    private int CalculateHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convert the input string to bytes
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Compute hash value from the input bytes
            var hashBytes = sha256.ComputeHash(inputBytes);

            // Convert the hash bytes to a hexadecimal string
            return BitConverter.ToInt32(hashBytes, 0) % 1000000;
        }
    }
    
}