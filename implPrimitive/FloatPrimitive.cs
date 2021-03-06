﻿using System;

namespace nonMetaSerializer.implPrimitive
{
    class FloatPrimitive : IPrimitive
    {
        private readonly int length = 4;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var floatValue = (float)valueField;
            return BitConverter.GetBytes(floatValue);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length);
            return BitConverter.ToSingle(bytes, 0);
        }
    }
}
