namespace DoMe.Domain.Common;

internal abstract class BaseAuditableEntity : BaseEntity
{
	public DateTime Created { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime? LastModified { get; set; }
	public string? LastModifiedBy { get; set; }
}