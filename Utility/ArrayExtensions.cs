using System;

namespace ShuffleInventory.Utility;

public static class ArrayExtensions
{
    public static void Shuffle<T>(this T[] array)
    {
        Random.Shared.Shuffle(array);
    }

    public static void UpdateAllAt<T>(this T[] array, T[] updated, int index) 
    {
        foreach (var item in updated)
        {
            array[index] = item;
            index++;
        }
    }
}