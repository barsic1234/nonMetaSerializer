using nonMetaSerializer.implPrimitive;
using nonMetaSerializer.implementationAction;

namespace nonMetaSerializer
{
    public static class BytesStream
    {

        public static byte[] Serialize(object dataObject)
        {
            var serializer = new SerializeAction(new ImplPrimitiveFactory());
            return serializer.StreamRecord(dataObject);
        }

        public static T Deserialize<T>(byte[] representBytes)
        {
            var deserializer = new DeserializeAction(new ImplPrimitiveFactory(), representBytes);
            return (T)deserializer.ObjectRecord(typeof(T));
        }
    }
}
