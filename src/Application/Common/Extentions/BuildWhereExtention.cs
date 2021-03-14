﻿using FreightManagement.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

//TODO: dot notation properties filtering https://stackoverflow.com/questions/47364344/filtering-but-property-and-child-entity-property
//TODO: In clause implementation
//TODO: is Null or is not null implementation
//TODO: Date Between to be implemented
//TODO: Number filter to be tested.


namespace FreightManagement.Application.Common.Extentions
{
    public static class BuildWhereExtention
    {
		public static IQueryable<T> WhereRules<T>(this IQueryable<T> source, IEnumerable<Filter> filters)
		{
			if (!filters.Any())
				return source;


			var parameter = Expression.Parameter(typeof(T));
			BinaryExpression binaryExpression = null;

			foreach (var filter in filters)
            {
                var prop = Expression.Property(parameter, filter.Name);
                var value = Expression.Constant(filter.Value);
                var property = typeof(T).GetProperty(filter.Name);
                BinaryExpression newBinary = null;


                if (prop.Type == typeof(string))
                {
                    newBinary = GetBinaryExpression(filter, prop, value);
                } else if (prop.Type == typeof(DateTime))
                {
                    var date  = DateTime.Parse(filter.Value);
                    newBinary = GetBinaryExpression(filter, prop, Expression.Constant(date));
                }else if (prop.Type == typeof(decimal))
                {
                    var number = decimal.Parse(filter.Value);
                    newBinary = GetBinaryExpression(filter, prop, Expression.Constant(number));
                }
                else if (prop.Type == typeof(int))
                {
                    var number = int.Parse(filter.Value);
                    newBinary = GetBinaryExpression(filter, prop, Expression.Constant(number));
                }
                else if (prop.Type == typeof(long))
                {
                    var number = long.Parse(filter.Value);
                    newBinary = GetBinaryExpression(filter, prop, Expression.Constant(number));
                }
                else if (prop.Type == typeof(bool))
                {
                    var boolValue = bool.Parse(filter.Value);
                    newBinary = GetBinaryExpression(filter, prop, Expression.Constant(boolValue));
                }
                else if (prop.Type.IsEnum)
                {
                    var number = int.Parse(filter.Value);
                    newBinary = GetBinaryExpression(filter, prop, Expression.Constant(number));
                }


                if (newBinary is not null)
                    binaryExpression = binaryExpression == null
                        ? newBinary : Expression.MakeBinary(ExpressionType.AndAlso, binaryExpression, newBinary);
            }
            if (binaryExpression is null)
				return source;

			var _where = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
			return source.Where(_where);
		}


        private static BinaryExpression GetBinaryExpression(Filter filter, MemberExpression prop, ConstantExpression value)
        {
            MethodInfo method = null;
            ConstantExpression zero = null;
            MethodCallExpression result = null;
            switch (filter.Operator)
            {
                case "EQUAL":
                case "NOT_EQUAL":
                case "GREATER_THAN":
                case "GREATER_THAN_OR_EQUAL":
                case "LESS_THAN":
                case "LESS_THAN_OR_EQUAL":
                    return Expression.MakeBinary(GetExpression(filter.Operator), prop, value);
                case "CONTAIN":
                    method = prop.Type.GetMethod("Contains", new[] { typeof(string) });
                    zero = Expression.Constant(true);
                    result = Expression.Call(prop, method, value);
                    return Expression.MakeBinary(ExpressionType.Equal, result, zero);
                case "DOES_NOT_CONTAIN":
                    method = prop.Type.GetMethod("Contains", new[] { typeof(string) });
                    zero = Expression.Constant(false);
                    result = Expression.Call(prop, method, value);
                    return Expression.MakeBinary(ExpressionType.Equal, result, zero);
                case "STARTS_WITH":
                    method = prop.Type.GetMethod("StartsWith", new[] { typeof(string) });
                    zero = Expression.Constant(true);
                    result = Expression.Call(prop, method, value);
                    return Expression.MakeBinary(ExpressionType.Equal, result, zero);
                case "ENDS_WITH":
                    method = prop.Type.GetMethod("EndsWith", new[] { typeof(string) });
                    zero = Expression.Constant(true);
                    result = Expression.Call(prop, method, value);
                    return Expression.MakeBinary(ExpressionType.Equal, result, zero);
                case "IS_EMPTY":
                    method = prop.Type.GetMethod("Length", new[] { typeof(string) });
                    zero = Expression.Constant(string.Empty);
                    result = Expression.Call(prop, method, value);
                    return Expression.MakeBinary(ExpressionType.Equal, result, zero);
                case "NOT_EMPTY":
                    method = prop.Type.GetMethod("Length", new[] { typeof(string) });
                    zero = Expression.Constant(string.Empty);
                    result = Expression.Call(prop, method, value);
                    return Expression.MakeBinary(ExpressionType.Equal, result, zero);
            }
            return null;
        }


        private static ExpressionType GetExpression(string operation)
        {
            return operation switch
            {
                "EQUAL" => ExpressionType.Equal,
				"NOT_EQUAL" => ExpressionType.NotEqual,
				"GREATER_THAN" => ExpressionType.GreaterThan,
				"GREATER_THAN_OR_EQUAL" => ExpressionType.GreaterThanOrEqual,
				"LESS_THAN" => ExpressionType.LessThan,
				"LESS_THAN_OR_EQUAL" => ExpressionType.LessThanOrEqual,

				"CONTAINT" => ExpressionType.Equal,
				"DOES_NOT_CONTAIN" => ExpressionType.NotEqual,
				"STARTS_WITH" => ExpressionType.Equal,
				"ENDS_WITH" => ExpressionType.Equal,
				"IS_EMPTY" => ExpressionType.Equal,
				"NOT_EMPTY" => ExpressionType.Equal,
                _ => ExpressionType.Equal,
            };
        }

        private static Expression GetConvertedSource(ParameterExpression sourceParameter, PropertyInfo sourceProperty, TypeCode typeCode)
        {
            var sourceExpressionProperty = Expression.Property(sourceParameter, sourceProperty);

            var changeTypeCall = Expression.Call(typeof(Convert).GetMethod("ChangeType", new[] { typeof(object), typeof(TypeCode) }), sourceExpressionProperty, Expression.Constant(typeCode));

            Expression convert = Expression.Convert(changeTypeCall, Type.GetType("System." + typeCode));

            var convertExpr = Expression.Condition(Expression.Equal(sourceExpressionProperty,
                                                    Expression.Constant(null, sourceProperty.PropertyType)),
                                                    Expression.Default(Type.GetType("System." + typeCode)),
                                                    convert);



            return convertExpr;
        }

        public static Expression IsDateBetween<TElement>(this IQueryable<TElement> queryable,
                                                       Expression<Func<TElement, DateTime>> fromDate,
                                                       Expression<Func<TElement, DateTime>> toDate,
                                                       DateTime date)
        {
            var p = fromDate.Parameters.Single();
            Expression member = p;

            Expression fromExpression = Expression.Property(member, (fromDate.Body as MemberExpression).Member.Name);
            Expression toExpression = Expression.Property(member, (toDate.Body as MemberExpression).Member.Name);

            var after = Expression.LessThanOrEqual(fromExpression, Expression.Constant(date, typeof(DateTime)));

            var before = Expression.GreaterThanOrEqual( toExpression, Expression.Constant(date, typeof(DateTime)));

            Expression body = Expression.And(after, before);

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }


    }


}
