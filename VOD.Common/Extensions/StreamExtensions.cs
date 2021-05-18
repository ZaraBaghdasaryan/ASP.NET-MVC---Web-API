using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.Extensions
{
    public static class StreamExtensions
    {
        public static async Task SerializeToJsonAndWriteAsync<T>(this Stream stream, T objectToWrite, Encoding encoding, int bufferSize, bool leaveOpen, bool resetStream = false) //Opposite of deserialize 
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (stream.CanWrite) throw new NotSupportedException("Can't write to this stream.");
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));

            using (var streamWriter = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
            {
                using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                {
                    var jsonSerializer = new JsonSerializer();
                    jsonSerializer.Serialize(jsonTextWriter, objectToWrite);
                    await jsonTextWriter.FlushAsync(); //Flushes out the leftover streams 

                }

            }
            //After writing set the stream to position 0
            if (resetStream && stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);

        }
    }
}
