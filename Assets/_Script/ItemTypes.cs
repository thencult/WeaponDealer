using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public sealed class ItemTypes
{
    // Added some library for the diffrent possible attributes in Items
    public enum Property
    {
        Fire,
        Cold,
        Poison,
        Electro,
        Gilded
    }
    // Base damage according to apect
    public static Dictionary<ItemTypes.Property, float> PropertyBaseDamage = new Dictionary<ItemTypes.Property, float>
    {
        {ItemTypes.Property.Fire, 12f},
        {ItemTypes.Property.Cold, 8f},
        {ItemTypes.Property.Electro, 8f},
        {ItemTypes.Property.Poison, 5f},
        {ItemTypes.Property.Gilded, 5f}
    };
    // Damage modifiers according to aspect type
    public static Dictionary<ItemTypes.Property, float> PropertyDamageModifiers = new Dictionary<ItemTypes.Property, float>
    {
        {ItemTypes.Property.Fire, 1.1f},
        {ItemTypes.Property.Cold, 1.2f},
        {ItemTypes.Property.Electro, 1.2f},
        {ItemTypes.Property.Poison, 1.5f},
        {ItemTypes.Property.Gilded, 1f}
    };
    // Base money to be paid according to type
    public static Dictionary<ItemTypes.Property, float> PropertyBasePrice = new Dictionary<ItemTypes.Property, float>()
    {
        {ItemTypes.Property.Fire, 5f},
        {ItemTypes.Property.Cold, 5f},
        {ItemTypes.Property.Electro, 5f},
        {ItemTypes.Property.Poison, 7f},
        {ItemTypes.Property.Gilded, 10f}
    };
    // Money modifier according to type
    public static Dictionary<ItemTypes.Property, float> PropertyPriceModifiers = new Dictionary<ItemTypes.Property, float>()
    {
        {ItemTypes.Property.Fire, 1.05f},
        {ItemTypes.Property.Cold, 1.05f},
        {ItemTypes.Property.Electro, 1.05f},
        {ItemTypes.Property.Poison, 1.02f},
        {ItemTypes.Property.Gilded, 1.2f}
    };
    public enum Material
    {
        Wood,
        Metal,
        Bone,
        Stone,
        MCrystall
    };

    public static Dictionary<ItemTypes.Material, float> MaterialBaseDamage = new Dictionary<ItemTypes.Material, float>
    {
        {ItemTypes.Material.Wood, 3f},
        {ItemTypes.Material.Metal, 5f},
        {ItemTypes.Material.Bone, 4f},
        {ItemTypes.Material.Stone, 4f},
        {ItemTypes.Material.MCrystall, 3f}
    };
    // Damage modifiers according to aspect type
    public static Dictionary<ItemTypes.Material, float> MaterialDamageModifiers = new Dictionary<ItemTypes.Material, float>
    {
        {ItemTypes.Material.Wood, 1.1f},
        {ItemTypes.Material.Metal, 1.2f},
        {ItemTypes.Material.Bone, 1.2f},
        {ItemTypes.Material.Stone, 1.2f},
        {ItemTypes.Material.MCrystall, 1.3f}
    };
    // Base money to be paid according to type
    public static Dictionary<ItemTypes.Material, float> MaterialBasePrice = new Dictionary<ItemTypes.Material, float>()
    {
        {ItemTypes.Material.Wood, 1f},
        {ItemTypes.Material.Metal, 3f},
        {ItemTypes.Material.Bone, 2f},
        {ItemTypes.Material.Stone, 2f},
        {ItemTypes.Material.MCrystall, 3f}
    };
    // Money modifier according to type
    public static Dictionary<ItemTypes.Material, float> MaterialPriceModifiers = new Dictionary<ItemTypes.Material, float>()
    {
        {ItemTypes.Material.Wood, 1.1f},
        {ItemTypes.Material.Metal, 1.2f},
        {ItemTypes.Material.Bone, 1.2f},
        {ItemTypes.Material.Stone, 1.2f},
        {ItemTypes.Material.MCrystall, 1.3f}
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
