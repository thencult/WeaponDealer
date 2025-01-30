using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public sealed class ItemTypes
{
    // Added some library for the diffrent possible attributes in Items
    public enum Type
    {
        Slash,
        Impact,
        Puncture,
        Fire,
        Cold,
        Electro,
        Void
    }
    // Base damage according to apect
    public static Dictionary<ItemTypes.Type, float> TypeBaseDamage = new Dictionary<ItemTypes.Type, float>
    {
        {ItemTypes.Type.Slash, 3f},
        {ItemTypes.Type.Impact, 3f},
        {ItemTypes.Type.Puncture, 4f},
        {ItemTypes.Type.Fire, 5f},
        {ItemTypes.Type.Cold, 5f},
        {ItemTypes.Type.Electro, 5f},
        {ItemTypes.Type.Void, 7f}
    };
    // Damage modifiers according to aspect type
    public static Dictionary<ItemTypes.Type, float> TypeDamageModifiers = new Dictionary<ItemTypes.Type, float>
    {
        {ItemTypes.Type.Slash, 1.4f},
        {ItemTypes.Type.Impact, 1.4f},
        {ItemTypes.Type.Puncture, 1.4f},
        {ItemTypes.Type.Fire, 1.3f},
        {ItemTypes.Type.Cold, 1.3f},
        {ItemTypes.Type.Electro, 1.3f},
        {ItemTypes.Type.Void, 1.15f}
    };
    // Base money to be paid according to type
    public static Dictionary<ItemTypes.Type, float> TypeBasePrice = new Dictionary<ItemTypes.Type, float>()
    {
        {ItemTypes.Type.Slash, 10f},
        {ItemTypes.Type.Impact, 10f},
        {ItemTypes.Type.Puncture, 10f},
        {ItemTypes.Type.Fire, 15f},
        {ItemTypes.Type.Cold, 15f},
        {ItemTypes.Type.Electro, 15f},
        {ItemTypes.Type.Void, 20f}
    };
    // Money modifier according to type
    public static Dictionary<ItemTypes.Type, float> TypePriceModifiers = new Dictionary<ItemTypes.Type, float>()
    {
        {ItemTypes.Type.Slash, 1.3f},
        {ItemTypes.Type.Impact, 1.3f},
        {ItemTypes.Type.Puncture, 1.3f},
        {ItemTypes.Type.Fire, 1.2f},
        {ItemTypes.Type.Cold, 1.2f},
        {ItemTypes.Type.Electro, 1.2f},
        {ItemTypes.Type.Void, 1.15f}
    };
    // Possible rarities of the item
    public enum Rarity {
        Normal,
        Enhanced,
        Perfected
    }
    // Additive damage according to rarity
    public static Dictionary<ItemTypes.Rarity, float> AdditiveRarityDamage = new Dictionary<ItemTypes.Rarity, float>()
    {
        {ItemTypes.Rarity.Normal, 0 },
        {ItemTypes.Rarity.Enhanced, 5 },
        {ItemTypes.Rarity.Perfected, 10 }
    };
    // Additive money according to rarity
    public static Dictionary<ItemTypes.Rarity, float> AdditiveRarityPrice = new Dictionary<ItemTypes.Rarity, float>()
    {
        {ItemTypes.Rarity.Normal, 5 },
        {ItemTypes.Rarity.Enhanced, 10 },
        {ItemTypes.Rarity.Perfected, 15 }
    };
}
