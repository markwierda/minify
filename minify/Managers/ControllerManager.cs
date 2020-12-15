using minify.View;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Controls;
using minify.DAL;
using minify.Interfaces;

namespace minify.Managers
{
    public static class ControllerManager
    {
        public static Dictionary<string, IController> Controllers { get; set; }

        public static void Initialize()
        {
            Controllers = new Dictionary<string, IController>();
        }

        public static T Get<T>() where T : IController
        {
            return (T)Get(typeof(T).Name);
        }

        public static IController Get(string controllerName)
        {
            return Controllers[controllerName] ?? null;
        }

        public static void AddRange(params IController[] controllers)
        {
            foreach (IController c in controllers)
            {
                Add(c);
            }
        }

        public static void Add(IController controller)
        {
            Controllers.TryAdd(controller.GetType().Name, controller);
        }
    }
}
