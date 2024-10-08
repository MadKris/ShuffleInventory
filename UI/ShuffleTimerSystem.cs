using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShuffleInventory.Configuration;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShuffleInventory.UI;

[Autoload(Side = ModSide.Client)]
public class ShuffleTimerSystem : ModSystem
{
    private static ShuffleInventoryServerConfig ServerConfig => ModContent.GetInstance<ShuffleInventoryServerConfig>();
    private static ShuffleInventoryClientConfig ClientConfig => ModContent.GetInstance<ShuffleInventoryClientConfig>();
    
    internal ShuffleTimerState TimerState;

    private UserInterface _timer;
    
    public override void Load()
    {
        TimerState = new ShuffleTimerState();
        TimerState.Activate();
        _timer = new UserInterface();
    }

    public override void PostUpdateEverything()
    {
        if (ServerConfig.EnableTimedShuffle && ClientConfig.DisplayTimer && !Main.playerInventory && !Main.player[Main.myPlayer].dead)
        {
            if (_timer.CurrentState == null)
            {
                _timer.SetState(TimerState);
            }
        }
        else
        {
            _timer.SetState(null);
        }
    }

    
    public override void UpdateUI(GameTime gameTime)
    {
        _timer?.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
        if (mouseTextIndex != -1)
        {
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "Shuffle Inventory: Timer layer",
                delegate
                {
                    _timer?.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }
}