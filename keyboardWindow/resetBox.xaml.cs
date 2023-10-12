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

namespace keyboardWindow
{
    /// <summary>
    /// Logika interakcji dla klasy resetBox.xaml
    /// </summary>
    public partial class resetBox : Window
    {
        static resetBox rBox;
        static bool result = false;
        string message;
        public resetBox(string _message)
        {
            InitializeComponent();
            message = _message;
            label.Content = message;
        }
        public static bool show(string message, double xPos = 0, double yPos = 0)
        {
            rBox = new resetBox(message);
            rBox.Top = yPos;
            rBox.Left = xPos;
            rBox.ShowDialog();
            return result;
        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            result = false;
            rBox.Close();
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            result = true;
            rBox.Close();
        }
    }
}
