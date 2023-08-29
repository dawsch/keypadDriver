using Buttons;
using driverClasses;
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

namespace keyboardWindow
{
    /// <summary>
    /// Logika interakcji dla klasy writeMenu.xaml
    /// </summary>
    public partial class writeMenu : UserControl
    {
        public writeMenu()
        {
            InitializeComponent();
        }
        private void buttonPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textToWriteBox.Text.Length == 0)
            {
                errorBox.Visibility = Visibility.Visible;
            }
            else
            {
                DragDrop.DoDragDrop((roundButton)sender, new write(textToWriteBox.Text), DragDropEffects.Move);
            }

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorBox.Visibility = Visibility.Hidden;
        }
    }
}
