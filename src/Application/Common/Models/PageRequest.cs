
namespace FreightManagement.Application.Common.Models
{
    public class Filter
    {
        public string Name;
        public string Value;
        public FieldOperator Operator { get; } = 0;

    }

    public enum FieldOperator
    {
        EQUAL = 0,
        NOT_EQUAL = 1,
        CONTAINT = 2,
        NOT_CONTAINT = 3
    }
}
