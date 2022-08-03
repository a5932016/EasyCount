using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Database
{
    /// <summary>
    /// 數據庫數據轉換擴展
    /// </summary>
    public static class DbDataConvertExtensions
    {
        /// <summary>
        /// 將DataTable轉List集合
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns>List{T}</returns>
        public static List<T> ToList<T>(this DataTable dataTable)
        {
            return dataTable.ToList(typeof(List<T>)) as List<T>;
        }

        /// <summary>
        /// 將DataTable轉List集合
        /// </summary>
        /// <typeparam name="T">返回值類型</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns>List{T}</returns>
        public static async Task<List<T>> ToListAsync<T>(this DataTable dataTable)
        {
            var list = await dataTable.ToListAsync(typeof(List<T>));
            return list as List<T>;
        }

        /// <summary>
        /// 將DataSet轉List集合
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>List{T}</returns>
        public static List<T> ToList<T>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T>));
            return tuple[0] as List<T>;
        }

        /// <summary>
        /// 將DataSet轉元組
        /// </summary>
        /// <typeparam name="T1">元組元素類型</typeparam>
        /// <typeparam name="T2">元組元素類型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>元組類型</returns>
        public static (List<T1> list1, List<T2> list2) ToList<T1, T2>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>);
        }

        /// <summary>
        /// 將DataSet轉元組
        /// </summary>
        /// <typeparam name="T1">元組元素類型</typeparam>
        /// <typeparam name="T2">元組元素類型</typeparam>
        /// <typeparam name="T3">元組元素類型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>元組類型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3) ToList<T1, T2, T3>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>);
        }

        /// <summary>
        /// 將DataSet轉元組
        /// </summary>
        /// <typeparam name="T1">元組元素類型</typeparam>
        /// <typeparam name="T2">元組元素類型</typeparam>
        /// <typeparam name="T3">元組元素類型</typeparam>
        /// <typeparam name="T4">元組元素類型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>元組類型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) ToList<T1, T2, T3, T4>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>);
        }

        /// <summary>
        /// 將DataSet轉元組
        /// </summary>
        /// <typeparam name="T1">元組元素類型</typeparam>
        /// <typeparam name="T2">元組元素類型</typeparam>
        /// <typeparam name="T3">元組元素類型</typeparam>
        /// <typeparam name="T4">元組元素類型</typeparam>
        /// <typeparam name="T5">元組元素類型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>元組類型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) ToList<T1, T2, T3, T4, T5>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>), typeof(List<T5>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>, tuple[4] as List<T5>);
        }

        /// <summary>
        /// 將DataSet轉元組
        /// </summary>
        /// <typeparam name="T1">元組元素類型</typeparam>
        /// <typeparam name="T2">元組元素類型</typeparam>
        /// <typeparam name="T3">元組元素類型</typeparam>
        /// <typeparam name="T4">元組元素類型</typeparam>
        /// <typeparam name="T5">元組元素類型</typeparam>
        /// <typeparam name="T6">元組元素類型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>元組類型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) ToList<T1, T2, T3, T4, T5, T6>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>), typeof(List<T5>), typeof(List<T6>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>, tuple[4] as List<T5>, tuple[5] as List<T6>);
        }

        /// <summary>
        /// 將DataSet轉元組
        /// </summary>
        /// <typeparam name="T1">元組元素類型</typeparam>
        /// <typeparam name="T2">元組元素類型</typeparam>
        /// <typeparam name="T3">元組元素類型</typeparam>
        /// <typeparam name="T4">元組元素類型</typeparam>
        /// <typeparam name="T5">元組元素類型</typeparam>
        /// <typeparam name="T6">元組元素類型</typeparam>
        /// <typeparam name="T7">元組元素類型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>元組類型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) ToList<T1, T2, T3, T4, T5, T6, T7>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>), typeof(List<T5>), typeof(List<T6>), typeof(List<T7>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>, tuple[4] as List<T5>, tuple[5] as List<T6>, tuple[6] as List<T7>);
        }

        /// <summary>
        /// 將DataSet轉元組
        /// </summary>
        /// <typeparam name="T1">元組元素類型</typeparam>
        /// <typeparam name="T2">元組元素類型</typeparam>
        /// <typeparam name="T3">元組元素類型</typeparam>
        /// <typeparam name="T4">元組元素類型</typeparam>
        /// <typeparam name="T5">元組元素類型</typeparam>
        /// <typeparam name="T6">元組元素類型</typeparam>
        /// <typeparam name="T7">元組元素類型</typeparam>
        /// <typeparam name="T8">元組元素類型</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>元組類型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) ToList<T1, T2, T3, T4, T5, T6, T7, T8>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>), typeof(List<T5>), typeof(List<T6>), typeof(List<T7>), typeof(List<T8>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>, tuple[4] as List<T5>, tuple[5] as List<T6>, tuple[6] as List<T7>, tuple[7] as List<T8>);
        }

        /// <summary>
        /// 將DataSet轉特定類型
        /// </summary>
        /// <param name="dataSet">DataSet</param>
        /// <param name="returnTypes">特定類型集合</param>
        /// <returns>List{object}</returns>
        public static List<object> ToList(this DataSet dataSet, params Type[] returnTypes)
        {
            if (returnTypes == null || returnTypes.Length == 0) return default;

            // 處理元組類型
            if (returnTypes.Length == 1 && returnTypes[0].IsValueType)
            {
                returnTypes = returnTypes[0].GenericTypeArguments;
            }

            // 獲取所有的 DataTable
            var dataTables = dataSet.Tables;

            // 處理結果集
            var list = new List<object>();
            for (int idx = 0; idx < returnTypes.Length; idx++)
            {
                list.Add(dataTables[idx].ToList(returnTypes[idx]));
            }

            return list;
        }

        /// <summary>
        /// 將DataSet轉特定類型
        /// </summary>
        /// <param name="dataSet">DataSet</param>
        /// <param name="returnTypes">特定類型集合</param>
        /// <returns>object</returns>
        public static Task<List<object>> ToListAsync(this DataSet dataSet, params Type[] returnTypes)
        {
            return Task.FromResult(dataSet.ToList(returnTypes));
        }

        /// <summary>
        /// 將DataTable轉特定類型
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="returnType">返回值類型</param>
        /// <returns>object</returns>
        public static object ToList(this DataTable dataTable, Type returnType)
        {
            var isGenericType = returnType.IsGenericType;

            // 獲取類型真實返回類型
            var underlyingType = isGenericType ? returnType.GenericTypeArguments.First() : returnType;

            var resultType = typeof(List<>).MakeGenericType(underlyingType);
            var list = Activator.CreateInstance(resultType);
            var addMethod = resultType.GetMethod("Add");

            // 將DataTable轉為行集合
            var dataRows = dataTable.AsEnumerable();

            // 如果是基元類型
            if (underlyingType.IsRichPrimitive())
            {
                // 遍歷所有行
                foreach (var dataRow in dataRows)
                {
                    // 只取第一列數據
                    var firstColumnValue = dataRow[0];

                    // 轉換成目標類型數據
                    var destValue = firstColumnValue?.ChangeType(underlyingType);

                    // 添加到集合中
                    _ = addMethod.Invoke(list, new[] { destValue });
                }
            }
            // 處理Object類型
            else if (underlyingType == typeof(object))
            {
                // 獲取所有列名
                var columns = dataTable.Columns;

                // 遍歷所有行
                foreach (var dataRow in dataRows)
                {
                    var dic = new Dictionary<string, object>();
                    foreach (DataColumn column in columns)
                    {
                        dic.Add(column.ColumnName, dataRow[column]);
                    }

                    _ = addMethod.Invoke(list, new[] { dic });
                }
            }
            else
            {
                // 獲取所有的數據列和列公開實例屬性
                var dataColumns = dataTable.Columns;
                var properties = underlyingType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                //.Where(p => !p.IsDefined(typeof(NotMappedAttribute), true));  // sql 數據轉換無須判斷 [NotMapperd] 特性

                // 遍歷所有行
                foreach (var dataRow in dataRows)
                {
                    var model = Activator.CreateInstance(underlyingType);

                    // 遍歷所有屬性並一一賦值
                    foreach (var property in properties)
                    {
                        // 获取属性对应的真实列名
                        var columnName = property.Name;
                        if (property.IsDefined(typeof(ColumnAttribute), true))
                        {
                            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>(true);
                            if (!string.IsNullOrWhiteSpace(columnAttribute.Name)) columnName = columnAttribute.Name;
                        }

                        // 如果 DataTable 不包含该列名，则跳过
                        if (!dataColumns.Contains(columnName)) continue;

                        // 获取列值
                        var columnValue = dataRow[columnName];
                        // 如果列值未空，则跳过
                        if (columnValue == DBNull.Value) continue;

                        // 转换成目标类型数据
                        var destValue = columnValue?.ChangeType(property.PropertyType);
                        property.SetValue(model, destValue);
                    }

                    // 添加到集合中
                    _ = addMethod.Invoke(list, new[] { model });
                }
            }

            return list;
        }

        /// <summary>
        /// 將DataTable轉特定類型
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="returnType">返回值類型</param>
        /// <returns>object</returns>
        public static Task<object> ToListAsync(this DataTable dataTable, Type returnType)
        {
            return Task.FromResult(dataTable.ToList(returnType));
        }

        /// <summary>
        /// 處理元組類型返回值处理元组类型返回值
        /// </summary>
        /// <param name="dataSet">數據集</param>
        /// <param name="tupleType">返回值類型</param>
        /// <returns></returns>
        internal static object ToValueTuple(this DataSet dataSet, Type tupleType)
        {
            // 獲取元組最底層類型
            var underlyingTypes = tupleType.GetGenericArguments().Select(u => u.IsGenericType ? u.GetGenericArguments().First() : u);

            var toListMethod = typeof(DbDataConvertExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(u => u.Name == "ToList" && u.IsGenericMethod && u.GetGenericArguments().Length == tupleType.GetGenericArguments().Length)
                .MakeGenericMethod(underlyingTypes.ToArray());

            return toListMethod.Invoke(null, new[] { dataSet });
        }
    }
}
