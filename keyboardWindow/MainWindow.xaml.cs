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
using expandMenuA;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using driverClasses;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Resources;
using System.Windows.Media.Effects;

namespace keyboardWindow
{
    public partial class MainWindow : Window
    {
        NotifyIcon notifyIcon;
        List<TextBlock> layerMenuButtons;
        List<keyPadLayer> keyPadLayers;
        List<Grid> g;
        int curentLayer = 0;
        resetKeylayout resetKeylayoutDel;
        saveKeylayout saveKeylayoutDel;
        reconnect reconnect;
        public static changeLayerDel changeLayerDel;
        List<List<driverClasses.Button>> buttons;

        public MainWindow(List<List<driverClasses.Button>> buttons, connectioStatus conStatus, driverClasses.resetKeylayout _resetKeyLayoutDel, driverClasses.saveKeylayout _saveKeylayoutDel, driverClasses.changeLayerDel _changeLayerDel, driverClasses.reconnect _reconnect)
        {
            InitializeComponent();

            notifyIcon = new NotifyIcon();
            keyPadLayers = new List<keyPadLayer>();
            resetKeylayoutDel = _resetKeyLayoutDel;
            saveKeylayoutDel = _saveKeylayoutDel;
            reconnect = _reconnect;
            changeLayerDel = _changeLayerDel;
            this.buttons = buttons;

            Stream iconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/assets/icon2.ico")).Stream;
            notifyIcon.Icon = new System.Drawing.Icon(iconStream);

            notifyIcon.Visible = true;
            notifyIcon.MouseClick += notifyIconClick;
            notifyIcon.Text = "KeyPad";

            if (conStatus == connectioStatus.disconnected)
            {
                conPanel.Visibility = Visibility.Visible;
            }

            layerMenuButtons = new List<TextBlock>();
            layerMenuButtons.Add(layerMenuButton1);
            layerMenuButtons.Add(layerMenuButton2);
            layerMenuButtons.Add(layerMenuButton3);
            layerMenuButtons.Add(layerMenuButton4);

            foreach (TextBlock item in layerMenuButtons)
            {
                item.PreviewMouseUp += layerMenuButton_clik;
            }

            keyPadLayers = createKepadLayers(buttons);
            g = getGrids(keyPadLayers);
            rightGrid.Children.Add(g[curentLayer]);
        }

        public void showConnectingWindow()
        {
            reconnetWindow rw = new reconnetWindow();
            rw.Top = this.Top + (this.Height / 2 - rw.Height / 2);
            rw.Left = this.Left + (this.Width / 2 - rw.Width / 2);
            blurIn();
            rw.ShowDialog();
        }

        private void refrashClick(object sender, RoutedEventArgs e)
        {
            reconnect();
        }

        private List<keyPadLayer> createKepadLayers(List<List<driverClasses.Button>> buttons)
        {
            List<keyPadLayer> layers = new List<keyPadLayer>();
            for (int i = 0; i < 4; i++)
            {
                driverClasses.Button[,] bArray = buttonListTo2darray.convert(3, 4, buttons[i], new List<int[]>() { new int[] { 0, 3 } });
                layers.Add(new keyPadLayer(3, 4, bArray));
                //keyPadLayers[i].disableButton(0, 0);
                layers[i].setButtonInfoWidnow(buttonInfoPanel);
            }

            return layers;
        }
        private List<Grid> getGrids(List<keyPadLayer> layers)
        {
            List<Grid> grids = new List<Grid>();
            grids = new List<Grid>();
            for (int i = 0; i < layers.Count; i++)
            {
                grids.Add(layers[i].GetGrid());
                grids[i].Margin = new Thickness(40, 15, 40, 10);
                Grid.SetRow(grids[i], 1);

                Ellipse e = new Ellipse()
                {
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(204, 204, 204)),
                    StrokeThickness = 8,
                    Margin = new Thickness(5),
                    Width = double.NaN,
                };

                e.SetBinding(Ellipse.HeightProperty, "width");

                grids[i].Children.Add(e);
                Grid.SetRow(e, 0);
                Grid.SetColumn(e, 0);
            }
            return grids;
        }

        private void layerMenuButton_clik(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = int.Parse(tb.Tag.ToString());

            foreach (TextBlock item in layerMenuButtons)
            {
                item.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(228, 228, 228));
            }

            tb.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));

            Storyboard storyboardMove1 = new Storyboard();
            storyboardMove1.Duration = new Duration(TimeSpan.FromMilliseconds(100));

            double x1 = 47 * index + (((sliderTrack.ActualWidth - 47 * 3) / 4) * index);

            DoubleAnimation move1 = new DoubleAnimation()
            {
                To = x1,
                Duration = storyboardMove1.Duration,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0,
            };
            Storyboard.SetTarget(move1, slider);
            Storyboard.SetTargetProperty(move1, new PropertyPath("X1"));

            storyboardMove1.Children.Add(move1);


            Storyboard storyboardMove2 = new Storyboard();
            storyboardMove2.Duration = new Duration(TimeSpan.FromMilliseconds(100));

            double x2 = 47 * index + 47 + (((sliderTrack.ActualWidth - 47 * 3) / 4) * index);

            DoubleAnimation move2 = new DoubleAnimation()
            {
                To = x2,
                Duration = storyboardMove2.Duration,
                AccelerationRatio = 1,
                DecelerationRatio = 0,
            };
            Storyboard.SetTarget(move2, slider);
            Storyboard.SetTargetProperty(move2, new PropertyPath("X2"));

            storyboardMove2.Children.Add(move2);

            storyboardMove1.Begin();
            storyboardMove2.Begin();

            rightGrid.Children.RemoveAt(rightGrid.Children.Count - 1);
            rightGrid.Children.Add(g[index]);
            keyPadLayers[curentLayer].closeButtonWindowPanel();
            buttonInfoPanel.Visibility = Visibility.Hidden;

            curentLayer = index;
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
            e.Cancel = true;
        }

        void notifyIconClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate();
            this.ShowInTaskbar = true;
        }
        private void m_notifyIcon_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void OnClose(object sender, CancelEventArgs args)
        {
            notifyIcon.Dispose();
            notifyIcon = null;
        }

        private void blurIn()
        {
            var storyboard = new Storyboard();
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 8,
                Duration = new TimeSpan(0, 0, 0, 1, 0),
                AutoReverse = true
            };
            var effect = new BlurEffect();
            this.Effect = effect;
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(storyboard, this.Effect);
            Storyboard.SetTargetProperty(storyboard, new PropertyPath("Radius"));
            storyboard.Begin();
        }
        private void blurOut()
        {
            var storyboardO = new Storyboard();
            var animationO = new DoubleAnimation
            {
                From = 8,
                To = 0,
                Duration = new TimeSpan(0, 0, 0, 1, 0),
                AutoReverse = true
            };
            var effectO = new BlurEffect();
            this.Effect = effectO;
            storyboardO.Children.Add(animationO);
            Storyboard.SetTarget(storyboardO, this.Effect);
            Storyboard.SetTargetProperty(storyboardO, new PropertyPath("Radius"));
            storyboardO.Begin();
        }
        
        private void resetClick(object sender, RoutedEventArgs e)
        {
            blurIn();

            if (resetBox.show("Do you want to reset current Layout?", this.Left + this.Width / 2 - 170, this.Top + this.Height / 2 - 75))
            {
                if (resetKeylayoutDel != null)
                {
                    buttons = resetKeylayoutDel();
                    rightGrid.Children.RemoveAt(rightGrid.Children.Count - 1);
                    keyPadLayers = createKepadLayers(buttons);
                    g = getGrids(keyPadLayers);
                    rightGrid.Children.Add(g[curentLayer]);
                }
            }

            blurOut();
            
            this.Effect = null;
        }
        private void saveClick(object sender, RoutedEventArgs e)
        {
            blurIn();

            if (resetBox.show("Do you want to save current layout?", this.Left + this.Width / 2 - 170, this.Top + this.Height / 2 - 75))
            {
                if (saveKeylayoutDel != null)
                {
                    saveKeylayoutDel();
                }
            }

            blurOut();

            this.Effect = null;
        }
        public static void test()
        {

        }


    }
}
