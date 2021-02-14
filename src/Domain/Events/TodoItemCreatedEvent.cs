using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities;

namespace FreightManagement.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
