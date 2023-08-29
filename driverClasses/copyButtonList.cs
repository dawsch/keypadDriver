using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using driverClasses;

namespace driverClasses
{
    public static class copyButtonList
    {
        public static List<List<Button>> copy(List<List<Button>> source)
        {
            List<List<Button>> copy = new List<List<Button>>();
            foreach (List<Button> list in source)
            {
                List<Button> listC = new List<Button>();
                foreach(Button button in list)
                {
                    listC.Add(button.copy());
                }
                copy.Add(listC);
            }
            return copy;
        }
    }
}