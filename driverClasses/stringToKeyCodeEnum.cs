using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driverClasses
{
    public static class stringToKeyCodeEnum
    {
        public static WindowsInput.Native.VirtualKeyCode convertStringToEnum(string input)
        {
            WindowsInput.Native.VirtualKeyCode output;
            Enum.TryParse(input, out output);
            return output;
        }
    }
}
