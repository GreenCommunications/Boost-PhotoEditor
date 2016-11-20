using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boost
{
    class DataNodes
    {
        private string node;

        public DataNodes(string name)
        {
            node = name;
        }

        public void create(string value)
        {
            TextWriter tw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Runtime/" + node + ".dat", true);
            tw.WriteLine(value);
            tw.Close();
        }

        public string getNodeValue()
        {
            return File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Runtime/" + node + ".dat");
        }
    }
}
