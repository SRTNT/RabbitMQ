// Ignore Spelling: SRT

using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDLL.SRTFile.ExcelDetails
{
    public class DataRow
    {
        public int RowNumber { get; set; }
        public List<DataCell> value { get; set; }

        internal int SetData(OfficeOpenXml.ExcelWorksheet sheet, int maxCol)
        {
            int col = 1;
            foreach (var item in value)
            {
                using (OfficeOpenXml.ExcelRange rng = sheet.Cells[RowNumber, col])
                {
                    rng.Value = item.value;
                    rng.Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)(int)item.HorizontalAlignment;
                    rng.Style.VerticalAlignment = (OfficeOpenXml.Style.ExcelVerticalAlignment)(int)item.VerticalAlignment;

                    if (item.ForeGround != null)
                        rng.Style.Font.Color.SetColor(item.ForeGround.Value);
                    if (item.BackGround != null)
                    {
                        rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(item.BackGround.Value);
                    }
                    //if (item.GridColor != null)
                    //{
                    //    rng.Style.Border.Top.Color.SetColor(item.GridColor.Value);
                    //    rng.Style.Border.Bottom.Color.SetColor(item.GridColor.Value);
                    //    rng.Style.Border.Left.Color.SetColor(item.GridColor.Value);
                    //    rng.Style.Border.Right.Color.SetColor(item.GridColor.Value);
                    //}

                    rng.AutoFitColumns();
                }
                maxCol = Math.Max(maxCol, col);
                col++;
            }//end column

            return maxCol;

        }
    }
}
