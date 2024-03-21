namespace SagaGuide.Infrastructure.EntityFramework.DataSeeders;

public class NameFormat
{
    private string _nameFormat;
    public NameFormat(string name)
    {
        _nameFormat = name + " ({0})";
    }

    public string GetSubName(string subName) => string.Format(_nameFormat, subName);
}