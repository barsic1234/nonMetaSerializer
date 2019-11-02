using System;

namespace nonMetaSerializer.researchAlgorithm
{
    internal abstract class AbstractAction
    {
        protected readonly IPrimitiveFactory primitiveFactory;

        protected AbstractAction(IPrimitiveFactory primitiveFactory)
        {
            this.primitiveFactory = primitiveFactory;
        }

        protected void SplitType(Type objectType)
        {
            if (objectType.IsArray)
            {
                ForArray();
            }
            else if (objectType.IsPrimitive)
            {
                ForPrimitive();
            }
            else if (objectType == typeof(string))
            {
                ForString();
            }
            else
            {
                ForEasyDataObject();
            }
        }
        internal abstract void ForArray();
        internal abstract void ForPrimitive();
        internal abstract void ForString();
        internal abstract void ForEasyDataObject();
    }
}
