using Application.DataTransfer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Helpers
{
    public static class SessionManager
    {
        public static void SetObjectAsJson(this ISession session, object value)
        {
            session.SetString("User",JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session)
        {
            var value = session.GetString("User");

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
