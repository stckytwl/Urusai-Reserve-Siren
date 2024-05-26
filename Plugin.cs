using System;
using System.Reflection;
using Aki.Common.Utils;
using BepInEx;
using BepInEx.Configuration;

namespace stckytwl.UrusaiRen;

[BepInPlugin("com.stckytwl.urusai-ren", "stckytwl.Urusai-ren", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static string Directory;
    public static ConfigEntry<float> PluginVolume;

    private void Awake()
    {
        Directory = Assembly.GetExecutingAssembly().Location.GetDirectory() + @"\";
        PluginVolume = Config.Bind("", "Reserve siren volume", 100f, new ConfigDescription("", new AcceptableValueRange<float>(0f, 100f)));
        
        new ChangeReserveSirenVolumePatch().Enable();
    }
}