namespace SagaGuide.Core.Domain.Common;

public interface IAuditable
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid ModifiedBy { get; set; }

    public DateTime ModifiedOn { get; set; }
    
    public uint Version { get; set; }
}