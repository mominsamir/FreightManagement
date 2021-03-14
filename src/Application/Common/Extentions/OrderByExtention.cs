using FreightManagement.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FreightManagement.Application.Common.Extentions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TModel> OrderByColumns<TModel>(
            this IQueryable<TModel> collection,
            IEnumerable<Sort> sortedColumns)
        {

            bool firstTime = true;

            // The type that represents each row in the table
            var itemType = typeof(TModel);

            // Name the parameter passed into the lamda "x", of the type TModel
            var parameter = Expression.Parameter(itemType, "x");

            // Loop through the sorted columns to build the expression tree
            foreach (var sortedColumn in sortedColumns)
            {
                // Get the property from the TModel, based on the key
                var prop = Expression.Property(parameter, sortedColumn.Column);

                // Build something like x => x.Cassette or x => x.SlotNumber
                var exp = Expression.Lambda(prop, parameter);

                // Based on the sorting direction, get the right method
                string method = String.Empty;
                if (firstTime)
                {
                    method = sortedColumn.SortOrder == "ascend" ? "OrderBy" : "OrderByDescending";

                    firstTime = false;
                }
                else
                {
                    method = sortedColumn.SortOrder == "ascend" ? "ThenBy" : "ThenByDescending";
                }

                Type[] types = new Type[] { itemType, exp.Body.Type };

                var mce = Expression.Call(typeof(Queryable), method, types, collection.Expression, exp);

                collection = collection.Provider.CreateQuery<TModel>(mce);
            }

            return collection;
        }
    }

}
