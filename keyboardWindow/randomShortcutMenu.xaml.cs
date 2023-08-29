using driverClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace keyboardWindow
{
    public partial class randomShortcutMenu : UserControl
    {
        List<VirtualKeyCode> randShortcut;
        List<VirtualKeyCode> keysToBeRand;
        public int shortcutLen
        {
            get { return randShortcut.Count; }
            set { 
                if (value >= 3 && value <= 12)
                {
                    keysNumber.Content = value.ToString();
                    if (value != randShortcut.Count)
                    {
                        if (value < randShortcut.Count)
                        {
                            randShortcut.RemoveRange(value, randShortcut.Count - value);
                            randomKeys.Children.RemoveAt(randomKeys.Children.Count - 1);
                        }
                        else
                        {
                            for (int i = 0; i < value - randShortcut.Count; i++)
                                addNewKey();
                        }
                    }
                }
            }
        }
        public randomShortcutMenu()
        {
            InitializeComponent();

            Array _keysToBeRand = Enum.GetValues(typeof(VirtualKeyCode));
            keysToBeRand = _keysToBeRand.Cast<VirtualKeyCode>().ToList();
            for (int i = 0; i < keysToBeRand.Count; i++)
            {
                if (!Regex.Match(keysToBeRand[i].ToString(), @"^VK_*").Success)
                {
                    keysToBeRand.RemoveAt(i);
                    i--;
                }
            }

            //randShortcut = new List<VirtualKeyCode>();
            //randShortcut.Add(VirtualKeyCode.CONTROL);
            //randShortcut.Add(VirtualKeyCode.SHIFT);
            //randomKeys.Children.Add(new Label { Content = "Ctrl", Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)), Background = new SolidColorBrush(Color.FromRgb(35, 35, 35))});
            //Grid.SetRow(randomKeys.Children[0], 0);
            //randomKeys.Children.Add(new Label { Content = "Shift", Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)), Background = new SolidColorBrush(Color.FromRgb(35, 35, 35))});
            //Grid.SetRow(randomKeys.Children[1], 1);
            //addNewKey();
            generateKeys(3);
        }

        private void oneLessClicked(object sender, RoutedEventArgs e)
        {
            shortcutLen = shortcutLen - 1;
        }

        private void oneMoreClicked(object sender, RoutedEventArgs e)
        {
            shortcutLen = shortcutLen + 1;
        }
        private void addNewKey()
        {
            Random rand = new Random();
            VirtualKeyCode key;
            do
            {
                key = keysToBeRand[rand.Next(0, keysToBeRand.Count)];

            } while (randShortcut.Any(item => string.Equals(item.ToString(), key.ToString())));
            randShortcut.Add(key);
            randomKeys.Children.Add(new Label { Content = key.ToString(), Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)) });
            int len = randomKeys.Children.Count;
            if (len % 3 - 1 < 0)
                Grid.SetRow(randomKeys.Children[len - 1], 2);
            else
                Grid.SetRow(randomKeys.Children[len - 1], len % 3 - 1);
            if (len % 3 == 0)
                Grid.SetColumn(randomKeys.Children[len - 1], (int)len / 3 - 1);
            else
                Grid.SetColumn(randomKeys.Children[len - 1], (int)len / 3);
        }

        private void generateKeys(int amount)
        {
            randomKeys.Children.Clear();
            randShortcut = new List<VirtualKeyCode>();
            randShortcut.Add(VirtualKeyCode.CONTROL);
            randShortcut.Add(VirtualKeyCode.SHIFT);
            randomKeys.Children.Add(new Label { Content = "Ctrl", Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)) });
            Grid.SetRow(randomKeys.Children[0], 0);
            randomKeys.Children.Add(new Label { Content = "Shift", Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)) });
            Grid.SetRow(randomKeys.Children[1], 1);
            addNewKey();

            for (int i = 3; i < amount; i++)
            {
                addNewKey();
            }
        }

        private void regenerateClicked(object sender, RoutedEventArgs e)
        {
            generateKeys(randomKeys.Children.Count);
        }

        private void leftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string keyStringList = "";
            foreach(VirtualKeyCode keyCode in randShortcut)
            {
                keyStringList += keyCode.ToString();
                keyStringList += ";";
            }
            string title = "randMacro" + "{" + keyStringList + "}";
            action a = new shortCutPress(randShortcut, title);
            DragDrop.DoDragDrop(randomKeys, a, System.Windows.DragDropEffects.Move);
        }
    }
}
