// Ignore Spelling: SRT

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GeneralDLL.SRTFile.ExcelDetails
{
    public class CellEffect
    {
        public Color BackColor { get; set; } = Color.White;
        public CellFontStyle FontStyle { get; set; } = new CellFontStyle();
        public CellBorderStyle Border { get; set; } = new CellBorderStyle();

        internal void Set(OfficeOpenXml.Style.ExcelStyle style)
        {
            style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

            style.Fill.BackgroundColor.SetColor(BackColor);

            style.Font.Color.SetColor(FontStyle.ForeColor);
            style.Font.Bold = FontStyle.Bold;

            style.Border.Top.Style = Border.Top.Style;
            style.Border.Bottom.Style = Border.Bottom.Style;
            style.Border.Right.Style = Border.Right.Style;
            style.Border.Left.Style = Border.Left.Style;

            style.Border.Top.Color.SetColor(Border.Top.Color);
            style.Border.Bottom.Color.SetColor(Border.Bottom.Color);
            style.Border.Right.Color.SetColor(Border.Right.Color);
            style.Border.Left.Color.SetColor(Border.Left.Color);
        }
    }
}
