using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities;

namespace FreightManagement.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
