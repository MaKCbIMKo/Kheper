namespace Kheper.Core.Util
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class Serialization
    {
        public static T Clone<T>(T instance)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, instance);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
