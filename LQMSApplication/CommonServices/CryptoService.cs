using System;
using System.Text;
using System.Text.Json;

namespace LQMSApplication.CommonServices
{
    public class CryptoService
    {
        public string Encrypt(object data)
        {
            string jsonData = JsonSerializer.Serialize(data);
            byte[] bytes = Encoding.UTF8.GetBytes(jsonData);
            return Convert.ToBase64String(bytes);
        }

        public T Decrypt<T>(string encodedData)
        {
            byte[] bytes = Convert.FromBase64String(encodedData);
            string jsonData = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
