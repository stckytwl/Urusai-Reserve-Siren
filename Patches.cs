using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using UnityEngine;

namespace stckytwl.UrusaiRen;

public class ChangeReserveSirenVolumePatch : ModulePatch
{
    private static readonly List<string> SirenObjectNames =
    [
        "Siren_01",
        "Siren_02",
        "Siren_03",
        "Siren_04",
        "Siren_05",
    ];
    
    protected override MethodBase GetTargetMethod()
    {
        return typeof(InteractiveSubscriber).GetMethod("PlaySounds", BindingFlags.Instance | BindingFlags.Public);
    }

    [PatchPostfix]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static void PatchPostfix(InteractiveSubscriber __instance)
    {
        if (SirenObjectNames.All(x => x != __instance.name) || __instance.gameObject.transform.childCount < 1)
        {
            Logger.LogInfo($"Skipping object \"{__instance.name}\" due to not being a siren object or has no children");
            return;
        }

        if (__instance.Sounds is null)
        {
            Logger.LogError($"Why tf does {__instance.name} have a null sounds field???");
            return;
        }
        
        __instance.gameObject.AddComponent<SirenController>();
    }
}