using PST.Reader.BL;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace PST.Reader.WPF.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Microsoft.Win32.OpenFileDialog openFileDlg; // Create OpenFileDialog
        private string _outputFolderPath = $@"C:\Users\{Environment.UserName}\Desktop\";

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
                var filePath = FileNameTextBox.Text;
                if (!File.Exists(filePath)) throw new FileNotFoundException();

                TextBlock1.Text = string.Empty;                
                string inputFileName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                string outputFileName = string.Empty;

                var pstService = new PstReadingService(filePath);

                if (FolderStructure.IsChecked != true && FolderItems.IsChecked != true)
                {
                    TextBlock1.Text = "Select the type of information you want to extract";
                }
                else if (FolderStructure.IsChecked == true)
                {
                    outputFileName = $"{inputFileName} - FolderStructure.txt";
                    pstService.GetFoldersStructure(_outputFolderPath + outputFileName);
                    PrintFileLocation(_outputFolderPath, outputFileName);
                }
                else if (FolderItems.IsChecked == true)
                {
                    outputFileName = $"{inputFileName} - FolderItems.txt";
                    pstService.GetFolderItems(_outputFolderPath + outputFileName);
                    PrintFileLocation(_outputFolderPath, outputFileName);
                }

            }
            catch (FileNotFoundException)
            {
                TextBlock1.Text = "File not found. Please try again!";
            }
        }

        private void PrintFileLocation(string outputFolderPath, string outputFileName)
        {
            TextBlock1.Text = $"Results output directory: {_outputFolderPath}\nFile Name: {outputFileName}";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OutputFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                _outputFolderPath = dialog.SelectedPath + @"\";
            }

        }
    }
}

