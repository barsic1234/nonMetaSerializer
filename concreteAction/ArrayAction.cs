using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nonMetaSerializer.concreteAction
{
    internal class ArrayAction : IConcreteAction
    {
        private readonly Type type;
        public ArrayAction(Type type)
        {
            this.type = type;
        }
        object IConcreteAction.Deserialize(StreamExtractorHandler streamExtractor)
        {
            IPrimitive rankPrimitive = primitiveFactory.MakePrimitive(typeof(byte));
            byte rank = (byte)rankPrimitive.GetValueField(StreamExtractor);

            int[] lengths = new int[rank];
            IPrimitive lengthPrimitive = primitiveFactory.MakePrimitive(typeof(ushort));

            object[] objLen = new object[rank];

            for (int dimension = 0; dimension < rank; dimension++)
            {
                objLen[dimension] = lengthPrimitive.GetValueField(StreamExtractor);
                lengths[dimension] = (ushort)objLen[dimension];
            }

            recordObject = Activator.CreateInstance(objectType, objLen);

            var array = (Array)recordObject;

            Type typeArray = array.GetType();
            Type typeElement = typeArray.GetElementType();

            var arrayIndex = new ArrayIterator(lengths);
            for (int i = 0; i < array.Length; i++)
            {
                object elementValue = CreateNewRecordInstance(typeElement);

                int[] indices = arrayIndex.GetNext();
                array.SetValue(elementValue, indices);
            }
        }

        List<byte> IConcreteAction.Serialize(object dataObject)
        {
            throw new NotImplementedException();
        }
    }
}
