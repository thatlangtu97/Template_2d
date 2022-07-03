using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentConfigCollection", menuName = "Data/EquipmentCollection")]
public class EquipmentConfigCollection : ScriptableObject
{
    public List<EquipmentConfig> weaponCollection;
    public List<EquipmentConfig> armorCollection;
    public List<EquipmentConfig> ringCollection;
    public List<EquipmentConfig> charmCollection;
    public List<ColorRarity> colorRarity;
    public List<BackGroundRarity> backgroundRarity;
    public Dictionary<int, EquipmentConfig> cacheConfig = new Dictionary<int, EquipmentConfig>();
    
}
[System.Serializable]
public class ColorRarity
{
    public Rarity rarity;
    public Color color;
}
[System.Serializable]
public class BackGroundRarity
{
    public Rarity rarity;
    public Sprite background;
}