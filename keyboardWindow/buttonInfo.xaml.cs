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
    delegate void windowClose();
    delegate void removeAction(string tag);
    internal delegate void updateKeypadButtonControl();
    public partial class buttonInfo : UserControl
    {
        internal windowClose windowClose;

        driverClasses.Button button;
        updateKeypadButtonControl updateKeypadC;

        public buttonInfo()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }
        internal void setTitle(string title)
        {
            titleBox.Text = title;
        }
        internal void setButton(ref driverClasses.Button _button, string title, updateKeypadButtonControl _updateKeypadC)
        {
            button = _button;
            updateKeypadC = _updateKeypadC;
            setTitle(title);
            this.Visibility=Visibility.Visible;

            listPanel.Children.RemoveRange(0, listPanel.Children.Count);

            for (int i = 0; i < button.actions.Count; i++)
            {
                buttonInfoListItem bi = new buttonInfoListItem();

                string actionTitle = button.actions[i].getLabel();
                List<string> addInfo = button.actions[i].getAddInfo();

                bi.title = actionTitle;
                bi.Tag = i.ToString();
                bi.removeEvent += removeItem;
                bi.updateKeypadButtonControl = updateKeypadC;
                if (addInfo != null)
                    bi.setKeyList(addInfo);

                bi.Margin = new Thickness(2);

                listPanel.Children.Add(bi);
            }
        }

        private void closeButtonClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            windowClose();
        }
        void removeItem(string tag)
        {
            int index = 0;
            while((string)((buttonInfoListItem)listPanel.Children[index]).Tag != tag)
            {
                index++;
            }
            button.removeAction(index);
            listPanel.Children.RemoveAt(index);
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            action a = (action)e.Data.GetData(e.Data.GetFormats()[0]);
            button.addAction(a);
            updateKeypadC();
            setButton(ref button, titleBox.Text, updateKeypadC);
            //updateFunctionListView();
            //buttonActiveStatusChange(this);
        }
    }
}
