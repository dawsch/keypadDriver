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
    /// Logika interakcji dla klasy changeLayerMenu.xaml
    /// </summary>
    /// 

    public partial class changeLayerMenu : UserControl
    {
        int changeTo = 0;
        public int ChangeTo
        {
            get { return changeTo; }
            set
            {
                if (value >= 0 && value <= 3)
                {
                    changeTo = value;
                    gotoButton.Content = (changeTo + 1).ToString();
                }
            }
        }
        public changeLayerMenu()
        {
            InitializeComponent();
        }
        private void oneLessClicked(object sender, RoutedEventArgs e)
        {
            ChangeTo = ChangeTo - 1;
        }

        private void oneMoreClicked(object sender, RoutedEventArgs e)
        {
            ChangeTo = ChangeTo + 1;
        }

        private void nextButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop((roundButton)sender, new changeLayer(MainWindow.changeLayerDel, -1), DragDropEffects.Move);
        }

        private void prevButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop((roundButton)sender, new changeLayer(MainWindow.changeLayerDel, -2), DragDropEffects.Move);
        }

        private void gotoButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop((roundButton)sender, new changeLayer(MainWindow.changeLayerDel, changeTo), DragDropEffects.Move);
        }
    }
}
