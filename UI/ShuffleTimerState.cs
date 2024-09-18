using System;
using System.Linq;
using Humanizer;
using Microsoft.Xna.Framework;
using ShuffleInventory.Configuration;
using Steamworks;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerIII;

namespace ShuffleInventory.UI;

public class ShuffleTimerState : UIState
{
    private static ShuffleInventoryServerConfig ServerConfig => ModContent.GetInstance<ShuffleInventoryServerConfig>();
    private static ShuffleInventoryClientConfig ClientConfig => ModContent.GetInstance<ShuffleInventoryClientConfig>();

    private const string ShuffleText = "Inventory shuffle in: ";
    public UIText timerText;
    private float defaultTextWidth;

    public override void OnInitialize()
    {
        
        timerText = new UIText(ShuffleText);
        defaultTextWidth = timerText.GetOuterDimensions().Width;
        Top.Pixels = Main.screenHeight - 4 * Main.inventoryScale - timerText.GetOuterDimensions().Height;
        Append(timerText);
    }

    public override void Update(GameTime gameTime)
    {
        Top.Pixels = Main.screenHeight - 12 * Main.inventoryScale - timerText.GetOuterDimensions().Height - ClientConfig.DisplayTimerOffsetY;
        Left.Pixels = Main.screenWidth - 32 * Main.inventoryScale - timerText.GetOuterDimensions().Width - ClientConfig.DisplayTimerOffsetX;
        
        var player = Main.player[Main.myPlayer].GetModPlayer<ShuffleInventoryPlayer>();
        base.Update(gameTime);
        if (player == null)
        {
            timerText.SetText("");
            return;
        }
        timerText.SetText($"{ShuffleText}{player.TimeLeft:mm\\:ss}");
        var colorInterpolationValue = (float)player.TimeElapsed.TotalMilliseconds / ServerConfig.TimeLimit;
        if (colorInterpolationValue < 0.5f)
        {
            timerText.TextColor = Color.Lerp(Color.Green, Color.Yellow, colorInterpolationValue * 2);
        }
        else
        {
            timerText.TextColor = Color.Lerp(Color.Yellow, Color.Red, (colorInterpolationValue - 0.5f) * 2);
        }
    }
}