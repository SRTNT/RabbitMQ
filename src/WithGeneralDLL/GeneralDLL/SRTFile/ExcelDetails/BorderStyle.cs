// Ignore Spelling: SRT

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GeneralDLL.SRTFile.ExcelDetails
{
    public class BorderStyle
    {
        public OfficeOpenXml.Style.ExcelBorderStyle Style { get; set; } = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        public Color Color { get; set; } = Color.DarkGreen;
    }
}
