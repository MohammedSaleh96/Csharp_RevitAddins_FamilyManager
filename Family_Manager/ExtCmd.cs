using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Family_Manager.WinForm;
using OfficeOpenXml;
using System.IO;
using System.Windows.Forms;

namespace Family_Manager
{

    [Transaction(TransactionMode.Manual)]
    internal class ExtCmd : IExternalCommand
    {
        public static UIDocument uidoc { get; set; }
        public static Document doc { get; set; }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;
            ExtCmd.doc = doc;

            Form1 form = new Form1(doc);
            form.Show();

            return Result.Succeeded;
        }
    }
}
