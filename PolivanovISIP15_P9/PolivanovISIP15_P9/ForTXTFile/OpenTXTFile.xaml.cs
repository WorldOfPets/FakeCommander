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
using System.Windows.Shapes;

namespace PolivanovISIP15_P9.ForTXTFile
{
    /// <summary>
    /// Логика взаимодействия для OpenTXTFile.xaml
    /// </summary>
    public partial class OpenTXTFile : Window
    {
        public OpenTXTFile()
        {
            InitializeComponent();
        }

        private void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(MainWindow.pathForCreate, false, Encoding.Default))
            {
                sw.WriteLine(TBtxt.Text);
            }
            this.Close();
        }
    }
}
