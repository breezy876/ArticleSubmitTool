using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace ArticleSubmitTool.Shared.Helpers
{
    public static class BinarySerializer
    {
        public static byte[] Serialize<T>(T data)
        {
            var stream = new MemoryStream();
            ProtoBuf.Serializer.Serialize<T>(stream, data);
            return stream.ToArray();
        }

        public static T Deserialize<T>(byte[] data)
        {
            var stream = new MemoryStream(data);
            var obj = Serializer.Deserialize<T>(stream);
            return obj;
        }


    }
}
