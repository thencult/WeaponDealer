using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Item : MonoBehaviour
{
    private string Name;
    private string Description;
    private List<ItemTypes.Property> Properties;
    private List<ItemTypes.Material> Materials;
    private ItemTypes.Rarity Rarity;
    private float AttackDamage;
    private float Price;

    public void CalculateAttack(ItemTypes.Rarity rarity, List<ItemTypes.Property> properties, List<ItemTypes.Material> materials)
    {
        float sum = 0;
        float PropertyModifiersSum = 1;
        float MaterialModifiersSum = 1;
        for (int i = 0; i < properties.Count - 1; i++)
        {
            sum += ItemTypes.PropertyBaseDamage.GetValueOrDefault(properties[i]);
            PropertyModifiersSum *= ItemTypes.PropertyDamageModifiers.GetValueOrDefault(properties[i]);
        }
        for(int i = 0; i <  materials.Count - 1; i++)
        {
            sum += ItemTypes.MaterialBaseDamage.GetValueOrDefault(materials[i]);
            MaterialModifiersSum *= ItemTypes.MaterialDamageModifiers.GetValueOrDefault(materials[i]);
        }
        sum += ItemTypes.AdditiveRarityDamage.GetValueOrDefault(rarity);
        float result = sum * Mathf.Sqrt(MaterialModifiersSum * PropertyModifiersSum);
        AttackDamage = result;
    }
    public void CalculatePrice(ItemTypes.Rarity rarity, List<ItemTypes.Property> properties, List<ItemTypes.Material> materials)
    {
        float sum = 0;
        float PropertyModifiersSum = 1;
        float MaterialModifiersSum = 1;
        for (int i = 0; i < properties.Count - 1; i++)
        {
            sum += ItemTypes.PropertyBasePrice.GetValueOrDefault(properties[i]);
            PropertyModifiersSum *= ItemTypes.PropertyPriceModifiers.GetValueOrDefault(properties[i]);
        }
        for(int i = 0; i < materials.Count - 1; i++)
        {
            sum += ItemTypes.MaterialBasePrice.GetValueOrDefault(materials[i]);
            MaterialModifiersSum *= ItemTypes.MaterialPriceModifiers.GetValueOrDefault(materials[i]);
        }
        sum += ItemTypes.AdditiveRarityPrice.GetValueOrDefault(rarity);
        float result = sum * Mathf.Sqrt(MaterialModifiersSum * PropertyModifiersSum);
        AttackDamage = result;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CalculateAttack(Rarity, Properties, Materials);
        CalculatePrice(Rarity, Properties, Materials);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
