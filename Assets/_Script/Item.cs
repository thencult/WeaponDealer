using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Item : MonoBehaviour
{
    private string Name;
    private string Description;
    private List<ItemTypes.Type> Types;
    private ItemTypes.Rarity Rarity;
    private float AttackDamage;
    private float Price;

    public void CalculateAttack(ItemTypes.Rarity rarity, List<ItemTypes.Type> types)
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
        AttackDamage = result;
    }
    public void CalculatePrice(ItemTypes.Rarity rarity, List<ItemTypes.Type> types)
    {
        float sum = 0;
        float modifiersSum = 0;
        for (int i = 0; i < types.Count - 1; i++)
        {
            sum += ItemTypes.TypeBasePrice.GetValueOrDefault(types[i]);
            modifiersSum += ItemTypes.TypePriceModifiers.GetValueOrDefault(types[i]);
        }
        sum += ItemTypes.AdditiveRarityPrice.GetValueOrDefault(rarity);
        float result = sum * modifiersSum;
        AttackDamage = result;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CalculateAttack(Rarity, Types);
        CalculatePrice(Rarity, Types);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
