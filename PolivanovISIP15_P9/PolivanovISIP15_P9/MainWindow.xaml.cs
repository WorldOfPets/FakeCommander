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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using PolivanovISIP15_P9.ClassesTC;
using PolivanovISIP15_P9.ForTXTFile;

namespace PolivanovISIP15_P9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                CMB.Items.Add(drive.Name);
                CMB2.Items.Add(drive.Name);
            }

        }
        public string location { get; set; }
        public string ForSavePath { get; set; }
        public string ForSavePath1 { get; set; }
        public string ForSavePath2 { get; set; }
        public void ListMain_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (location == "1")
            {
                ForSavePath += Convert.ToString(ListMain.SelectedItem) + "\\";
                if (System.IO.Directory.Exists(ForSavePath))
                {
                    ListMain.ItemsSource = CorrectFile.methodForList(TBadres.Text, ListMain.SelectedItem);
                    TBadres.Text = ForSavePath;
                }
            }
            else if (location == "2")
            {
                ForSavePath2 += Convert.ToString(ListForCopy.SelectedItem) + "\\";
                if (System.IO.Directory.Exists(ForSavePath2))
                {
                    ListForCopy.ItemsSource = CorrectFile.methodForList(TBadres2.Text, ListForCopy.SelectedItem);
                    TBadres2.Text = ForSavePath2;
                }
            }
        }

        private void CMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            if (location == "1")
            {
                TBadres.Text = "";
                ListMain.ItemsSource = CorrectFile.methodForList(TBadres.Text, CMB.SelectedValue.ToString());
                TBadres.Text = CMB.SelectedValue.ToString() + "\\";
                ForSavePath = TBadres.Text;
            }
            else if (location == "2")
            {
                TBadres2.Text = "";
                ListForCopy.ItemsSource = CorrectFile.methodForList(TBadres2.Text, CMB2.SelectedValue.ToString());
                TBadres2.Text = CMB2.SelectedValue.ToString() + "\\";
                ForSavePath2 = TBadres2.Text;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TBadres.Text = CMB.SelectedValue.ToString() + "\\";
            ForSavePath = TBadres.Text;
            ListMain.ItemsSource = CorrectFile.correctF((CMB.SelectedValue.ToString() + "\\"), TBadres.Text.Length); 
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            TBadres2.Text = CMB2.SelectedValue.ToString() + "\\";
            ForSavePath2 = TBadres2.Text;
            ListForCopy.ItemsSource = CorrectFile.correctF((CMB2.SelectedValue.ToString() + "\\"), TBadres2.Text.Length);
        }

        public string pathForCopyAndDeleteFile { get; set; }
        private void ListMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            location = "1";
            FileInform.Text = Convert.ToString(ListMain.SelectedItem);
            string path = TBadres.Text + FileInform.Text;
            pathForCopyAndDeleteFile = path;
            FileInfo info = new FileInfo(path);
            if (info.Exists)
            {
                FileInform.Text = info.Name + "  Размер: " + info.Length + "  Расширение: " + info.Extension + "  Дата создания: " + info.CreationTime;
            }
            

        }

        private void CMB_LostFocus(object sender, RoutedEventArgs e)
        {
            location = "1";
        }
        private void CMB_LostFocus2(object sender, RoutedEventArgs e)
        {
            location = "2";
        }
        private void ListForCopy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            location = "2";
            FileInform.Text = Convert.ToString(ListForCopy.SelectedItem);
            string path = TBadres2.Text + FileInform.Text;
            pathForCopyAndDeleteFile = path;
            FileInfo info = new FileInfo(path);
            if (info.Exists)
            {
                FileInform.Text = info.Name + "  Размер: " + info.Length + "  Расширение: " + info.Extension + "  Дата создания: " + info.CreationTime;
            }
        }

        private void OpenTxt_Click(object sender, RoutedEventArgs e)
        {
            OpenTXTFile openTXTFile = new OpenTXTFile();
            //openPhoto openP = new openPhoto();
            ForSavePath1 = TBadres.Text + ListMain.SelectedItem.ToString();
            pathForCreate = ForSavePath1;
            FileInfo fileInfo = new FileInfo(ForSavePath1);
            StreamReader sr = new StreamReader(ForSavePath1, Encoding.Default);
            if (fileInfo.Extension != "TXT")
            {
                //openP.Show();
                //Image image = new Image();
                //BitmapImage bitmapImage = new BitmapImage();
                //bitmapImage.BeginInit();
                //bitmapImage.UriSource = new Uri(ForSavePath1, UriKind.Relative);
                //bitmapImage.EndInit();
                //image.Source = bitmapImage;
                //image.Stretch = Stretch.Uniform;
                //openP.ImageBox.Source = bitmapImage;
                //openP.ImageBox.Stretch = Stretch.Uniform;
            }
            else
            {
                openTXTFile.Show();
                openTXTFile.TBtxt.Text = sr.ReadToEnd();
            }
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (location == "1") 
            {
                string path = TBadres.Text + Convert.ToString(ListMain.SelectedItem);
                pathForCopyAndDeleteFile = path;
                FileInfo info = new FileInfo(path);
                if (info.Exists)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "You seriously want to delete this file: " + info.Name,
                        "Delete",
                        MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        info.Delete();
                        ListMain.ItemsSource = CorrectFile.methodForList(TBadres.Text, "");
                    }
                }
            }
            if (location == "2")
            {
                string path = TBadres2.Text + Convert.ToString(ListForCopy.SelectedItem);
                pathForCopyAndDeleteFile = path;
                FileInfo info = new FileInfo(path);
                if (info.Exists)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "You seriously want to delete the file: " + info.Name,
                        "Delete",
                        MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        info.Delete();
                        ListForCopy.ItemsSource = CorrectFile.methodForList(TBadres2.Text, "");
                    }
                }
            }

        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            if (TBadres.Text != "" && TBadres2.Text != "")
            {
                if (location == "1") 
                {
                    string path = TBadres.Text + ListMain.SelectedItem.ToString();
                    string newPath = TBadres2.Text + ListMain.SelectedItem.ToString();
                    FileInfo fileInfo = new FileInfo(path);
                    if (fileInfo.Exists)
                    {
                        File.Move(path, newPath);
                        ListForCopy.ItemsSource = CorrectFile.methodForList(TBadres2.Text, "");
                        ListMain.ItemsSource = CorrectFile.methodForList(TBadres.Text, "");
                    }
                }
                if (location == "2")
                {
                    string path = TBadres2.Text + ListForCopy.SelectedItem.ToString();
                    string newPath = TBadres.Text + ListForCopy.SelectedItem.ToString();
                    FileInfo fileInfo = new FileInfo(path);
                    if (fileInfo.Exists)
                    {
                        File.Move(path, newPath);
                        ListForCopy.ItemsSource = CorrectFile.methodForList(TBadres2.Text, "");
                        ListMain.ItemsSource = CorrectFile.methodForList(TBadres.Text, "");
                    }
                }

            }

        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (location == "1")
            {
                string path = TBadres.Text + ListMain.SelectedItem.ToString();
                string newPath = TBadres2.Text + ListMain.SelectedItem.ToString();
                FileInfo fileInfo = new FileInfo(path);
                FileInfo fileInfo2 = new FileInfo(newPath);
                if (fileInfo.Exists && !fileInfo2.Exists)
                {
                    File.Copy(path, newPath, true);
                    ListForCopy.ItemsSource = CorrectFile.methodForList(TBadres2.Text, "");
                }
            }
            if (location == "2")
            {
                string path = TBadres2.Text + ListMain.SelectedItem.ToString();
                string newPath =  TBadres.Text + ListMain.SelectedItem.ToString();
                FileInfo fileInfo = new FileInfo(path);
                FileInfo fileInfo2 = new FileInfo(newPath);
                if (fileInfo.Exists && !fileInfo2.Exists)
                {
                    File.Copy(path, newPath, true);
                    ListMain.ItemsSource = CorrectFile.methodForList(TBadres.Text, "");
                }
            }
        }

        public static string pathForCreate { get; set; }
        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            if (TBadres.Text != "")
            {
                pathForCreate = TBadres.Text;
                CreateFile cf = new CreateFile();
                cf.Show();
            }

        }
    }
}
