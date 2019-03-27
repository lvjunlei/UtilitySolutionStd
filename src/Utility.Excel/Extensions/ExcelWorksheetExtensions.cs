#region ExcelWorksheetExtensions 文件信息
/***********************************************************
**文 件 名：ExcelWorksheetExtensions 
**命名空间：Utility.Excel.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-02-28 10:20:35 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using OfficeOpenXml;

namespace Utility.Excel.Extensions
{
    /// <summary>
    /// ExcelWorksheetExtensions
    /// </summary>
    public static class ExcelWorksheetExtensions
    {
        /// <summary>
        /// 添加/创建Worksheet
        /// </summary>
        /// <param name="package">要创建worksheet的excel</param>
        /// <param name="sheetName">要创建的worksheet名称</param>
        /// <returns></returns>
        public static ExcelWorksheet CreateSheet(this ExcelPackage package, string sheetName)
        {
            return package.Workbook.Worksheets.Add(sheetName);
        }

        /// <summary>
        /// 添加表头
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <param name="headerTexts"></param>
        public static void AddHeader(this ExcelWorksheet sheet, int rowIndex, params string[] headerTexts)
        {
            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, rowIndex, i + 1, headerTexts[i]);
            }
        }


        /// <summary>
        /// 添加表头
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="headerText"></param>
        public static void AddHeader(this ExcelWorksheet sheet, int rowIndex, int columnIndex, string headerText)
        {
            sheet.Cells[rowIndex, columnIndex].Value = headerText;
            sheet.Cells[rowIndex, columnIndex].Style.Font.Bold = true;
        }
    }
}
