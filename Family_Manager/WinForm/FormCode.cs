using Family_Manager.WinForm.FormClasses;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Family_Manager.WinForm
{
    internal class FormCode
    {
        private string directoryPath;
        
        public FormCode(string imagePath)
        {
            directoryPath = imagePath;
        }
        public void LoadImagesOnly(ListView listView)
        {
            // Check if the directory exists
            if (Directory.Exists(directoryPath))
            {
                // Clear existing items in the ListView
                listView.Items.Clear();

                // Set the size of the images in the ListView
                listView.LargeImageList = new ImageList();
                listView.LargeImageList.ImageSize = new Size(250, 250); // Adjust the dimensions as needed

                // Get all image files from the directory
                string[] imageFiles = Directory.GetFiles(directoryPath, "*.png"); // Adjust the file extension as needed

                // Iterate over each image file
                foreach (string imagePath in imageFiles)
                {
                    // Load the image
                    Image img = Image.FromFile(imagePath);

                    // Get the file name without extension
                    string fileName = Path.GetFileNameWithoutExtension(imagePath);

                    // Add the image to the ListView
                    listView.LargeImageList.Images.Add(fileName, img);

                    // Add the image file name to the ListView
                    ListViewItem item = new ListViewItem(fileName);
                    item.ImageKey = fileName;
                    listView.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Directory does not exist.");
            }
        }




    }
}
