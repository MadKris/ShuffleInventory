using System;
using System.Runtime.CompilerServices;
using ShuffleInventory.Configuration;
using Terraria.ModLoader;
using ShuffleInventory.Utility;
using Terraria;

namespace ShuffleInventory;

public class ShuffleInventoryPlayer : ModPlayer
{
    private static ShuffleInventoryServerConfig ServerConfig => ModContent.GetInstance<ShuffleInventoryServerConfig>();

    public TimeSpan TimeElapsed => DateTime.UtcNow - LastShuffleTime;
    
    public TimeSpan TimeLeft => TimeSpan.FromMilliseconds(ServerConfig.TimeLimit - TimeElapsed.TotalMilliseconds);
    
    public DateTime LastShuffleTime { get; private set; } = DateTime.UtcNow;

    public override void PostUpdate()
    {
        if (!ServerConfig.EnableTimedShuffle || TimeElapsed.TotalMilliseconds < ServerConfig.TimeLimit) return;
        ShuffleWholeInventory();
        LastShuffleTime = DateTime.UtcNow;
    }
    public virtual void ShuffleWholeInventory()
    {
        ShuffleAccessories();
        ShuffleInventory(); 
        ShuffleDyes();
    }

    public void ShuffleDyes()
    {
        if (!ServerConfig.ShuffleDyes) return;
        this.Player.dye.Shuffle();
    }

    public void ShuffleAccessories()
    {
        if (!ServerConfig.ShuffleAccessories) return;
        const int vanityOffset = 10;
        const int accessoryFirst = 3;
        var accessoryCount = Player.InitialAccSlotCount + Player.GetAmountOfExtraAccessorySlotsToShow();
        
        var accessoryLast = accessoryFirst + accessoryCount;
        var shuffledAccessories = this.Player.armor[accessoryFirst..accessoryLast];
        shuffledAccessories.Shuffle();
        
        const int vanityFirst = vanityOffset + accessoryFirst;
        var vanityLast = vanityFirst + accessoryCount;
        var shuffledVanity = this.Player.armor[vanityFirst..vanityLast];
        shuffledVanity.Shuffle();
        
        this.Player.armor.UpdateAllAt(shuffledAccessories, accessoryFirst);
        this.Player.armor.UpdateAllAt(shuffledVanity, accessoryFirst + vanityOffset);
    }

    public void ShuffleInventory()
    {
        const int hotbarSize = 10;
        const int hotbarFirst = Main.InventoryItemSlotsStart;
        const int hotbarLast = hotbarFirst + hotbarSize;
        const int inventorySize = Main.InventoryItemSlotsCount - hotbarSize;
        const int inventoryLast = hotbarLast + inventorySize;
        const int ammoFirst = Main.InventoryAmmoSlotsStart;
        const int ammoLast = ammoFirst + Main.InventoryAmmoSlotsCount;
        const int coinsFirst = Main.InventoryCoinSlotsStart;
        const int coinsLast = coinsFirst + Main.InventoryCoinSlotsCount;
        
        var shuffledHotbar = this.Player.inventory[hotbarFirst..hotbarLast];
        var shuffledInventory = this.Player.inventory[hotbarLast..inventoryLast];
        var shuffledAmmo = this.Player.inventory[ammoFirst..ammoLast];
        var shuffledCoins = this.Player.inventory[coinsFirst..coinsLast];
        
        shuffledHotbar.Shuffle();
        shuffledInventory.Shuffle();
        shuffledAmmo.Shuffle();
        shuffledCoins.Shuffle();

        if (ServerConfig.ShuffleHotbar)
        {
            this.Player.inventory.UpdateAllAt(shuffledHotbar, hotbarFirst);
        }
        if (ServerConfig.ShuffleInventory)
        {
            this.Player.inventory.UpdateAllAt(shuffledInventory, hotbarLast);
        }
        if (ServerConfig.ShuffleAmmo)
        {
            this.Player.inventory.UpdateAllAt(shuffledAmmo, ammoFirst);
        }
        if (ServerConfig.ShuffleCoins)
        {
            this.Player.inventory.UpdateAllAt(shuffledCoins, coinsFirst);
        }
    }
}