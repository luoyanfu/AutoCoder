using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using AutoCoder.model;
using DALFactory;
using PInterface;
using MessageBox = System.Windows.MessageBox;

namespace AutoCoder
{
    /// <summary>
    /// MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region properties

        /// <summary>
        /// full path
        /// </summary>
        private string _fullPath;

        #endregion properties

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel();
        }

        /// <summary>
        /// select file path
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileInfo = FileDialog(new OpenFileDialog());
                if (fileInfo == null)
                {
                    return;
                }
                FileNameTextBox.Text = fileInfo.Name;
                _fullPath = fileInfo.FullName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// file dialog setting
        /// </summary>
        private static FileInfo FileDialog(FileDialog fileDialog)
        {
            fileDialog.Filter = @"(*.xls)|*.xls|" + @"(*.xlsx)|*.xlsx";
            var result = fileDialog.ShowDialog();
            return result == System.Windows.Forms.DialogResult.OK ? new FileInfo(fileDialog.FileName) : null;
        }

        /// <summary>
        /// auto code event
        /// </summary>
        private void Button_Click_AutoCode(object sender, RoutedEventArgs e)
        {
            var mainWindow = this.DataContext as MainWindowModel;
            if (mainWindow == null || string.IsNullOrWhiteSpace(_fullPath))
            {
                return;
            }

            IBaseInterface iChoose = DLLAccess.CreateBaseInterface(mainWindow.AssemblyFile, mainWindow.ClassNamespace);
            iChoose.AutoCode(_fullPath);
        }
    }
}