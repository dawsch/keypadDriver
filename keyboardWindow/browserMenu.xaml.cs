using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
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
using Buttons;
using driverClasses;
using WindowsInput;

namespace keyboardWindow
{
    /// <summary>
    /// Logika interakcji dla klasy browserMenu.xaml
    /// </summary>
    public partial class browserMenu : UserControl
    {
        public browserMenu()
        {
            InitializeComponent();
        }

        private void testButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop((System.Windows.Controls.Button)sender, new openWebsite("https://www.google.pl/"), DragDropEffects.Move);
        }
        private void leftButtonDown(object sender, MouseButtonEventArgs e)
        {
            roundButton rb = (roundButton)sender;
            action a = new keyPress(stringToKeyCodeEnum.convertStringToEnum(rb.Tag.ToString()), rb.Content.ToString());

            DragDrop.DoDragDrop(rb, a, System.Windows.DragDropEffects.Move);
        }
        private void buttonPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            DragDrop.DoDragDrop((System.Windows.Controls.Button)sender, new openWebsite(pathBox.Text), DragDropEffects.Move);
        }

        private void pathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dragButton.Content = new openWebsite(pathBox.Text).getLabel();
        }
        private void leftButtonDownShortcut(object sender, MouseButtonEventArgs e)
        {
            roundButton rb = (roundButton)sender;

            string[] sList = rb.Tag.ToString().Split(',');

            List<WindowsInput.Native.VirtualKeyCode> list = new List<WindowsInput.Native.VirtualKeyCode>();

            foreach(string s in sList)
            {
                list.Add(stringToKeyCodeEnum.convertStringToEnum(s));
            }

            action a = new shortCutPress(list, rb.Content.ToString());
            //action a = new keyPress(stringToKeyCodeEnum.convertStringToEnum(rb.Tag.ToString()));
            DragDrop.DoDragDrop(rb, a, System.Windows.DragDropEffects.Move);
        }
    }
}
