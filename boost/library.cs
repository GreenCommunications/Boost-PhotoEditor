using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boost
{
    class library
    {
        public static Bitmap[] currentLibrary
        {
            get;
            set;
        }

        string[] fileEntries;

        string extractPath = Path.GetTempPath() + "BOOST_EXTRACT";

        public library(string directory)
        {
            if(!Directory.Exists(Path.GetTempPath() + "BOOST_EXTRACT"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "BOOST_EXTRACT");
            }

            fileEntries = Directory.GetFiles(Path.GetTempPath() + "BOOST_EXTRACT");
            foreach(string fileName in fileEntries)
            {
                File.Delete(fileName);
            }

            ZipFile.ExtractToDirectory(directory, extractPath);
        }
    }
}
