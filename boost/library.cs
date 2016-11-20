using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
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

        public void load(string dir)
        {
            if (!Directory.Exists(Path.GetTempPath() + "BOOST_EXTRACT"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "BOOST_EXTRACT");
            }

            fileEntries = Directory.GetFiles(Path.GetTempPath() + "BOOST_EXTRACT");
            foreach (string fileName in fileEntries)
            {
                File.Delete(fileName);
            }

            File.Copy(dir, Path.GetTempPath() + @"BOOST_EXTRACT\LIBRAW");
            File.Move(Path.GetTempPath() + @"BOOST_EXTRACT\LIBRAW", Path.GetTempPath() + @"BOOST_EXTRACT\LIB.zip");

            ZipFile.ExtractToDirectory(Path.GetTempPath() + @"BOOST_EXTRACT\LIB.zip", extractPath);

            File.Delete(Path.GetTempPath() + @"BOOST_EXTRACT\LIB.zip");
            File.Delete(Path.GetTempPath() + @"BOOST_EXTRACT\LIBRAW");
        }

        public bool create(string name)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Librys/" + name + ".boostlibrary"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Librys/" + name);
                ZipFile.CreateFromDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Librys/" + name, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Librys/" + name + ".boostlibrary");

                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Librys/" + name);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool addToLibrary(Bitmap image, string name, string type)
        {
            if (!File.Exists(Path.GetTempPath() + @"BOOST_EXTRACT\" + name + "." + type))
            {
                ImageCodecInfo ImageCodecInfo;
                System.Drawing.Imaging.Encoder _Encoder;
                EncoderParameter EncoderParameter;
                EncoderParameters EncoderParameters;

                ImageCodecInfo = GetEncoderInfo("image/" + type);

                _Encoder = System.Drawing.Imaging.Encoder.Quality;

                EncoderParameters = new EncoderParameters(1);

                EncoderParameter = new EncoderParameter(_Encoder, 100L);
                EncoderParameters.Param[0] = EncoderParameter;
                image.Save(Path.GetTempPath() + @"BOOST_EXTRACT\" + name + "." + type, ImageCodecInfo, EncoderParameters);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void compile(string name)
        {
            if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Librys/" + name + ".boostlibrary"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Librys/" + name + ".boostlibrary");
            }

            ZipFile.CreateFromDirectory(Path.GetTempPath() + "BOOST_EXTRACT", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/boost/Librys/" + name + ".boostlibrary");
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

    }
}
