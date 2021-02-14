using FreightManagement.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace FreightManagement.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
