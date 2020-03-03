using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace PetGroomingApplication.Services
{
    public static class Helpers
    {
        public static Object ObjectMapper(Object obj1, Object obj2)
        {
            foreach (var field in obj1.GetType().GetProperties())
            {
                var val = obj1.GetType().GetProperty(field.Name).GetValue(obj1, null);
                obj2.GetType().GetProperty(field.Name).SetValue(obj2, val);
            }
            return obj2;
        }
    }
 }