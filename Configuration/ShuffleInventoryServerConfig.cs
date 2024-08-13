using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShuffleInventory.Configuration;

internal class ShuffleInventoryServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;
    
    [Header("$Mods.ShuffleInventory.Configs.ShuffleInventoryServerConfig.GeneralHeader")]
    [DefaultValue(true)] public bool EnableTimedShuffle;
    
    [Range(100, 3600000)][DefaultValue(2000)] public int TimeLimit;
    [Header("$Mods.ShuffleInventory.Configs.ShuffleInventoryServerConfig.ShuffleVariantsHeader")]
    [DefaultValue(true)] public bool ShuffleAccessories;
    [DefaultValue(true)] public bool ShuffleHotbar;
    [DefaultValue(true)] public bool ShuffleInventory;
    [DefaultValue(true)] public bool ShuffleAmmo;
    [DefaultValue(true)] public bool ShuffleCoins;
    [DefaultValue(true)] public bool ShuffleDyes;
}