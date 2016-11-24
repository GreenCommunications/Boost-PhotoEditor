using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Drawing;

namespace boost
{
    class Plugin
    {
        string path;

        public Plugin(string path_)
        {
            path = path_;
        }

        Bitmap result;
        public object RunImgUpdate(Bitmap current)
        {
            var DLL = Assembly.LoadFile(path);
            foreach (Type type in DLL.GetExportedTypes())
            {
                dynamic c = Activator.CreateInstance(type);
                result = c.UpdateImage(current);
            }

            if(result != null)
            {
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
