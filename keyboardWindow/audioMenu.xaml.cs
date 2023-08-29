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
using Buttons;

namespace keyboardWindow
{
    /// <summary>
    /// Logika interakcji dla klasy audioMenu.xaml
    /// </summary>
    public partial class audioMenu : UserControl
    {
        public audioMenu()
        {
            InitializeComponent();
        }
        private void leftButtonDown(object sender, MouseButtonEventArgs e)
        {
            roundButton rb = (roundButton)sender;
            action a = new keyPress(stringToKeyCodeEnum.convertStringToEnum(rb.Tag.ToString()));
            DragDrop.DoDragDrop(rb, a, System.Windows.DragDropEffects.Move);
        }
    }
}
