using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driverClasses
{
    public class action
    {
        public virtual void execute()
        {

        }
        public virtual string getLabel()
        {
            return "none";
        }
        public virtual List<string> getAddInfo()
        {
            return null;
        }
        public virtual action copy()
        {
            return new action();
        }
        public virtual string serialize()
        {
            return "";
        }
    }
}
