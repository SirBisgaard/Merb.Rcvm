using System;
using System.Text;
using Newtonsoft.Json;

namespace Merb.Rcvm.VehicleService.Domain.Queue
{
    public static class ObjectSerialize
    {
        public static byte[] Serialize(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var json = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(json);
        }

        public static object DeSerialize(this byte[] arrBytes, Type type)
        {
            var json = Encoding.UTF8.GetString(arrBytes);
            return JsonConvert.DeserializeObject(json, type);
        }

        public static string DeSerializeText(this byte[] arrBytes)
        {
            return Encoding.UTF8.GetString(arrBytes);
        }
    }
}