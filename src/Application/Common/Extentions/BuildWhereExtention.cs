using FreightManagement.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FreightManagement.Application.Common.Extentions
{
    public static class BuildWhereExtention
    {
		public static IQueryable<T> WhereRules<T>(this IQueryable<T> source, IEnumerable<Filter> filters)
		{
			var parameter = Expression.Parameter(typeof(T));
			BinaryExpression binaryExpression = null;

            if (!filters.Any())
            {
				return source;
            }

			foreach (var filter in filters)
			{
				var prop = Expression.Property(parameter, filter.Name);
				var value = Expression.Constant(filter.Value);
				var newBinary = Expression.MakeBinary(GetExpression(filter.Operator), prop, value);

				binaryExpression =
					binaryExpression == null
					? newBinary
					: Expression.MakeBinary(ExpressionType.AndAlso, binaryExpression, newBinary);
			}

			var _where = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter).Compile();
			source.Where(_where);
			return source;
		}

		private static ExpressionType GetExpression(FieldOperator operation)
        {
            switch (operation)
            {
				case  FieldOperator.EQUAL:
					return ExpressionType.Equal;
				case FieldOperator.CONTAINT:
					return ExpressionType.Equal;
				case FieldOperator.NOT_CONTAINT:
					return ExpressionType.NotEqual;
				case FieldOperator.NOT_EQUAL:
					return ExpressionType.NotEqual;
				default:
					return ExpressionType.Equal;

			}
        }
    }
}
