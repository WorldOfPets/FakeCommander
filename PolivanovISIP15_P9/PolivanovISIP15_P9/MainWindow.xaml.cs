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
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                string path = "";
                foreach (DriveInfo drive in drives)
                {
                    if (path == "") { path = drive.Name; }
                    CMB.Items.Add(drive.Name);
                    CMB2.Items.Add(drive.Name);
                }
                #region Инициализация листов
                int correct = path.Length;
                TBadres.Text = path + "\\";
                TBadres2.Text = path + "\\";
                string[] dirsPath = Directory.GetDirectories(path);
                string[] filesPath = Directory.GetFiles(path);
                string[] all = dirsPath.Concat(filesPath).ToArray();
                string[] allcorrect = new string[all.Length];
                for (int j = 0; j < all.Length; j++)
                {
                    string s = Convert.ToString(all[j]);
                    allcorrect[j] = s.Remove(0, correct);
                }
                ListMain.ItemsSource = allcorrect;
                ListForCopy.ItemsSource = allcorrect;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }
            

        }
        #region Переменные
        public string location { get; set; }
        public string ForSavePath { get; set; }
        public string ForSavePath1 { get; set; }
        public string ForSavePath2 { get; set; }
        public static string proverka { get; set; }
        public string pathForCopyAndDeleteFile { get; set; }
        public static string pathForCreate { get; set; }
        #endregion

        #region Двойной клик
        public void ListMain_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OpenTXTFile openTXTFile = new OpenTXTFile();
                if (location == "1")
                {
                    ForSavePath += Convert.ToString(ListMain.SelectedItem) + "\\";
                    if (Directory.Exists(ForSavePath))
                    {
                        ListMain.ItemsSource = CorrectFile.methodForList(TBadres.Text, ListMain.SelectedItem);
                        TBadres.Text = ForSavePath;
                    }
                    else
                    {
                        ForSavePath1 = ListMain.SelectedItem.ToString();
                        string path = TBadres.Text + ListMain.SelectedItem.ToString();
                        FileInfo info = new FileInfo(path);
                        if (info.Extension == ".txt")
                        {
                            pathForCreate = path;
                            StreamReader sr = new StreamReader(path, Encoding.Default);
                            openTXTFile.Show();
                            openTXTFile.TBtxt.Text = sr.ReadToEnd();
                            proverka = openTXTFile.TBtxt.Text;
                        }
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
                    else if (System.IO.File.Exists(ForSavePath2))
                    {
                        ForSavePath1 = ListMain.SelectedItem.ToString();
                        string path = TBadres2.Text + ListMain.SelectedItem.ToString();
                        FileInfo info = new FileInfo(path);
                        if (info.Extension == ".txt")
                        {
                            pathForCreate = path;
                            StreamReader sr = new StreamReader(path, Encoding.Default);
                            openTXTFile.Show();
                            openTXTFile.TBtxt.Text = sr.ReadToEnd();
                            proverka = openTXTFile.TBtxt.Text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }
        }
#endregion

        #region Комбобоксы
        private void CMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }
        }
        #endregion

        #region Кнопки назад
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                TBadres.Text = CMB.SelectedValue.ToString() + "\\";
                ForSavePath = TBadres.Text;
                ListMain.ItemsSource = CorrectFile.correctF((CMB.SelectedValue.ToString() + "\\"), TBadres.Text.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            try
            {
                TBadres2.Text = CMB2.SelectedValue.ToString() + "\\";
                ForSavePath2 = TBadres2.Text;
                ListForCopy.ItemsSource = CorrectFile.correctF((CMB2.SelectedValue.ToString() + "\\"), TBadres2.Text.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }
        }
        #endregion

        #region Определяем в каком листе находится пользователь
        private void ListMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Информация о файле
            try
            {
                location = "1";
                FileInform.Text = Convert.ToString(ListMain.SelectedItem);
                string path = TBadres.Text + FileInform.Text;
                FileInfo info = new FileInfo(path);
                if (info.Exists)
                {
                    FileInform.Text = info.Name + "  Размер: " + info.Length + "  Расширение: " + info.Extension + "  Дата создания: " + info.CreationTime;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
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
            //Информация о файле
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }
        }
        #endregion

        #region Открытие фото
        private void OpenTxt_Click(object sender, RoutedEventArgs e)
        {
            //OpenTXTFile openTXTFile = new OpenTXTFile();
            openPhoto openP = new openPhoto();
            //ForSavePath1 = TBadres.Text + ListMain.SelectedItem.ToString();
            //pathForCreate = ForSavePath1;
            //FileInfo fileInfo = new FileInfo(ForSavePath1);
            //StreamReader sr = new StreamReader(ForSavePath1, Encoding.Default);
            openP.Show();
            //Image image = new Image();
            //BitmapImage bitmapImage = new BitmapImage();
            //bitmapImage.BeginInit();
            //bitmapImage.UriSource = new Uri(ForSavePath1, UriKind.Relative);
            //bitmapImage.EndInit();
            //image.Source = bitmapImage;
            //image.Stretch = Stretch.Uniform;
            //openP.ImageBox.Source = bitmapImage;
            //openP.ImageBox.Stretch = Stretch.Uniform;
            //openTXTFile.Show();
            //openTXTFile.TBtxt.Text = sr.ReadToEnd();
        }
        #endregion

        #region Удаление файла
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }
        }
        #endregion

        #region Перемещение
        private void Move_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }

        }
        #endregion

        #region Копирование
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            try
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
                    string newPath = TBadres.Text + ListMain.SelectedItem.ToString();
                    FileInfo fileInfo = new FileInfo(path);
                    FileInfo fileInfo2 = new FileInfo(newPath);
                    if (fileInfo.Exists && !fileInfo2.Exists)
                    {
                        File.Copy(path, newPath, true);
                        ListMain.ItemsSource = CorrectFile.methodForList(TBadres.Text, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }
        }
        #endregion

        #region Создание
        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TBadres.Text != "")
                {
                    pathForCreate = TBadres.Text;
                    CreateFile cf = new CreateFile();
                    cf.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка.");
            }
        }

        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
