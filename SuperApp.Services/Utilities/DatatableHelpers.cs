using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace SuperApp.Services.Utilities
{
    internal class DatatableHelpers
    {
        private static readonly ConcurrentDictionary<Type, Action<IEnumerable<object>, DataTable>> _mappers = 
            new ConcurrentDictionary<Type, Action<IEnumerable<object>,DataTable>>();
        public static class DataTableHelper
        {
            private static readonly ConcurrentDictionary<Type, Action<IEnumerable<object>, DataTable>> _mappers =
                new ConcurrentDictionary<Type, Action<IEnumerable<object>, DataTable>>();
            public static DataTable ConvertirDTOaDataTable<T>(IEnumerable<T> items)
            {
                // Crear DataTable basado en el tipo del DTO
                var dataTable = new DataTable(typeof(T).Name);
                // Obtener el mapeo compilado, o crearlo si no existe
                var mapper = _mappers.GetOrAdd(typeof(T), type => CreateMapper(type, dataTable));
                // Ejecutar el mapeo compilado
                mapper(items.Cast<object>(), dataTable);
                return dataTable;
            }
            private static Action<IEnumerable<object>, DataTable> CreateMapper(Type type, DataTable dataTable)
            {
                var paramItems = Expression.Parameter(typeof(IEnumerable<object>), "items");
                var paramTable = Expression.Parameter(typeof(DataTable), "table");
                // Crear las columnas en el DataTable basadas en las propiedades del DTO
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var blockExpression = new List<Expression>();
                var castedItem = Expression.Variable(type, "castedItem");
                var castExpression = Expression.Assign(castedItem, Expression.Convert(paramItems, type));
                blockExpression.Add(castExpression);
                var dataRowVariable = Expression.Variable(typeof(DataRow), "dataRow");
                var newRowExpression = Expression.Assign(dataRowVariable, Expression.Call(paramTable, "NewRow", null));
                blockExpression.Add(newRowExpression);
                foreach (var prop in properties)
                {
                    if(!dataTable.Columns.Contains(prop.Name))
                    {
                        dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }
                    var propertyValue = Expression.Property(castedItem, prop);
                    var convertValue=Expression.Convert(propertyValue, typeof(object));

                    var setColumnValue = Expression.Assign(Expression.Property(dataRowVariable, "item", Expression.Constant(prop.Name)), convertValue);
                    blockExpression.Add(setColumnValue);
                }
                var loopBody = Expression.Block(new[] {castedItem,dataRowVariable},blockExpression);
                var loopParameter = Expression.Parameter(typeof(object), "item");
                var loop = Expression.Call(typeof(Enumerable), "ForEach", new[] { typeof(object) }, paramItems, Expression.Lambda<Action<object>>(loopBody, loopParameter));
                var lambda=Expression.Lambda<Action<IEnumerable<object>,DataTable>>(loop, paramItems,paramTable);
                return lambda.Compile();
            }
        }
    }
}
