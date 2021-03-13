
namespace FreightManagement.Application.Common.Models
{
    public class Filter
    {
        public string Name { get; }
        public string Value { get; }
        public FieldOperator Operator { get; } = 0;


        public Filter(string name, string value, FieldOperator @operator)
        {
            Name = name;
            Value = value;
            Operator = @operator;
        }

    }

    public enum FieldOperator
    {
        EQUAL = 0,
        NOT_EQUAL = 1,
        CONTAINT = 2,
        NOT_CONTAINT = 3
    }
}
