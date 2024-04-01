using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_Manager.WinForm
{
    internal class ExtEvent1 : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {

            Document doc = app.ActiveUIDocument.Document;
            using (Transaction trn = new Transaction(doc, "Load Family"))
            {
                try
                {
                    trn.Start();
                    // Construct the file path based on the directory path and family name
                    string filePath = Path.Combine(Form1.directoryPath, Form1.familyName + ".rfa"); // Corrected concatenation
                                                                                                    // Load the family in Revit
                    doc.LoadFamily(filePath);
                    trn.Commit();
                }
                catch (ArgumentException ex)
                {
                    // Handle ArgumentException (e.g., invalid file path or family name)
                    TaskDialog.Show("Error", "Invalid file path or family name.");
                }
                catch (FileNotFoundException ex)
                {
                    // Handle FileNotFoundException (e.g., family file not found)
                    TaskDialog.Show("Error", "Family file not found.");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    TaskDialog.Show("Error", "An error occurred: " + ex.Message);
                }

            }
        }

        public string GetName()
        {
            return "MS";
        }
    }
}
