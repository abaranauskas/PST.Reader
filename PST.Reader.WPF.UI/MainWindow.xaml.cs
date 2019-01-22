using PST.Reader.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PST.Reader.WPF.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Microsoft.Win32.OpenFileDialog openFileDlg; // Create OpenFileDialog

        public MainWindow()
        {
            InitializeComponent();
            openFileDlg = new Microsoft.Win32.OpenFileDialog();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                FileNameTextBox.Text = openFileDlg.FileName;
            }
        }

        private void ExatractInfo_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                openFileDlg.FileName = @"C:\Users\aidas\OneDrive\Documents\Outlook Files\asdf@asdf.com.pst";
                //openFileDlg.FileName = FileNameTextBox.Text;
                TextBlock1.Text = string.Empty;

                var pstService = new PstReadingService(openFileDlg.FileName);
                var folders = pstService.GetFolders();
                /*var folderItems = */pstService.GetFolderDetails();
                //ReadPstAsync();            

                
             
               
                //if (!string.IsNullOrWhiteSpace(openFileDlg.FileName)) TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
            catch (FileNotFoundException)
            {
                TextBlock1.Text = "File not found. Please try again!";
            }
            catch (DirectoryNotFoundException)
            {
                TextBlock1.Text = "Directory not found. Please try again!";
            }
            catch (ArgumentException)
            {
                TextBlock1.Text = "Directory not found. Please try again!";
            }
            finally
            {
                FileNameTextBox.Text = "Please enter the pst file path or click 'Browse a file' button";
            }
        }

        //public async Task ReadPstAsync(string filePath)
        //{
        //    await Task.Run(() =>
        //    {
        //        var pstService = new PstReadingService(filePath);
        //        pstService.GetAccountDetails();
        //        pstService.GetFolders();

        //    });
        //}
    }
}

