using System.Windows;
using System.Windows.Controls;

namespace MyControls
{
    public class HeaderFooterControl : HeaderedContentControl
    {
        static HeaderFooterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderFooterControl), new FrameworkPropertyMetadata(typeof(HeaderFooterControl)));
        }

        public object Footer
        {
            get { return (object)GetValue(FooterProperty); }
            set { SetValue(FooterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Footer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FooterProperty =
            DependencyProperty.Register("Footer", typeof(object), typeof(HeaderFooterControl), new UIPropertyMetadata(null));
    }
}
