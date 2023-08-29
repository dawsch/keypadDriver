using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using driverClasses;

namespace keyboardWindow
{
    internal delegate void buttonActiveStatusChange(keypadButtonControl control);

    public class keyPadLayer
    {
        keypadButtonControl[,] layout;
        int width, height;

        buttonInfo buttonInfoPanel;

        public keyPadLayer(int width, int height)
        {
            this.width = width;
            this.height = height;
            layout = new keypadButtonControl[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    layout[i, j] = new keypadButtonControl();
                    layout[i, j].buttonActiveStatusChange += setButtonAsActive;
                    layout[i, j].name = "Button " + (i + (3 * j)).ToString();
                }
            }
        }
        public keyPadLayer(int width, int height, driverClasses.Button[,] buttons)
        {
            this.width = width;
            this.height = height;
            layout = new keypadButtonControl[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (buttons[i, j] == null)
                        continue;
                    layout[i, j] = new keypadButtonControl(buttons[i, j]);
                    layout[i, j].buttonActiveStatusChange += setButtonAsActive;
                    layout[i, j].name = "Button " + (i + (3 * j)).ToString();
                }
            }
        }
        internal void closeButtonWindowPanel()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (layout[i, j] != null) layout[i, j].disableActive();
                }
            }
        }
        internal void setButtonInfoWidnow(buttonInfo bi)
        {
            buttonInfoPanel = bi;
            buttonInfoPanel.windowClose += closeButtonWindowPanel;
        }
        public void disableButton(int x, int y)
        {
            layout[x, y] = null;
        }
        public void addAction(int x, int y, action a)
        {
            layout[x, y].addAction(a);
        }
        public Grid GetGrid()
        {
            Grid grid = new Grid();
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)});
            }
            for (int i = 0; i < layout.GetLength(1); i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });
            }

            for (int x = 0; x < layout.GetLength(0); x++)
            {
                for (int y = 0; y < layout.GetLength(1); y++)
                {
                    if (layout[x, y] != null)
                    {
                        Grid.SetColumn(layout[x, y], x);
                        Grid.SetRow(layout[x, y], height - y - 1);
                    }
                }
            }

            for (int x = 0; x < layout.GetLength(0); x++)
            {
                for (int y = 0; y < layout.GetLength(1); y++)
                {
                    if (layout[x, y] != null)
                    {
                        grid.Children.Add(layout[x, y]);
                    }
                }
            }

            return grid;
        }
        void setButtonAsActive(keypadButtonControl control)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (layout[i, j] != null)layout[i, j].disableActive();
                }
            }
            control.setAsActive();
            if (buttonInfoPanel != null)
            {
                buttonInfoPanel.setButton(ref control.b, control.name, control.updateFunctionListView);
            }
        }
        internal void updateView()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (layout[i, j] != null) 
                        layout[i, j].updateFunctionListView();
                }
            }
        }
    }
}
