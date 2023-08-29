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

namespace Buttons
{
    public class roundButton : Button
    {


        public int cornerRadius
        {
            get { return (int)GetValue(cornerRadiusProperty); }
            set { SetValue(cornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cornerRadiusProperty =
            DependencyProperty.Register("cornerRadius", typeof(int), typeof(roundButton), new PropertyMetadata(0));



        public Brush backgroundTriggerHover
        {
            get { return (Brush)GetValue(brushProperty); }
            set { SetValue(brushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for brush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty brushProperty =
            DependencyProperty.Register("brush", typeof(Brush), typeof(roundButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(63,63,63))));




        public Brush backgroundTriggerClick
        {
            get { return (Brush)GetValue(backgroundTriggerClickProperty); }
            set { SetValue(backgroundTriggerClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for backgroundTriggerClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty backgroundTriggerClickProperty =
            DependencyProperty.Register("backgroundTriggerClick", typeof(Brush), typeof(roundButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(76, 76, 76))));




        static roundButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(roundButton), new FrameworkPropertyMetadata(typeof(roundButton)));
        }
    }
}
