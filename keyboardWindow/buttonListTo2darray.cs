using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using driverClasses;

namespace keyboardWindow
{
    internal static class buttonListTo2darray
    {
        internal static Button[,] convert(int width, int height, List<Button> buttons, List<int[]> disabledButtons = null)
        {
            Button[,] bArray = new Button[width, height];
            int[] subtract = new int[height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (disabledButtons != null)
                    {
                        bool h = false;
                        for (int dB = 0; dB < disabledButtons.Count; dB++)
                        {
                            if (i == disabledButtons[dB][0] && j == disabledButtons[dB][1])
                            {
                                subtract[j]++;
                                h = true;
                            }
                        }
                        if (h)
                            continue;
                    }
                    bArray[i, j] = buttons[i + (3 * j) - subtract[j]];
                }
            }
            return bArray;
        }
    }
}
