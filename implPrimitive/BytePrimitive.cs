using System.Collections.Generic;

namespace nonMetaSerializer.implPrimitive
{
    class BytePrimitive : IPrimitive
    {
        private readonly int length = 1;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var byteValue = (byte)valueField;
            var bytes = new byte[] { byteValue };
            return bytes;
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            List<byte> bytes = streamExtractor(length);
            byte singleByte = bytes[0];
            return singleByte;
        }
    }
}
