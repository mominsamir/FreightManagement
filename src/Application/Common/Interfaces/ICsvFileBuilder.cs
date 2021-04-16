using FreightManagement.Domain.Entities.Users;
using System.Collections.Generic;

namespace FreightManagement.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<User> records);
    }
}
