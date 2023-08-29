using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace expandMenuA
{
    /// <summary>
    /// Wykonaj kroki 1a lub 1b, a następnie 2, aby użyć tej kontrolki niestandardowej w pliku XAML.
    ///
    /// Krok 1a) Użycie tej kontrolki niestandardowej w pliku XAML, który istnieje w bieżącym projekcie.
    /// Dodaj ten atrybut XmlNamespace do głównego elementu pliku znaczników, gdzie jest 
    /// do użycia:
    ///
    ///     xmlns:MyNamespace="clr-namespace:expandMenuA"
    ///
    ///
    /// Krok 1b) Użycie tej kontrolki niestandardowej w pliku XAML, który istnieje w innym projekcie.
    /// Dodaj ten atrybut XmlNamespace do głównego elementu pliku znaczników, gdzie jest 
    /// do użycia:
    ///
    ///     xmlns:MyNamespace="clr-namespace:expandMenuA;assembly=expandMenuA"
    ///
    /// Należy również dodać odwołanie do projektu z projektu, w którym znajduje się plik XAML
    /// do tego projektu i skompiluj ponownie, aby uniknąć błędów kompilacji:
    ///
    ///     Kliknij prawym przyciskiem myszy docelowy projekt w Eksploratorze rozwiązań i
    ///     „Dodaj odwołanie”->„Projekty”->[Wybierz ten projekt]
    ///
    ///
    /// Krok 2)
    /// Przejdź dalej i użyj swojego formantu w pliku XAML.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [TemplatePart(Name = expandMenuA.ElementHeadButton, Type = typeof(Button))]
    [TemplatePart(Name = expandMenuA.ElementContent, Type = typeof(Border))]
    [TemplatePart(Name = expandMenuA.ElementMenuArron, Type = typeof(Canvas))]
    [TemplatePart(Name = expandMenuA.ElementConteiner, Type = typeof(ContentPresenter))]
    public class expandMenuA : ContentControl
    {
        private const string ElementHeadButton = "PART_titleButton";
        private const string ElementContent = "PART_content";
        private const string ElementMenuArron = "PART_menuArron";
        private const string ElementConteiner = "PART_Conteiner";

        private Button titleButton;
        private Border content;
        private Canvas menuArrow;
        private ContentPresenter conteiner;

        static expandMenuA()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(expandMenuA), new FrameworkPropertyMetadata(typeof(expandMenuA)));
        }

        public event RoutedEventHandler titleMenuClick
        {
            add { AddHandler(titleButtonClickEvent, value); }
            remove { RemoveHandler(titleButtonClickEvent, value); }
        }

        public static readonly RoutedEvent titleButtonClickEvent =
            EventManager.RegisterRoutedEvent("titleButtonClick",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(expandMenuA));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            menuArrow = this.Template.FindName(ElementMenuArron, this) as Canvas;
            titleButton = this.Template.FindName(ElementHeadButton, this) as Button;
            content = this.Template.FindName(ElementContent, this) as Border;
            conteiner = this.Template.FindName(ElementConteiner, this) as ContentPresenter;

            conteiner.Margin = new Thickness(cornerRadius.TopLeft, 0, cornerRadius.TopLeft, cornerRadius.TopLeft);

            titleButton.Click += new RoutedEventHandler(titleButtonClick);

            menuArrow.RenderTransform = new RotateTransform() { Angle = 180 };
            menuArrow.RenderTransformOrigin = new Point(0.5, 0.5);

            content.Height = titleButton.Height;

            #region arrowAnimation
            Storyboard storyboardOpen = new Storyboard();
            storyboardOpen.Duration = new Duration(TimeSpan.FromMilliseconds(150));
            DoubleAnimation rotateAnimationOpen = new DoubleAnimation()
            {
                To = 0,
                Duration = storyboardOpen.Duration,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5,
            };
            Storyboard.SetTarget(rotateAnimationOpen, menuArrow);
            Storyboard.SetTargetProperty(rotateAnimationOpen, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            storyboardOpen.Children.Add(rotateAnimationOpen);
            Resources.Add("rotateArrowOpen", storyboardOpen);

            Storyboard storyboardClose = new Storyboard();
            storyboardClose.Duration = new Duration(TimeSpan.FromMilliseconds(150));
            DoubleAnimation rotateAnimationClose = new DoubleAnimation()
            {
                To = 180,
                Duration = storyboardClose.Duration,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5,
            };
            Storyboard.SetTarget(rotateAnimationClose, menuArrow);
            Storyboard.SetTargetProperty(rotateAnimationClose, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            storyboardClose.Children.Add(rotateAnimationClose);
            Resources.Add("rotateArrowClose", storyboardClose);
            #endregion

            #region ContentAnimation

            content.Height = titleButton.Height;
            //content.Measure(new Size(conteiner.MaxWidth, conteiner.MaxHeight));
            conteiner.Measure(new Size(content.MaxWidth, content.MaxHeight));
            //DoubleAnimation heightAnimation = new DoubleAnimation(conteiner.DesiredSize.Height + titleButton.Height, _openCloseDuration)

            Storyboard storyboardContentOpen = new Storyboard();
            storyboardContentOpen.Duration = new Duration(TimeSpan.FromMilliseconds(250));

            //storyboardContentOpen.Completed += (sender, args) => { content.Height = double.NaN;

            DoubleAnimation expandContent = new DoubleAnimation()
            {
                To = conteiner.DesiredSize.Height + titleButton.Height,
                Duration = storyboardContentOpen.Duration,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5,
            };
            Storyboard.SetTarget(expandContent, content);
            Storyboard.SetTargetProperty(expandContent, new PropertyPath("Height"));

            storyboardContentOpen.Children.Add(expandContent);
            Resources.Add("expandContent", storyboardContentOpen);

            Storyboard storyboardContentClose = new Storyboard();
            storyboardContentClose.Duration = new Duration(TimeSpan.FromMilliseconds(250));
            DoubleAnimation hideContent = new DoubleAnimation()
            {
                To = titleButton.Height,
                Duration = storyboardContentClose.Duration,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5,
            };
            Storyboard.SetTarget(hideContent, content);
            Storyboard.SetTargetProperty(hideContent, new PropertyPath("Height"));

            storyboardContentClose.Children.Add(hideContent);
            Resources.Add("hideContent", storyboardContentClose);
            #endregion
            //txtFileName.TextChanged += new TextChangedEventHandler(txtFileName_TextChanged);
        }

        public void updateHeight()
        {
            conteiner.Measure(new Size(content.MaxWidth, content.MaxHeight));
            content.Height = conteiner.DesiredSize.Height + titleButton.Height;
        }

        void titleButtonClick(object sender, RoutedEventArgs e)
        {
            if (IsExpanded)
            {
                ((Storyboard)Resources["rotateArrowClose"]).Begin();
                ((Storyboard)Resources["hideContent"]).Begin();
            }
            else
            {
                conteiner.Measure(new Size(content.MaxWidth, content.MaxHeight));
                Storyboard storyboardContentOpen = new Storyboard();
                storyboardContentOpen.Duration = new Duration(TimeSpan.FromMilliseconds(250));

                //storyboardContentOpen.Completed += (sender2, args) => { content.Height = double.NaN; };

                DoubleAnimation expandContent = new DoubleAnimation()
                {
                    To = conteiner.DesiredSize.Height + titleButton.Height,
                    Duration = storyboardContentOpen.Duration,
                    AccelerationRatio = 0.5,
                    DecelerationRatio = 0.5,
                };
                Storyboard.SetTarget(expandContent, content);
                Storyboard.SetTargetProperty(expandContent, new PropertyPath("Height"));

                storyboardContentOpen.Children.Add(expandContent);

                storyboardContentOpen.Begin();
                ((Storyboard)Resources["rotateArrowOpen"]).Begin();
                //((Storyboard)Resources["expandContent"]).Begin();
                //content.Height = Double.NaN;
            }
            IsExpanded = !IsExpanded;
            //toryboard.Begin();
        }



        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(expandMenuA), new PropertyMetadata(false));


        public string menuTitle
        {
            get { return (string)GetValue(menuTitleProperty); }
            set { SetValue(menuTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for menuTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty menuTitleProperty =
            DependencyProperty.Register("menuTitle", typeof(string), typeof(expandMenuA), new PropertyMetadata("menu"));



        const double defaultHeight = 40;
        public double expanderHeight
        {
            get { return (double)GetValue(expanderHeightProperty); }
            set { SetValue(expanderHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for expanderHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty expanderHeightProperty =
            DependencyProperty.Register("expanderHeight", typeof(double), typeof(expandMenuA), new PropertyMetadata(defaultHeight));



        public CornerRadius cornerRadius
        {
            get { return (CornerRadius)GetValue(cornerRadiusProperty); }
            set { SetValue(cornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cornerRadiusProperty =
            DependencyProperty.Register("cornerRadius", typeof(CornerRadius), typeof(expandMenuA), new PropertyMetadata(new CornerRadius(8)));


        public ImageSource imageSource
        {
            get { return (ImageSource)GetValue(imageSourceProperty); }
            set { SetValue(imageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for imageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty imageSourceProperty =
            DependencyProperty.Register("imageSource", typeof(ImageSource), typeof(expandMenuA), new PropertyMetadata(null));
    }
}
