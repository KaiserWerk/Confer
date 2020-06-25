using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GUI.Helper
{
    public class Messenger
    {
        private static Dictionary<Type, Action<object>> registrants = new Dictionary<Type, Action<object>>();

        public static void Send<T>(T obj) where T : class
        {
            foreach (var item in registrants)
            {
                if (item.Key == typeof(T))
                {
                    item.Value(obj);
                    //break; // remove this to allow multiple recipients
                }
            }
        }

        public static void Register<T>(Action<object> act) where T : class
        {
            registrants.Add(typeof(T), act);
            Debug.WriteLine("registrant added");
        }
    }
}
