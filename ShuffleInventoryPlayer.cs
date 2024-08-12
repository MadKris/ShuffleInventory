using System;
using System.Runtime.CompilerServices;
using Terraria.ModLoader;
using ShuffleInventory.Utility;
using Terraria;

namespace ShuffleInventory;

public class ShuffleInventoryPlayer : ModPlayer
{
    private const double TimeLimit = 2000;
    private DateTime _lastShuffleTime = DateTime.UtcNow;
    public override void PostUpdate()
    {
        var time = (DateTime.UtcNow - _lastShuffleTime).TotalMilliseconds;
        if (time < TimeLimit) return;
        ShuffleAccessories();
        ShuffleInventory();
        _lastShuffleTime = DateTime.UtcNow;
    }

    private void ShuffleAccessories()
    {
        const int vanityOffset = 10;
        const int accessoryFirst = 3;
        const int vanityFirst = vanityOffset + accessoryFirst;
        
        var accessoryCount = Player.InitialAccSlotCount + Player.GetAmountOfExtraAccessorySlotsToShow();
        
        var accessoryLast = accessoryFirst + accessoryCount;
        var vanityLast = vanityFirst + accessoryCount;
        
        var shuffledAccessories = this.Player.armor[accessoryFirst..accessoryLast];
        var shuffledVanity = this.Player.armor[vanityFirst..vanityLast];
        
        shuffledAccessories.Shuffle();
        shuffledVanity.Shuffle();
        this.Player.armor.UpdateAllAt(shuffledAccessories, accessoryFirst);
        this.Player.armor.UpdateAllAt(shuffledVanity, accessoryFirst + vanityOffset);
        this.Player.dye.Shuffle();
        
    }

    private void ShuffleInventory()
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
        
        this.Player.inventory.UpdateAllAt(shuffledHotbar, hotbarFirst);
        this.Player.inventory.UpdateAllAt(shuffledInventory, hotbarLast);
        this.Player.inventory.UpdateAllAt(shuffledAmmo, ammoFirst);
        this.Player.inventory.UpdateAllAt(shuffledCoins, coinsFirst);
    }
}