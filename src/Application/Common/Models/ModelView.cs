
namespace FreightManagement.Application.Common.Models
{
    public class ModelView<T>
    {
        public T Model;
        public bool IsEditable;
        public bool IsDeletable;
        public bool AddNew;

        public ModelView()
        {
        }

        public ModelView(T entity)
        {
            Model = entity;
        }
    }
}
