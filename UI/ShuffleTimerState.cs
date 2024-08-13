using System;
using Microsoft.Xna.Framework;
using ShuffleInventory.Configuration;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShuffleInventory.UI;

public class ShuffleTimerState : UIState
{
    private static ShuffleInventoryServerConfig ServerConfig => ModContent.GetInstance<ShuffleInventoryServerConfig>();
    public UIText timerText;

    public override void OnInitialize()
    {
        timerText = new UIText("Shuffling in: ");
        Append(timerText);
        this.Left.Pixels = 32 * Main.inventoryScale;
        this.Top.Pixels = 8 + 144 * Main.inventoryScale;
    }

    public override void Update(GameTime gameTime)
    {
        var player = Main.player[Main.myPlayer].GetModPlayer<ShuffleInventoryPlayer>();
        base.Update(gameTime);
        if (player == null)
        {
            timerText.SetText("");
            return;
        }
        timerText.SetText($"Shuffling in: {player.TimeLeft:mm\\:ss\\:fff}");
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