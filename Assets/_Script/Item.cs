using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Item : MonoBehaviour
{
    private string Name;
    private string Description;
    private List<ItemTypes.Type> Type;
    private ItemTypes.Rarity Rarity;
    private float AttackDamage;

    public static float CalculateAttack(ItemTypes.Rarity rarity, List<ItemTypes.Type> types)
    {
        float sum = 0;
        float modifiersSum = 0;
        for (int i = 0; i < types.Count - 1; i++)
        {
            sum += ItemTypes.TypeBaseDamage.GetValueOrDefault(types[i]);
            modifiersSum += ItemTypes.TypeDamageModifiers.GetValueOrDefault(types[i]);
        }
        sum += ItemTypes.AdditiveRarityDamage.GetValueOrDefault(rarity);
        float result = sum * modifiersSum;
        return result;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AttackDamage = CalculateAttack(Rarity, Type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
