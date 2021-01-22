using nonMetaSerializer.actionContainers;

namespace nonMetaSerializer
{
    public static class BytesStream //основной класс для использования функций библиотеки
    {

        public static byte[] Serialize(object dataObject)
        {
            var serializer = new Serialize();
            return serializer.StreamRecord(dataObject);
        }

        public static T Deserialize<T>(byte[] representBytes)
        {
            var deserializer = new Deserialize(representBytes);
            return (T)deserializer.ObjectRecord(typeof(T));
        }
    }
}
