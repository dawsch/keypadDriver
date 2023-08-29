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
    public partial class keypadButtonControl : UserControl
    {
        internal driverClasses.Button b;
        internal buttonActiveStatusChange buttonActiveStatusChange;

        internal bool isActive = false;
        internal string name;

        public keypadButtonControl()
        {
            InitializeComponent();

            border.PreviewMouseMove += Border_PreviewMouseMove;
            border.MouseLeave += Border_MouseLeave;
            border.PreviewDragEnter += Border_PreviewDragEnter;
            border.PreviewDragLeave += Border_PreviewDragLeave;
            border.PreviewMouseLeftButtonUp += Border_PreviewMouseLeftButtonUp;

            this.PreviewDrop += KeypadButtonControl_PreviewDrop;

            b = new driverClasses.Button();
        }
        public keypadButtonControl(driverClasses.Button _b)
        {
            InitializeComponent();

            border.PreviewMouseMove += Border_PreviewMouseMove;
            border.MouseLeave += Border_MouseLeave;
            border.PreviewDragEnter += Border_PreviewDragEnter;
            border.PreviewDragLeave += Border_PreviewDragLeave;
            border.PreviewMouseLeftButtonUp += Border_PreviewMouseLeftButtonUp;

            this.PreviewDrop += KeypadButtonControl_PreviewDrop;

            b = _b;
            updateFunctionListView();
        }
        public void setButton(driverClasses.Button _b)
        {
            b = _b;
            updateFunctionListView();
        }

        private void Border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            buttonActiveStatusChange(this);
        }

        internal void updateFunctionListView()
        {
            functionListView.Children.Clear();
            string[] labeles = b.getLabeles();
            foreach (string item in labeles)
            {
                TextBlock tb = new TextBlock()
                {
                    Text = item,
                    Foreground = new SolidColorBrush(Color.FromRgb(230, 230, 230)),
                };
                functionListView.Children.Add(tb);
            }
        }

        private void KeypadButtonControl_PreviewDrop(object sender, DragEventArgs e)
        {
            action a = (action)e.Data.GetData(e.Data.GetFormats()[0]);
            b.addAction(a);
            updateFunctionListView();
            buttonActiveStatusChange(this);
        }

        internal void addAction(action a)
        {
            b.addAction(a);
        }

        private void Border_PreviewDragLeave(object sender, DragEventArgs e)
        {
            if (!isActive) border.Margin = new Thickness(5);
        }

        private void Border_PreviewDragEnter(object sender, DragEventArgs e)
        {
            border.Margin = new Thickness(2);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isActive) border.Margin = new Thickness(5);
        }

        private void Border_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            border.Margin = new Thickness(2);
        }
        internal void setAsActive()
        {
            border.Margin = new Thickness(2);
            border.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            isActive = true;
        }
        internal void disableActive()
        {
            border.Margin = new Thickness(5);
            border.BorderBrush = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            isActive = false;
        }
    }
}
