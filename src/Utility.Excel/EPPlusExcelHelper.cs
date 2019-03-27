#region EPPlusExcelHelper 文件信息
/***********************************************************
**文 件 名：EPPlusExcelHelper 
**命名空间：Utility.Excel 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-01 10:33:16 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

namespace Utility.Excel
{
    public class EpPlusExcelHelper
    {
        public ExcelPackage ExcelPackage { get; }
        private readonly Stream _fs;

        public EpPlusExcelHelper(string filePath)
        {
            if (File.Exists(filePath))
            {
                var file = new FileInfo(filePath);
                ExcelPackage = new ExcelPackage(file);
            }
            else
            {
                _fs = File.Create(filePath);
                ExcelPackage = new ExcelPackage(_fs);

            }
        }
        /// <summary>
        /// 获取sheet，没有时创建
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public ExcelWorksheet GetOrAddSheet(string sheetName)
        {
            ExcelWorksheet ws = ExcelPackage.Workbook.Worksheets.FirstOrDefault(i => i.Name == sheetName);
            if (ws == null)
            {
                ws = ExcelPackage.Workbook.Worksheets.Add(sheetName);
            }
            return ws;
        }

        /// <summary>
        /// 使用EPPlus导出Excel(xlsx)
        /// </summary>
        /// <param name="sourceTable">数据源</param>
        public void AppendSheetToWorkBook(DataTable sourceTable)
        {
            AppendSheetToWorkBook(sourceTable, true);
        }

        /// <summary>
        /// 使用EPPlus导出Excel(xlsx)
        /// </summary>
        /// <param name="sourceTable">数据源</param>
        /// <param name="isDeleteSameNameSheet">是否删除同名的sheet</param>
        public void AppendSheetToWorkBook(DataTable sourceTable, bool isDeleteSameNameSheet)
        {
            //Create the worksheet

            ExcelWorksheet ws = AddSheet(sourceTable.TableName, isDeleteSameNameSheet);

            //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells["A1"].LoadFromDataTable(sourceTable, true);

            //Format the row
            FromatRow(sourceTable.Rows.Count, sourceTable.Columns.Count, ws);

        }

        /// <summary>
        /// 删除指定的sheet
        /// </summary>
        /// <param name="ExcelPackage"></param>
        /// <param name="sheetName"></param>
        public void DeleteSheet(string sheetName)
        {
            var sheet = ExcelPackage.Workbook.Worksheets.FirstOrDefault(i => i.Name == sheetName);
            if (sheet != null)
            {
                ExcelPackage.Workbook.Worksheets.Delete(sheet);
            }
        }
        /// <summary>
        /// 导出列表到excel，已存在同名sheet将删除已存在的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ExcelPackage"></param>
        /// <param name="list">数据源</param>
        /// <param name="sheetName">sheet名称</param>
        public void AppendSheetToWorkBook<T>(IEnumerable<T> list, string sheetName)
        {
            AppendSheetToWorkBook(list, sheetName, true);
        }
        /// <summary>
        /// 导出列表到excel，已存在同名sheet将删除已存在的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ExcelPackage"></param>
        /// <param name="list">数据源</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="isDeleteSameNameSheet">是否删除已存在的同名sheet，false时将重命名导出的sheet</param>
        public void AppendSheetToWorkBook<T>(IEnumerable<T> list, string sheetName, bool isDeleteSameNameSheet)
        {
            ExcelWorksheet ws = AddSheet(sheetName, isDeleteSameNameSheet);

            //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells["A1"].LoadFromCollection(list, true);

        }

        /// <summary>
        /// 添加文字图片
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="msg">要转换成图片的文字</param>
        public void AddPicture(string sheetName, string msg)
        {
            Bitmap img = GetPictureString(msg);

            var sheet = GetOrAddSheet(sheetName);
            var picName = "92FF5CFE-2C1D-4A6B-92C6-661BDB9ED016";
            var pic = sheet.Drawings.FirstOrDefault(i => i.Name == picName);
            if (pic != null)
            {
                sheet.Drawings.Remove(pic);
            }
            pic = sheet.Drawings.AddPicture(picName, img);

            pic.SetPosition(3, 0, 6, 0);
        }
        /// <summary>
        /// 文字绘制图片
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static Bitmap GetPictureString(string msg)
        {
            var msgs = msg.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var maxLenght = msgs.Max(i => i.Length);
            var rowCount = msgs.Count();
            var rowHeight = 23;
            var fontWidth = 17;
            var img = new Bitmap(maxLenght * fontWidth, rowCount * rowHeight);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(Color.White);
                Font font = new Font("Arial", 12, (FontStyle.Bold));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Blue, Color.DarkRed, 1.2f, true);

                for (int i = 0; i < msgs.Count(); i++)
                {
                    g.DrawString(msgs[i], font, brush, 3, 2 + rowHeight * i);
                }
            }

            return img;
        }

        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ListToDataTable<T>(IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="values">行类容，一个单元格一个对象</param>
        /// <param name="rowIndex">插入位置，起始位置为1</param>
        public void InsertValues(string sheetName, List<object> values, int rowIndex)
        {
            var sheet = GetOrAddSheet(sheetName);
            sheet.InsertRow(rowIndex, 1);
            int i = 1;
            foreach (var item in values)
            {
                sheet.SetValue(rowIndex, i, item);
                i++;
            }
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        public void Save()
        {
            ExcelPackage.Save();
        }

        /// <summary>
        /// 添加Sheet到ExcelPackage
        /// </summary>
        /// <param name="ExcelPackage">ExcelPackage</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="isDeleteSameNameSheet">如果存在同名的sheet是否删除</param>
        /// <returns></returns>
        private ExcelWorksheet AddSheet(string sheetName, bool isDeleteSameNameSheet)
        {
            if (isDeleteSameNameSheet)
            {
                DeleteSheet(sheetName);
            }
            else
            {
                while (ExcelPackage.Workbook.Worksheets.Any(i => i.Name == sheetName))
                {
                    sheetName = sheetName + "(1)";
                }
            }

            ExcelWorksheet ws = ExcelPackage.Workbook.Worksheets.Add(sheetName);
            return ws;
        }

        private void FromatRow(int rowCount, int colCount, ExcelWorksheet ws)
        {
            ExcelBorderStyle borderStyle = ExcelBorderStyle.Thin;
            Color borderColor = Color.FromArgb(155, 155, 155);

            using (ExcelRange rng = ws.Cells[1, 1, rowCount + 1, colCount])
            {
                rng.Style.Font.Name = "宋体";
                rng.Style.Font.Size = 10;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));

                rng.Style.Border.Top.Style = borderStyle;
                rng.Style.Border.Top.Color.SetColor(borderColor);

                rng.Style.Border.Bottom.Style = borderStyle;
                rng.Style.Border.Bottom.Color.SetColor(borderColor);

                rng.Style.Border.Right.Style = borderStyle;
                rng.Style.Border.Right.Color.SetColor(borderColor);
            }

            //Format the header row
            using (ExcelRange rng = ws.Cells[1, 1, 1, colCount])
            {
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(234, 241, 246));  //Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.FromArgb(51, 51, 51));
            }
        }

        public void Dispose()
        {
            ExcelPackage.Dispose();
            if (_fs != null)
            {
                _fs.Dispose();
                _fs.Close();
            }

        }
    }
}
