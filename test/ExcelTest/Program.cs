using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ExcelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelTest();

            Console.ReadLine();
        }

        static void ExcelTest()
        {
            var fileName = $"{DateTime.Now.ToFileTime()}.xlsx";

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                var sheet = package.Workbook.Worksheets.Add("房屋套数查询明细");
                //sheet.Cells.Style.ShrinkToFit = true;
                sheet.Column(1).Width = 20d;
                sheet.Column(2).Width = 20d;
                sheet.Column(3).Width = 20d;
                sheet.Column(4).Width = 20d;
                sheet.Column(5).Width = 20d;
                sheet.Column(6).Width = 20d;
                sheet.Column(7).Width = 20d;

                sheet.Cells[1, 1].Value = "不动产查询明细";
                sheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[1, 1].Style.Font.Size = 16f;
                sheet.Cells[1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                sheet.Cells[1, 1, 1, 7].Merge = true;

                sheet.Cells[2, 1].Value = "序号";
                sheet.Cells[2, 1].Style.Font.Bold = true;
                sheet.Cells[2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[2, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[2, 1].Style.Font.Size = 12f;
                sheet.Cells[2, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                sheet.Cells[2, 2].Value = "CBD房号";
                sheet.Cells[2, 2].Style.Font.Bold = true;
                sheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[2, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[2, 2].Style.Font.Size = 12f;
                sheet.Cells[2, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                sheet.Cells[2, 3].Value = "CBD购房人姓名";
                sheet.Cells[2, 3].Style.Font.Bold = true;
                sheet.Cells[2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[2, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[2, 3].Style.Font.Size = 12f;
                sheet.Cells[2, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                sheet.Cells[2, 4].Value = "家庭成员";
                sheet.Cells[2, 4].Style.Font.Bold = true;
                sheet.Cells[2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[2, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[2, 4].Style.Font.Size = 12f;
                sheet.Cells[2, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                sheet.Cells[2, 5].Value = "姓名";
                sheet.Cells[2, 5].Style.Font.Bold = true;
                sheet.Cells[2, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[2, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[2, 5].Style.Font.Size = 12f;
                sheet.Cells[2, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                sheet.Cells[2, 6].Value = "曾用名（若有）";
                sheet.Cells[2, 6].Style.Font.Bold = true;
                sheet.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[2, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[2, 6].Style.Font.Size = 12f;
                sheet.Cells[2, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                sheet.Cells[2, 7].Value = "身份证号";
                sheet.Cells[2, 7].Style.Font.Bold = true;
                sheet.Cells[2, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[2, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[2, 7].Style.Font.Size = 12f;
                sheet.Cells[2, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin,Color.Brown);

                sheet.Cells[3, 1].Value = "1";
                sheet.Cells[3, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[3, 1].Style.Font.Size = 12f;
                sheet.Cells[3, 1, 7, 1].Merge = true;

                sheet.Cells[3, 2].Value = "3-2-1402";
                sheet.Cells[3, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[3, 2].Style.Font.Size = 12f;
                sheet.Cells[3, 2, 7, 2].Merge = true;

                sheet.Cells[3, 3].Value = "姚明";
                sheet.Cells[3, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[3, 3].Style.Font.Size = 12f;
                sheet.Cells[3, 3, 7, 3].Merge = true;

                sheet.Cells[3, 4].Value = "CBD购房人";
                sheet.Cells[3, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[3, 4].Style.Font.Size = 12f;

                sheet.Cells[4, 4].Value = "配偶";
                sheet.Cells[4, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[4, 4].Style.Font.Size = 12f;

                sheet.Cells[5, 4].Value = "子/女1";
                sheet.Cells[5, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[5, 4].Style.Font.Size = 12f;

                sheet.Cells[3, 5].Value = "姚明";
                sheet.Cells[3, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[3, 5].Style.Font.Size = 12f;

                sheet.Cells[4, 5].Value = "林志玲";
                sheet.Cells[4, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[4, 5].Style.Font.Size = 12f;

                sheet.Cells[5, 5].Value = "姚玲";
                sheet.Cells[5, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[5, 5].Style.Font.Size = 12f;

                sheet.Cells[3, 7].Value = "372930198701087356";
                sheet.Cells[3, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[3, 7].Style.Font.Size = 12f;

                sheet.Cells[4, 7].Value = "372930198701087356";
                sheet.Cells[4, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[4, 7].Style.Font.Size = 12f;

                sheet.Cells[5, 7].Value = "372930198701087356";
                sheet.Cells[5, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells[5, 7].Style.Font.Size = 12f;

                package.Workbook.Properties.Author = "Lvjunlei";
                package.Save();
            }
        }


        /// <summary>
        /// 添加表头
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="headerTexts"></param>
        public static void AddHeader(ExcelWorksheet sheet, params string[] headerTexts)
        {
            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, i + 1, headerTexts[i]);
            }
        }

        /// <summary>
        /// 添加表头
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="columnIndex"></param>
        /// <param name="headerText"></param>
        public static void AddHeader(ExcelWorksheet sheet, int columnIndex, string headerText)
        {
            sheet.Cells[1, columnIndex].Value = headerText;
            sheet.Cells[1, columnIndex].Style.Font.Bold = true;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="items"></param>
        /// <param name="propertySelectors"></param>
        public static void AddObjects(ExcelWorksheet sheet, int startRowIndex, IList<Student> items, Func<Student, object>[] propertySelectors)
        {
            for (var i = 0; i < items.Count; i++)
            {
                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    sheet.Cells[i + startRowIndex, j + 1].Value = propertySelectors[j](items[i]);
                }
            }
        }
    }
    public class ExcelExportDto<T>
    {
        public ExcelExportDto(string columnName, Func<T, object> columnValue)
        {
            ColumnName = columnName;
            ColumnValue = columnValue;
        }
        public string ColumnName { get; set; }

        public Func<T, object> ColumnValue { get; set; }
    }
    public class Student
    {
        public String Name { get; set; }

        public String Code { get; set; }
    }
}
