﻿using BepInEx;
using BepInEx.Configuration;

namespace stckytwl.UrusaiRen;

[BepInPlugin("com.stckytwl.urusai-ren", "Urusai-ren", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static ConfigEntry<int> StartPlayAmount;
    public static ConfigEntry<int> EndPlayAmount;

    private void Awake()
    {
        Utils.Logger = Logger;
        StartPlayAmount = Config.Bind("", "Siren start loop amount", 3,
            new ConfigDescription("How many times the siren will blare after toggling a switch", null,
                new ConfigurationManagerAttributes { IsAdvanced = true, Order = 1 }));
        EndPlayAmount = Config.Bind("", "Siren end loop amount", 1,
            new ConfigDescription("How many time the siren will blare when switch expires", null,
                new ConfigurationManagerAttributes { IsAdvanced = true, Order = 0 }));

        new AddSirenControllerPatch().Enable();
    }
}