using System;

namespace nonMetaSerializer.implPrimitive
{
    class ShortPrimitive : IPrimitive
    {
        private readonly int length = 2;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var shortValue = (short)valueField;
            return BitConverter.GetBytes(shortValue);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length).ToArray();
            return BitConverter.ToInt16(bytes, 0);
        }
    }
}
