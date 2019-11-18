using System;

namespace nonMetaSerializer.implPrimitive
{
    class LongPrimitive : IPrimitive
    {
        private readonly int length = 8;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var longValue = (long)valueField;
            return BitConverter.GetBytes(longValue);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length).ToArray();
            return BitConverter.ToInt64(bytes, 0);
        }
    }
}
