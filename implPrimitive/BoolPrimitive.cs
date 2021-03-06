﻿using System;

namespace nonMetaSerializer.implPrimitive
{
    internal class BoolPrimitive : IPrimitive
    {
        private readonly int length = 1;
        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var booleanValue = (bool)valueField;
            return BitConverter.GetBytes(booleanValue);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length);
            return BitConverter.ToBoolean(bytes, 0);
        }
    }
}
