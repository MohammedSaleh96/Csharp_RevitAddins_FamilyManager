using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Family_Manager.WinForm.FormClasses
{
    public class DataFromExcell
    {
        // Properties
        public static List<string> keys { get; set; }= new List<string>();
        public static List<string> famNames { get; set; }= new List<string>();
        // Methods
        public static void ReadExcelData()
        {
            string path = @"D:\MS_AddinsFolder\MÜPRO Family Manager\Family_Manager\bin\Debug\Resources\Data.xlsx";
            FileInfo file = new FileInfo(path);
            using (ExcelPackage package = new ExcelPackage(path))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets["Sheet1"];
                //sheet.Cells["A4"].Value = "Hello00ooooooooooo World!";
                int rowCount = sheet.Dimension.Rows;
                List<string> li = new List<string>();
                ////Loop through rows and add cell values to the keys list
                for (int i = 2; i <= rowCount+6; i++) // Adjusted loop bounds
                {
                    var cellValue = sheet.Cells[i, 2].Value;
                    if (cellValue != null) // Check if the cell value is not null
                    {
                        keys.Add(cellValue.ToString());
                    }
                    var cellValueFam = sheet.Cells[i, 3].Value;
                    if (cellValueFam != null) // Check if the cell value is not null
                    {
                        famNames.Add(cellValueFam.ToString());
                    }
                }
                
            }
        }



    }
}
