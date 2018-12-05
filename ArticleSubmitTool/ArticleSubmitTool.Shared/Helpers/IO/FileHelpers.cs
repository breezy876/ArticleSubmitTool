using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSubmitTool.Shared.Helpers.IO
{
    public class FileHelpers
    {

        public static byte[] ReadBytes(string filename)
        {
            MemoryStream stream = (MemoryStream)Read(filename);
            byte[] buffer = stream.GetBuffer();
            return buffer;
        }

        public static Stream Read(string filename)
        {
            MemoryStream outStream;

            using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {

                outStream = new MemoryStream();
                fileStream.CopyTo(outStream);
            }
            outStream.Position = 0;
            return outStream;
        }

        public static string ReadToEnd(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string content = reader.ReadToEnd();
                    return content;
                }
            }
            catch { return null; }

        }

        public static string[] ReadAllLines(string filename)
        {
            return ReadToEnd(filename).Replace("\n", "").Split('\r');
        }
    }
}
