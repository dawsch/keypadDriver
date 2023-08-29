using driverClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driverClasses
{
    internal static class saver
    {
        internal static string save(List<List<Button>> buttons)
        {
            string result = "";
            for (int i = 0; i < buttons.Count; i++)
            {
                result += "layer" + i.ToString() + " {\n";
                for (int o = 0; o < buttons[i].Count; o++)
                {
                    string bString = "button" + o.ToString() + " {" + buttons[i][o].serialize() + "}\n";
                    result += bString;
                }
                result += "}\n";
            }

            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string file = exePath + "config2.txt";

            StreamWriter writer = new StreamWriter(file);
            writer.Write(result);
            writer.Close();

            return result;
        }
    }
}
