using Ambev.DeveloperEvaluation.Common.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    [NotMapped]
    public List<BaseEvent> DomainEvents { get; private set; } = new();

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }
    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        DomainEvents.Remove(domainEvent);
    }
    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }
}
