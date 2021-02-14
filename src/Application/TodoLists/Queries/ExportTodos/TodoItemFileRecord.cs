using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities;

namespace FreightManagement.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
