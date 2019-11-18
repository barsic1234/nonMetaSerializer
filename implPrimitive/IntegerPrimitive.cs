using System;

namespace nonMetaSerializer.implPrimitive
{
    internal class IntegerPrimitive : IPrimitive
    {
        private readonly int length = 4;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var integer = (int)valueField;
            return BitConverter.GetBytes(integer);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
