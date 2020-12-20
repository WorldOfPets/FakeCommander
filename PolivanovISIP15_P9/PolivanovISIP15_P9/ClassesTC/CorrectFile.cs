using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PolivanovISIP15_P9.ClassesTC
{
    class CorrectFile
    {
        public static string[] correctF(string path, int correct) 
        {
            MainWindow mainWindow = new MainWindow();
            string[] dirsPath = Directory.GetDirectories(path);
            string[] filesPath = Directory.GetFiles(path);
            string[] all = dirsPath.Concat(filesPath).ToArray();
            string[] allcorrect = new string[all.Length];
            for (int j = 0; j < all.Length; j++)
            {
                string s = Convert.ToString(all[j]);
                allcorrect[j] = s.Remove(0, correct);
            }
            return allcorrect;
        }
        public static string[] methodForList(string tb, object listItem) 
        { 
            if(listItem.ToString() != "")
            { 
                tb += Convert.ToString(listItem) + "\\";
            }
            string path = tb.ToString();
            int correct = tb.Length;
            return CorrectFile.correctF(path, correct);
        }
    }
}
