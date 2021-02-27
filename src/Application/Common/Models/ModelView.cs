
namespace FreightManagement.Application.Common.Models
{
    public class ModelView<T>
    {
        public T Model { get; }
        public bool IsEditable { get; }
        public bool IsDeletable { get; }
        public bool AddNew { get; }

        public ModelView() { }
        public ModelView(T entity)
        {
            Model = entity;
        }

        public ModelView(T model, bool isEditable, bool isDeletable, bool addNew)
        {
            Model = model;
            IsEditable = isEditable;
            IsDeletable = isDeletable;
            AddNew = addNew;
        }
    }
}
