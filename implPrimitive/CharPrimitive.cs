using System;

namespace nonMetaSerializer.implPrimitive
{
    class CharPrimitive : IPrimitive
    {
        private readonly int length = 2;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var charValue = (char)valueField;
            return BitConverter.GetBytes(charValue);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length);
            return BitConverter.ToChar(bytes, 0);
        }
    }
}
