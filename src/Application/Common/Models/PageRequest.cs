
namespace FreightManagement.Application.Common.Models
{
    public class Filter
    {
        public string Name { get; }
        public string Value { get; }
        public string Operator { get; }

        public Filter(string name, string value, string @operator)
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
        NOT_CONTAINT = 3,
        STARTS_WITH = 4
    }

    public class Sort
    {
        public Sort(string column, string sortOrder)
        {
            Column = column;
            SortOrder = sortOrder;
        }

        public string Column { get; }
        public string SortOrder { get; }
    }

    public enum SortDirection
    {
        Ascending = 0,
        Descending = 1
    }
}
