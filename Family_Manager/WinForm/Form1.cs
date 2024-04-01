using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using Family_Manager.WinForm.FormClasses;

namespace Family_Manager.WinForm
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        ExternalEvent externalEvent = ExternalEvent.Create(new ExtEvent1());
        public static string familyName { get; set; }
        // Private
        private Dictionary<string, string> keyToFamilyNameMap; // Declare as a class member
        public static string directoryPath = @"D:\MS_AddinsFolder\MÜPRO Family Manager\Family_Manager\bin\Debug\Resources";
        private FormCode formCode { get; set; }
        private List<ListViewItem> originalItems; // Declare originalItems as a class member

        public Form1(Document doc)
        {
            InitializeComponent();
            formCode = new FormCode(directoryPath);
            formCode.LoadImagesOnly(listView1);
            DataFromExcell.ReadExcelData();

            // Ensure that ReadExcelData.famNames and ReadExcelData.keys have the same count
            if (DataFromExcell.famNames.Count == DataFromExcell.keys.Count)
            {
                // Build a dictionary to map each key to its corresponding family name
                keyToFamilyNameMap = new Dictionary<string, string>();
                for (int i = 0; i < DataFromExcell.famNames.Count; i++)
                {
                    keyToFamilyNameMap[DataFromExcell.keys[i]] = DataFromExcell.famNames[i];
                }

                // Save original items when the form loads
                originalItems = listView1.Items.Cast<ListViewItem>().ToList();
            }
            else
            {
                MessageBox.Show("The count of family names and keys does not match.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (keyToFamilyNameMap == null)
                return; // Exit if keyToFamilyNameMap is not initialized

            string filterText = textBox1.Text.Trim().ToLower(); // Convert text to lowercase for case-insensitive comparison

            // Clear existing items from listView1
            listView1.Items.Clear();

            if (string.IsNullOrEmpty(filterText))
            {
                // If filter text is empty, display all original items
                listView1.Items.AddRange(originalItems.ToArray());
            }
            else
            {
                // Filter items based on filter text
                var filteredItems = originalItems.Where(item =>
                    item.Text.ToLower().Contains(filterText) ||
                    keyToFamilyNameMap.Any(kv => kv.Key.StartsWith(filterText) && kv.Value.ToLower() == item.Text.ToLower()))
                    .ToList();
                // Add filtered items to listView1
                listView1.Items.AddRange(filteredItems.ToArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Your button1 click event code here
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if an item is selected in the ListView
            if (listView1.SelectedItems.Count > 0)
            {
                // Get the selected item
                ListViewItem selectedItem = listView1.SelectedItems[0];
                // Get the family name from the selected item's text
                familyName = selectedItem.Text;
                // Create an instance of ExtEvent1 with the family name
                // Raise the external event
                externalEvent.Raise();
            }
            else
            {
                MessageBox.Show("Please select an item from the list.");
            }

        }
    }
}
