namespace RentARide.Domain.Common
{
    public interface IAuditable
    {
        
        DateTime? LastModifiedAt { get; set; }
    }
}
