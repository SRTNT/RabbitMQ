// Ignore Spelling: SRT

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GeneralDLL.SRTFile.ExcelDetails
{
    public class DataCell
    {
        #region HA
        public enum ExcelHorizontalAlignment
        {
            //
            // Summary:
            //     General aligned
            General,
            //
            // Summary:
            //     Left aligned
            Left,
            //
            // Summary:
            //     Center aligned
            Center,
            //
            // Summary:
            //     The horizontal alignment is centered across multiple cells
            CenterContinuous,
            //
            // Summary:
            //     Right aligned
            Right,
            //
            // Summary:
            //     The value of the cell should be filled across the entire width of the cell.
            Fill,
            //
            // Summary:
            //     Each word in each line of text inside the cell is evenly distributed across the
            //     width of the cell
            Distributed,
            //
            // Summary:
            //     The horizontal alignment is justified to the Left and Right for each row.
            Justify
        }
        #endregion

        #region VA
        public enum ExcelVerticalAlignment
        {
            //
            // Summary:
            //     Top aligned
            Top,
            //
            // Summary:
            //     Center aligned
            Center,
            //
            // Summary:
            //     Bottom aligned
            Bottom,
            //
            // Summary:
            //     Distributed. Each line of text inside the cell is evenly distributed across the
            //     height of the cell
            Distributed,
            //
            // Summary:
            //     Justify. Each line of text inside the cell is evenly distributed across the height
            //     of the cell
            Justify
        }
        #endregion

        public object value { get; set; }
        public Color? BackGround { get; set; } = null;
        public Color? ForeGround { get; set; } = null;
        public Color? GridColor { get; set; } = null;
        public ExcelHorizontalAlignment HorizontalAlignment { get; set; } = ExcelHorizontalAlignment.Left;
        public ExcelVerticalAlignment VerticalAlignment { get; set; } = ExcelVerticalAlignment.Center;
    }
}
