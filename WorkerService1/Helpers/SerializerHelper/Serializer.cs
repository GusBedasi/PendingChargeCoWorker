using Newtonsoft.Json;
using System.Text;

namespace WorkerService1.Helpers.SerializerHelper
{
    public static class Serializer
    {
        public static T Deserialize<T>(byte[] obj)
        {
            var json = Encoding.UTF8.GetString(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static byte[] Serialize<T>(T obj)
        {
            var message = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(message);
        }
    }
}
