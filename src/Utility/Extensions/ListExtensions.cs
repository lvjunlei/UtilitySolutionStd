#region ListExtensions 文件信息
/***********************************************************
**文 件 名：ListExtensions 
**命名空间：Utility.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2016-10-22 23:02:17 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Utility.Extensions
{
    /// <summary>
    /// List扩展集合
    /// </summary>
    public static class ListExtensions
    {
        #region QuickSort 快速排序算法

        /// <summary>
        /// 快速排序算法
        /// 按指定的方向对指定的列表进行排序。
        /// </summary>
        /// <typeparam name="T">要排序的元素的类型</typeparam>
        /// <param name="toSort">要排序的列表。</param>
        /// <param name="direction">在侵权行为中排序元素的方向。</param>
        /// <param name="startIndex">开始索引。</param>
        /// <param name="endIndex">结束索引。</param>
        /// <param name="compareFunc">比较功能。</param>
        public static void QuickSort<T>(this IList<T> toSort, SortDirection direction, int startIndex, int endIndex, Comparison<T> compareFunc)
        {
            Func<T, T, bool> valueComparerTest;
            switch (direction)
            {
                case SortDirection.Ascending:
                    valueComparerTest = (a, b) => (compareFunc(a, b) < 0);
                    break;
                case SortDirection.Descending:
                    valueComparerTest = (a, b) => (compareFunc(a, b) > 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction specified, can't craete value comparer func");
            }

            PerformSort(toSort, startIndex, endIndex, valueComparerTest);
        }

        /// <summary>
        /// 在列表中执行分区的排序，这个例程被递归调用。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSort">排序。</param>
        /// <param name="left">左索引。</param>
        /// <param name="right">正确的索引。</param>
        /// <param name="valueComparerTest">值比较器测试。</param>
        private static void PerformSort<T>(this IList<T> toSort, int left, int right, Func<T, T, bool> valueComparerTest)
        {
            while (true)
            {
                if (right <= left)
                {
                    return;
                }
                var pivotIndex = Partition(toSort, left, right, left, valueComparerTest);
                PerformSort(toSort, left, pivotIndex - 1, valueComparerTest);
                left = pivotIndex + 1;
            }
        }

        /// <summary>
        ///分区指定的列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSort">排序。</param>
        /// <param name="left">左边。</param>
        /// <param name="right">右边</param>
        /// <param name="pivotIndex">枢轴索引。</param>
        /// <param name="valueComparerTest">值比较器测试。</param>
        /// <returns>新枢纽点的索引</returns>
        private static int Partition<T>(this IList<T> toSort, int left, int right, int pivotIndex, Func<T, T, bool> valueComparerTest)
        {
            var pivotValue = toSort[pivotIndex];
            toSort.SwapValues(pivotIndex, right);
            var storeIndex = left;
            for (var i = left; i < right; i++)
            {
                if (!valueComparerTest(toSort[i], pivotValue))
                {
                    continue;
                }
                toSort.SwapValues(i, storeIndex);
                storeIndex++;
            }
            toSort.SwapValues(storeIndex, right);
            return storeIndex;
        }

        #endregion

        #region ShellSort 希尔排序算法

        /// <summary>
        /// 希尔排序算法
        /// 按指定的方向对指定的列表进行排序。
        /// </summary>
        /// <typeparam name="T">要排序的元素的类型</typeparam>
        /// <param name="toSort">要排序的列表</param>
        /// <param name="direction">排序方向</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="endIndex">结束开始索引</param>
        /// <param name="compareFunc">比较功能。</param>
        public static void ShellSort<T>(this IList<T> toSort, SortDirection direction, int startIndex, int endIndex, Comparison<T> compareFunc)
        {
            Func<T, T, bool> valueComparerTest;
            switch (direction)
            {
                case SortDirection.Ascending:
                    valueComparerTest = (a, b) => (compareFunc(a, b) > 0);
                    break;
                case SortDirection.Descending:
                    valueComparerTest = (a, b) => (compareFunc(a, b) < 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction specified, can't craete value comparer func");
            }

            int[] increments = { 1391376, 463792, 198768, 86961, 33936, 13776, 4592, 1968, 861, 336, 112, 48, 21, 7, 3, 1 };
            foreach (var t in increments)
            {
                for (int intervalIndex = t, i = startIndex + intervalIndex; i <= endIndex; i++)
                {
                    var currentValue = toSort[i];
                    var j = i;
                    while ((j >= intervalIndex) && valueComparerTest(toSort[j - intervalIndex], currentValue))
                    {
                        toSort[j] = toSort[j - intervalIndex];
                        j -= intervalIndex;
                    }
                    toSort[j] = currentValue;
                }
            }
        }

        #endregion

        #region SelectionSort 选择排序

        /// <summary>
        /// 选择排序
        /// 按指定的方向对指定的列表进行排序。
        /// </summary>
        /// <typeparam name="T">要排序的元素的类型</typeparam>
        /// <param name="toSort">要排序的列表。</param>
        /// <param name="direction">在侵权行为中排序元素的方向。</param>
        /// <param name="startIndex">开始索引。</param>
        /// <param name="endIndex">结束索引。</param>
        /// <param name="compareFunc">比较功能。</param>
        public static void SelectionSort<T>(this IList<T> toSort, SortDirection direction, int startIndex, int endIndex, Comparison<T> compareFunc)
        {
            Func<T, T, bool> valueComparerTest;
            switch (direction)
            {
                case SortDirection.Ascending:
                    valueComparerTest = (a, b) => (compareFunc(a, b) > 0);
                    break;
                case SortDirection.Descending:
                    valueComparerTest = (a, b) => (compareFunc(a, b) < 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), "指定的方向无效，无法创建值比较器函数");
            }

            for (var i = startIndex; i < endIndex; i++)
            {
                var indexValueToSwap = i;
                for (var j = i + 1; j <= endIndex; j++)
                {
                    if (valueComparerTest(toSort[indexValueToSwap], toSort[j]))
                    {
                        indexValueToSwap = j;
                    }
                }
                toSort.SwapValues(i, indexValueToSwap);
            }
        }

        #endregion

        /// <summary>
        /// 交换索引处的两个值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">IList</param>
        /// <param name="left">左边的值</param>
        /// <param name="right">右边的值</param>
        public static void SwapValues<T>(this IList<T> list, int left, int right)
        {
            var temp = list[left];
            list[left] = list[right];
            list[right] = temp;
        }

        /// <summary>
        /// 空集合转换为DataTable结构
        /// </summary>
        /// <param name="list">空集合</param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IList list)
        {
            var result = new DataTable();
            if (list.Count <= 0) return result;
            var propertys = list[0].GetType().GetProperties();
            foreach (var pi in propertys)
            {
                if (pi != null)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
            }
            foreach (var t in list)
            {
                var tempList = new ArrayList();
                foreach (var pi in propertys)
                {
                    var obj = pi.GetValue(t, null);
                    tempList.Add(obj);
                }
                var array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
            return result;
        }

        /// <summary>
        /// 非空集合转换为DataTable结构
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="list">非空集合</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            var ds = new DataSet();
            var dt = new DataTable(typeof(T).Name);
            var myPropertyInfo =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var t in list)
            {
                if (t == null) continue;
                var row = dt.NewRow();
                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    var pi = myPropertyInfo[i];
                    var name = pi.Name;
                    if (dt.Columns[name] != null) continue;
                    DataColumn column;
                    if (pi.PropertyType.UnderlyingSystemType.ToString() == "System.Nullable`1[System.Int32]")
                    {
                        column = new DataColumn(name, typeof(int));
                        dt.Columns.Add(column);
                        if (pi.GetValue(t, null) != null)
                            row[name] = pi.GetValue(t, null);
                        else
                            row[name] = DBNull.Value;
                    }
                    else
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                        row[name] = pi.GetValue(t, null);
                    }
                }
                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            return ds.Tables[0];
        }

        /// <summary>
        /// 表中有数据或无数据时使用,可排除DATASET不支持System.Nullable错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(this IList<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                var result = new DataTable();
                if (list == null || list.Count <= 0) return result;
                var propertys = list[0].GetType().GetProperties();
                foreach (var pi in propertys)
                {
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }
                foreach (var t in list)
                {
                    var tempList = new ArrayList();
                    foreach (var pi in propertys)
                    {
                        var obj = pi.GetValue(t, null);
                        tempList.Add(obj);
                    }
                    var array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
                return result;
            }
            var ds = new DataSet();
            var dt = new DataTable(typeof(T).Name);
            var myPropertyInfo =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var t in list)
            {
                if (t == null) continue;
                var row = dt.NewRow();
                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    var pi = myPropertyInfo[i];
                    var name = pi.Name;
                    if (dt.Columns[name] != null) continue;
                    DataColumn column;
                    if (pi.PropertyType.UnderlyingSystemType.ToString() == "System.Nullable`1[System.Int32]")
                    {
                        column = new DataColumn(name, typeof(int));
                        dt.Columns.Add(column);
                        if (pi.GetValue(t, null) != null)
                            row[name] = pi.GetValue(t, null);
                        else
                            row[name] = DBNull.Value;
                    }
                    else
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                        row[name] = pi.GetValue(t, null);
                    }
                }
                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            return ds.Tables[0];
        }

        /// <summary>
        /// 合并相同的DataTable
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DataTable MergeSameDatatable(this DataTable dt1, DataTable dt2)
        {
            var dt = dt1.Clone();
            var obj = new object[dt.Columns.Count];
            for (var i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i].ItemArray.CopyTo(obj, 0);
                dt.Rows.Add(obj);
            }
            for (var i = 0; i < dt2.Rows.Count; i++)
            {
                dt2.Rows[i].ItemArray.CopyTo(obj, 0);
                dt.Rows.Add(obj);
            }
            return dt;
        }

        /// <summary>
        /// 将两个列不同的DataTable合并成一个新的DataTable
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="dtName"></param>
        /// <returns></returns>
        public static DataTable UniteDataTable(this DataTable dt1, DataTable dt2, string dtName)
        {
            var dt3 = dt1.Clone();
            for (var i = 0; i < dt2.Columns.Count; i++)
            {
                dt3.Columns.Add(dt2.Columns[i].ColumnName);
            }
            var obj = new object[dt3.Columns.Count];

            for (var i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i].ItemArray.CopyTo(obj, 0);
                dt3.Rows.Add(obj);
            }

            if (dt1.Rows.Count >= dt2.Rows.Count)
            {
                for (var i = 0; i < dt2.Rows.Count; i++)
                {
                    for (var j = 0; j < dt2.Columns.Count; j++)
                    {
                        dt3.Rows[i][j + dt1.Columns.Count] = dt2.Rows[i][j].ToString();
                    }
                }
            }
            else
            {
                for (var i = 0; i < dt2.Rows.Count - dt1.Rows.Count; i++)
                {
                    var dr3 = dt3.NewRow();
                    dt3.Rows.Add(dr3);
                }
                for (var i = 0; i < dt2.Rows.Count; i++)
                {
                    for (var j = 0; j < dt2.Columns.Count; j++)
                    {
                        dt3.Rows[i][j + dt1.Columns.Count] = dt2.Rows[i][j].ToString();
                    }
                }
            }
            dt3.TableName = dtName;
            return dt3;
        }

        /// <summary>
        /// Datatable 转 List Dictionary<string object=""/>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> DataTableToListDictory(this DataTable dt)
        {
            var ld = new List<Dictionary<string, object>>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dic = new Dictionary<string, object>();
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    dic.Add(dt.Columns[j].ColumnName, dt.Rows[i][j]);
                }
                ld.Add(dic);
            }
            return ld;
        }
    }

    /// <summary>
    /// 排序方向
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// 升序
        /// </summary>
        Ascending,

        /// <summary>
        /// 降序
        /// </summary>
        Descending,
    }
}
