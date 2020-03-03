using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace VerbsPracticeApp.SessionData
{
    /// <summary>
    /// helper class to serialize/deserialize data to/from session
    /// </summary>
    public static class SessionConverterHelper
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}