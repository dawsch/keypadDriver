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
    /// Logika interakcji dla klasy windowsShortcutMenu.xaml
    /// </summary>
    public partial class windowsShortcutMenu : UserControl
    {
        public windowsShortcutMenu()
        {
            InitializeComponent();
        }
        private void leftButtonDown(object sender, MouseButtonEventArgs e)
        {
            roundButton rb = (roundButton)sender;

            string[] sList = rb.Tag.ToString().Split(',');

            List<WindowsInput.Native.VirtualKeyCode> list = new List<WindowsInput.Native.VirtualKeyCode>();

            foreach (string s in sList)
            {
                list.Add(stringToKeyCodeEnum.convertStringToEnum(s));
            }

            action a = new shortCutPress(list, rb.Content.ToString());
            DragDrop.DoDragDrop(rb, a, System.Windows.DragDropEffects.Move);
        }
    }
}
