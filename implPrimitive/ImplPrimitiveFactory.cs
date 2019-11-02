using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nonMetaSerializer.researchAlgorithm;
using nonMetaSerializer.errors;

namespace nonMetaSerializer.implPrimitive
{
    class ImplPrimitiveFactory : IPrimitiveFactory
    {
        IPrimitive IPrimitiveFactory.MakePrimitive(Type typeField)
        {
            var typeCode = Type.GetTypeCode(typeField);

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return new BoolPrimitive();
                case TypeCode.Byte:
                    return new BytePrimitive();
                case TypeCode.Char:
                    return new CharPrimitive();
                case TypeCode.Double:
                    return new DoublePrimitive();
                case TypeCode.Single:
                    return new FloatPrimitive();
                case TypeCode.Int32:
                    return new IntegerPrimitive();
                case TypeCode.Int64:
                    return new LongPrimitive();
                case TypeCode.Int16:
                    return new ShortPrimitive();
                case TypeCode.UInt32:
                    return new UintPrimitive();
                case TypeCode.UInt64:
                    return new UlongPrimitive();
                case TypeCode.UInt16:
                    return new UshortPrimitive();
                default:
                    throw new NonMetaSerializerException(ErrorCode.UNASSIGNED_PRIMIRIVE, typeField.ToString());
            }
        }
    }
}
