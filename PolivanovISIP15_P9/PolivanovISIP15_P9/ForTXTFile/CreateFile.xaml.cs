using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;
using PolivanovISIP15_P9.ClassesTC;

namespace PolivanovISIP15_P9.ForTXTFile
{
    /// <summary>
    /// Логика взаимодействия для CreateFile.xaml
    /// </summary>
    public partial class CreateFile : Window
    {
        public CreateFile()
        {
            InitializeComponent();
            TBPath.Text = MainWindow.pathForCreate;
            
        }

        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BTNCreateFileOk_Click(object sender, RoutedEventArgs e)
        {
            string path = MainWindow.pathForCreate + FileNameForCreate.Text;
            FileInfo fileInfo = new FileInfo(path);
            MainWindow mainWindow = new MainWindow();

            if (!fileInfo.Exists)
            {
                fileInfo.Create();
                MessageBox.Show("You are creat new file: " + fileInfo.Name);
            }
            else
                MessageBox.Show("File exists.");
        }

        private void BTNCreateDirect_Click(object sender, RoutedEventArgs e)
        {
            string path = MainWindow.pathForCreate + FileNameForCreate.Text;
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                directory.Create();
                MessageBox.Show("You are create directory: " + directory.Name);
            }
            else
                MessageBox.Show("Directory exists.");
        }
    }
}
