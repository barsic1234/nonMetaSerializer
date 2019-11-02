using System;

namespace nonMetaSerializer.researchAlgorithm
{
    interface IPrimitiveFactory
    {
        IPrimitive MakePrimitive(Type typeField);
    }
}
