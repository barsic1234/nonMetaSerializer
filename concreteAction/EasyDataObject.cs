using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using nonMetaSerializer.implPrimitive;

namespace nonMetaSerializer.concreteAction
{
    internal class EasyDataObject : IConcreteAction
    {
        private readonly Type type;

        public EasyDataObject(Type type)
        {
            this.type = type;
        }

        object IConcreteAction.Deserialize(StreamExtractorHandler streamExtractor)
        {
            object recordObject = Activator.CreateInstance(type);

            FieldInfo[] fieldInfos = type.GetFields();
            foreach (FieldInfo field in fieldInfos)
            {
                IConcreteAction action = ActionFactory.MakeAction(field.FieldType);
                object fieldValue = action.Deserialize(streamExtractor);
                field.SetValue(recordObject, fieldValue);
            }

            return recordObject;
        }

        List<byte> IConcreteAction.Serialize(object dataObject)
        {
            var resultStream = new List<byte>();

            FieldInfo[] fieldInfos = type.GetFields();
            foreach (FieldInfo field in fieldInfos)
            {
                object fieldObject = field.GetValue(dataObject);
                IConcreteAction action = ActionFactory.MakeAction(field.FieldType);
                List<byte> representBytes = action.Serialize(fieldObject);
                resultStream.AddRange(representBytes);
            }

            return resultStream;
        }
    }
}
