namespace SagaGuide.Api.Contract;

public abstract class AuditableViewModel : GuidViewModel
{
    /// <summary>
    ///     User who created the entity
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    ///     Creation DateTime of the entity
    /// </summary>
    public DateTime CreatedOn { get; set; }
    
    /// <summary>
    ///     User who modified the entity
    /// </summary>
    public Guid ModifiedBy { get; set; }

    /// <summary>
    ///     Modified DateTime of the entity
    /// </summary>
    public DateTime ModifiedOn { get; set; }
    
    /// <summary>
    ///     entity version used for optimistic concurrency
    /// </summary>
    public uint Version { get; set; }
}