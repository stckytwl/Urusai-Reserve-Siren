using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using HarmonyLib;

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

    public static int PlayAmount;
    
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(InteractiveSubscriber), "PlaySounds");
    }

    [PatchPostfix]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static void PatchPostfix(InteractiveSubscriber __instance, EDoorState state)
    {
        if (SirenObjectNames.All(x => x != __instance.name) || __instance.gameObject.transform.childCount < 1)
        {
            Utils.Logger.LogDebug($"Skipping object \"{__instance.name}\" due to not being a siren object or has no children.");
            return;
        }
        
        if ((state != EDoorState.Open) == (state != EDoorState.Shut))
        {
            Utils.Logger.LogDebug($"Skipping object \"{__instance.name}\" due to door state being {state}, not Open or Shut.");
            return;
        }

        switch (state)
        {
            case EDoorState.Open:
                PlayAmount = Plugin.StartPlayAmount.Value;
                break;
            case EDoorState.Shut:
                PlayAmount = Plugin.EndPlayAmount.Value;
                break;
            default: // Shouldn't happen.
                PlayAmount = 1;
                Utils.Logger.LogError($"{__instance.name} is of state {state} when it shouldn't!");
                break;
        }
        
        __instance.gameObject.AddComponent<SirenController>();
    }
}