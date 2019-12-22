using Nez;
using Nez.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectTestingProject.Scenes
{
    public class SelectScene : Scene
    {
        [Command("load", "Load a new scene with the selected SceneName")]
        public static void LoadRoom(string sceneName)
        {
            try
            {
                Type t = Type.GetType($"EffectTestingProject.Scenes.{sceneName}");
                Core.Scene = (Nez.Scene)Activator.CreateInstance(t);
            }
            catch
            {
                DebugConsole.Instance.Log("could not find scene named " + sceneName);
            }
        }
        public static T GetTfromString<T>(string mystring)
        {
            var foo = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            return (T)(foo.ConvertFromInvariantString(mystring));
        }
    }
}
