// Ignore Spelling: SRT

using GeneralDLL.SRTFile.ExcelDetails;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace GeneralDLL.SRTFile
{
    public class ExcelFile
    {
        #region Constructors
        public ExcelFile(string _Path, string _NameFile)
        {
            if (_Path == "")
                FilePath = Path.GetTempFileName();
            else
            {
                if (!Directory.Exists(_Path))
                    Directory.CreateDirectory(_Path);
                FilePath = _Path;
            }

            FileName = _NameFile;
        }
        public ExcelFile(string _PathAndFile)
        {
            FileFullPath = _PathAndFile;
        }
        #endregion

        #region Main Path
        string myPath = "";
        public string FilePath
        {
            get { return myPath; }
            set { myPath = fixFolderPath(value); }
        }
        private string fixFolderPath(string path)
        {
            path = path.Replace(@"\\", @"\");
            if (path.Substring(path.Length - 1) == @"\")
                return path;
            return path + @"\";
        }
        #endregion

        #region name File
        string nameFile = "";
        public string FileName
        {
            get { return nameFile; }

            set { nameFile = File_name_check(value); }
        }
        private string File_name_check(string _fn)
        {
            if (_fn.IndexOf(".xlsx") > 0)
                return _fn;
            else
                return _fn + ".xlsx";
        }
        #endregion

        #region Full Address
        public string FileFullPath
        {
            get => Path.Combine(FilePath, FileName);
            private set
            {
                var _Path = value.Substring(0, value.LastIndexOf('\\'));
                var _NameFile = value.Replace(_Path, "").Replace("\\", "");

                if (value == "")
                    FilePath = Path.GetTempFileName();
                else
                {
                    if (!Directory.Exists(_Path))
                        Directory.CreateDirectory(_Path);
                    FilePath = _Path;
                }

                FileName = _NameFile;
            }
        }
        #endregion

        #region Existed
        public bool FileExist => Exist();
        public bool Exist()
        {
            try
            {
                if (File.Exists(FileFullPath))
                    return true;
                return false;
            }
            catch { return false; }
        }
        #endregion

        #region FileDate
        public DateTime FileDate => File.GetCreationTime(FileFullPath);
        #endregion

        #region Delete
        public bool Delete()
        {
            try
            {
                File.Delete(FilePath + FileName);
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region Read Data
        /// <summary>
        /// 
        /// </summary>
        /// <param name="worksheet">start in 0</param>
        /// <returns></returns>
        public List<List<string>> Excel_GetDataFromFile(int worksheet)
        {
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;

            var lst = new List<List<string>>();
            var package = new OfficeOpenXml.ExcelPackage(new FileInfo(FileFullPath));
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
            OfficeOpenXml.ExcelWorksheet sheet = package.Workbook.Worksheets[worksheet];

            for (int i = 1; i <= sheet.Dimension.End.Row; i++)
            {
                var lst_det = new List<string>();
                for (int j = sheet.Dimension.Start.Column; j <= sheet.Dimension.End.Column; j++)
                {
                    //if (sheet.Cells[i, j].Value != null)
                    lst_det.Add(sheet.Cells[i, j].Value?.ToString() ?? "");
                    //gm.cnst_first_nm = ws.Cells[i, j].Value.ToString();
                }
                lst.Add(lst_det);
            }
            return lst;
        }
        #endregion

        #region Write Data
        public void Excel_WriteDataToFile(List<List<string>> lstData, bool hasHeader = true, string worksheet = "srtSheet1")
        {
            CellEffect header = new CellEffect();

            header.BackColor = Color.Green;
            header.FontStyle.ForeColor = Color.White;
            header.FontStyle.Bold = true;

            header.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            header.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            header.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            header.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;

            CellEffect Body = new CellEffect();

            Excel_WriteDataToFile(lstData, hasHeader, header, Body, worksheet);
        }
        public void Excel_WriteDataToFile(List<List<string>> lstData, bool hasHeader, CellEffect HeaderStyle, CellEffect BodyStyle, string worksheet = "srtSheet1")
        {
            try
            {
                var path = FileFullPath;
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;

                var package = new OfficeOpenXml.ExcelPackage();
                OfficeOpenXml.ExcelWorksheet sheet = package.Workbook.Worksheets.Add(worksheet);

                int row = 1, col = 1, maxCol = 1;

                foreach (var item_row in lstData)
                {
                    col = 1;
                    foreach (var item in item_row)
                    {
                        using (OfficeOpenXml.ExcelRange rng = sheet.Cells[row, col])
                        {
                            rng.Value = item;
                            rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            rng.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            rng.AutoFitColumns();
                        }
                        maxCol = Math.Max(maxCol, col);
                        col++;
                    }//end column

                    row++;
                }//end row

                if (hasHeader)
                {
                    using (OfficeOpenXml.ExcelRange rng = sheet.Cells[1, 1, 1, maxCol])
                    { HeaderStyle.Set(rng.Style); }
                    using (OfficeOpenXml.ExcelRange rng = sheet.Cells[2, 1, row - 1, maxCol])
                    { BodyStyle.Set(rng.Style); }
                }
                else
                {
                    using (OfficeOpenXml.ExcelRange rng = sheet.Cells[1, 1, row - 1, maxCol])
                    { HeaderStyle.Set(rng.Style); }
                }

                sheet.Protection.IsProtected = false;
                sheet.Protection.AllowSelectLockedCells = false;

                package.SaveAs(new FileInfo(path));
            }
            catch (Exception exp)
            { throw new Exception("Write Data To File Excel", exp); }
        }

        public void Excel_WriteDataToFile(List<DataRow> lstData, string worksheet = "srtSheet1")
        {
            try
            {
                var path = FileFullPath;
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;

                var package = new OfficeOpenXml.ExcelPackage();
                OfficeOpenXml.ExcelWorksheet sheet = package.Workbook.Worksheets.Add(worksheet);

                int maxCol = 1;

                foreach (var item_row in lstData)
                {
                    maxCol = item_row.SetData(sheet, maxCol);
                }//end row

                sheet.Protection.IsProtected = false;
                sheet.Protection.AllowSelectLockedCells = false;

                package.SaveAs(new FileInfo(path));
            }
            catch (Exception exp)
            { throw new Exception("Write Data To File Excel", exp); }
        } 
        #endregion
    }
}
