using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShuffleInventory.Configuration;

public class ShuffleInventoryClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    
    [Header("$Mods.ShuffleInventory.Configs.ShuffleInventoryServerConfig.UIHeader")] 
    [DefaultValue(true)]
    public bool DisplayTimer;

    [DefaultValue(0)]
    [Range(0, 120)]
    public int DisplayTimerOffsetX;
    [DefaultValue(0)]
    [Range(0, 120)]
    public int DisplayTimerOffsetY;

}