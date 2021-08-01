using System;
using System.Collections.Generic;
using System.Text;
using JsonCLR.Json;
using System.Linq;

namespace ProjetoSQLCLR
{
    public static class JsonConverter
    {
        private const string type = "__type";

        public static string Serializer(object obj)
        {
            var settings = new JsonWriterSettings();
            settings.PrettyPrint = true;
            settings.Tab = "  ";

            var sb = new StringBuilder();
            var wr = new JsonWriter(sb, settings);
            wr.Write(obj);
            return sb.ToString();
        }

        public static T Deserialize<T>(string json) where T : class
        {
            var settings = new JsonReaderSettings();
            settings.AllowUnquotedObjectKeys = true;
            settings.AllowNullValueTypes = true;
            settings.TypeHintName = type;
            var rd = new JsonReader(json, settings);
            return rd.Deserialize<T>();
        }

        public static List<T> DeserializeList<T>(string json) where T : class
        {
            var settings = new JsonReaderSettings();
            settings.AllowUnquotedObjectKeys = true;
            settings.AllowNullValueTypes = true;
            settings.TypeHintName = type;
            var rd = new JsonReader(json, settings);
            var obj = rd.Deserialize(typeof(List<T>)) as object[];
            return obj.Select(x => x as T).ToList();
        }
    }
}
