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
    /// Logika interakcji dla klasy buttonInfoListItem.xaml
    /// </summary>
    /// 

    public enum buttonActionStatus
    {
        press,
        release
    }

    public partial class buttonInfoListItem : UserControl
    {
        public string title
        {
            get { return (string)GetValue(titleProperty); }
            set { SetValue(titleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty titleProperty =
            DependencyProperty.Register("title", typeof(string), typeof(buttonInfoListItem), new PropertyMetadata("none"));


        internal removeAction removeEvent;
        internal updateKeypadButtonControl updateKeypadButtonControl;


        public buttonActionStatus pressOrRelease
        {
            get { return (buttonActionStatus)GetValue(pressOrReleaseProperty); }
            set { SetValue(pressOrReleaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for pressOrRelease.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty pressOrReleaseProperty =
            DependencyProperty.Register("pressOrRelease", typeof(buttonActionStatus), typeof(buttonInfoListItem), new PropertyMetadata(buttonActionStatus.press));

        public buttonInfoListItem()
        {

            InitializeComponent();
        }
        public void setKeyList(List<string> keyList)
        {
            for (int i = 0; i < keyList.Count; i++)
            {
                keyListPanel.Children.Add(new Label { Content = keyList[i], Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)), Height = 25 });
            }
            ContBorder.Height = 25 * keyList.Count + 15;
            ContBorder.Visibility = Visibility.Visible;
            canvas.Height = 25 * keyList.Count + 30;
        }

        private void removeMe(object sender, RoutedEventArgs e)
        {
            removeEvent((string)this.Tag);
            updateKeypadButtonControl();
        }
    }

    public class offsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Double)value - Double.Parse((string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
