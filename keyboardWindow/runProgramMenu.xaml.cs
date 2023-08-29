using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Buttons;
using driverClasses;
using System.IO;

namespace keyboardWindow
{
    /// <summary>
    /// Logika interakcji dla klasy runProgramMenu.xaml
    /// </summary>
    public partial class runProgramMenu : UserControl
    {
        public runProgramMenu()
        {
            InitializeComponent();

            string calcPath = @"C:\Windows\System32\calc.exe";
            Icon iconCalc = System.Drawing.Icon.ExtractAssociatedIcon(calcPath);
            calculator.Tag = calcPath;
            calculator.Content = new System.Windows.Controls.Image()
            {
                Source = Imaging.CreateBitmapSourceFromHIcon(iconCalc.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            };

            string explorerPath = @"C:\Windows\explorer.exe";
            Icon iconExplorer = System.Drawing.Icon.ExtractAssociatedIcon(explorerPath);
            fileExplorer.Tag = explorerPath;
            fileExplorer.Content = new System.Windows.Controls.Image()
            {
                Source = Imaging.CreateBitmapSourceFromHIcon(iconExplorer.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            };

            string notepadPath = @"C:\Windows\System32\notepad.exe";
            Icon iconNotepad = System.Drawing.Icon.ExtractAssociatedIcon(notepadPath);
            notepad.Tag = notepadPath;
            notepad.Content = new System.Windows.Controls.Image()
            {
                Source = Imaging.CreateBitmapSourceFromHIcon(iconNotepad.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            };

            string taskPath = @"C:\WINDOWS\system32\Taskmgr.exe";
            Icon taskIcon = System.Drawing.Icon.ExtractAssociatedIcon(taskPath);
            task.Tag = taskPath;
            task.Content = new System.Windows.Controls.Image()
            {
                Source = Imaging.CreateBitmapSourceFromHIcon(taskIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            };
        }

        private void program_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            roundButton rb = (roundButton)sender;
            DragDrop.DoDragDrop(rb, new runProgram(rb.Tag.ToString()), DragDropEffects.Move);
        }

        private void buttonPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!File.Exists(pathBox.Text))
            {
                errorBox.Visibility = Visibility.Visible;
            }
            else
            {
                DragDrop.DoDragDrop((roundButton)sender, new runProgram(pathBox.Text), DragDropEffects.Move);
            }
        }

        private void pathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorBox.Visibility = Visibility.Hidden;
            if (File.Exists(pathBox.Text))
            {
                dragButton.Content = pathBox.Text.Split('\\').Last<string>();
            }
            else
            {
                dragButton.Content = "Choose path";
            }
        }
    }
}
