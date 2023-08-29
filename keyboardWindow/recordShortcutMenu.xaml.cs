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
using WindowsInput.Native;
using driverClasses;

namespace keyboardWindow
{
    public partial class recordShortcutMenu : UserControl
    {
        List<VirtualKeyCode> keys;
        bool recording = false;
        bool Recording
        {
            get { return recording; }
            set {
                if (value == true)
                {
                    indicator.Visibility = Visibility.Visible;
                    recButton.Content = "Stop";
                }
                else
                {
                    indicator.Visibility = Visibility.Hidden;
                    recButton.Content = "Start";
                }
                recording = value; }
        }

        public recordShortcutMenu()
        {
            InitializeComponent();
            keys = new List<VirtualKeyCode>();
            Keyboard.AddKeyDownHandler(this, OnKeyDownHandler);
        }

        private void addNewKey(VirtualKeyCode key)
        {
            if (keys.Count <= 12 && !keys.Any(item => string.Equals(item.ToString(), key.ToString())))
            {
                keys.Add(key);
                recKeys.Children.Add(new Label { Content = key.ToString(), Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)) });
                int len = recKeys.Children.Count;
                if (len % 3 - 1 < 0)
                    Grid.SetRow(recKeys.Children[len - 1], 2);
                else
                    Grid.SetRow(recKeys.Children[len - 1], len % 3 - 1);
                if (len % 3 == 0)
                    Grid.SetColumn(recKeys.Children[len - 1], (int)len / 3 - 1);
                else
                    Grid.SetColumn(recKeys.Children[len - 1], (int)len / 3);
            }
        }


        private void startRecording(object sender, RoutedEventArgs e)
        {
            if (Recording == false)
                Recording = true;
            else
                Recording = false;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (recording == true)
            {
                VirtualKeyCode CodeOfKeyToEmulate = (VirtualKeyCode)KeyInterop.VirtualKeyFromKey(e.Key);
                addNewKey(CodeOfKeyToEmulate);
            }
        }
        private void leftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string keyStringList = "";
            foreach (VirtualKeyCode keyCode in keys)
            {
                keyStringList += keyCode.ToString();
                keyStringList += ";";
            }
            string title = "recMacro" + "{" + keyStringList + "}";
            action a = new shortCutPress(keys, title);
            DragDrop.DoDragDrop(recKeys, a, System.Windows.DragDropEffects.Move);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            keys.Clear();
            recKeys.Children.Clear();
            keys = new List<VirtualKeyCode>();
            Recording = false;
        }
    }
}
