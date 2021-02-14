using FreightManagement.Domain.Common;
using System.Threading.Tasks;

namespace FreightManagement.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
