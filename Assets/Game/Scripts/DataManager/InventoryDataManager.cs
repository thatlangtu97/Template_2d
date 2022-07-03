using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager : IObjectDataManager
{
    const string INVENTORY_DATA_FILE = "InventoryData.binary";
    private InventoryData inventoryData = new InventoryData();
    private const string identityKey = "IdentityKeyEquipment";
    public void DeleteData()
    {
        DataManager.DeleteData(INVENTORY_DATA_FILE);
        inventoryData = new InventoryData();
    }

    public void LoadData()
    {
        try
        {
            inventoryData = DataManager.LoadData<InventoryData>(INVENTORY_DATA_FILE);
            if (inventoryData == null)
            {
                inventoryData = new InventoryData();
                SaveData();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("CurrencyData Error: " + e);
            inventoryData = new InventoryData();
            SaveData();
            return;
        }
    }


    public void SaveData()
    {
        DataManager.SaveData(inventoryData, INVENTORY_DATA_FILE);
    }
    public int GenerateIdentityEquipment()
    {
        int returnValue = PlayerPrefs.GetInt(identityKey, 0);
        returnValue++;
        PlayerPrefs.SetInt(identityKey, returnValue);
        PlayerPrefs.Save();
        return returnValue;
    }
    
    public List<EquipmentData> GetAllEquipmentBySlot(GearSlot gearSlot)
    {
        if (inventoryData.EquipmentDicBySlot.ContainsKey(gearSlot))
        {
            return inventoryData.EquipmentDicBySlot[gearSlot];
        }
        return new List<EquipmentData>();
    }
    public void CraftItem(EquipmentData equipmentData)
    {
        List<EquipmentData> tempList = inventoryData.EquipmentDicBySlot[equipmentData.gearSlot];
        foreach (EquipmentData tempData in tempList)
        {
            if (tempData.id == equipmentData.id)
            {
                tempData.rarity = (Rarity)Mathf.Clamp((int)tempData.rarity + 1, (int)Rarity.common, (int)Rarity.heroic);
                SaveData();
                return;
            }
        }
    }
    public void RemoveItem(EquipmentData equipmentData)
    {
        List<EquipmentData> tempList = inventoryData.EquipmentDicBySlot[equipmentData.gearSlot];
        foreach (EquipmentData tempData in tempList)
        {
            if (tempData.id == equipmentData.id)
            {
                inventoryData.EquipmentDicBySlot[tempData.gearSlot].Remove(tempData);
                SaveData();
                return;
            }
        }
    }
    public void RemoveItem(List<EquipmentData>  equipmentDatas)
    {
        Dictionary<GearSlot , List<EquipmentData>> dictionary = new Dictionary<GearSlot, List<EquipmentData>>();

        foreach (var equipment in equipmentDatas)
        {
            GearSlot key = equipment.gearSlot;
            if (dictionary.ContainsKey(key))
            {
                dictionary[key].Add(equipment);
            }
            else
            {
                List<EquipmentData> listAdd = new List<EquipmentData>();
                listAdd.Add(equipment);
                dictionary.Add(key,listAdd);
            }
        }

        foreach (GearSlot key in dictionary.Keys)
        {
            List<EquipmentData> tempDatas = dictionary[key];
            foreach (EquipmentData tempData in tempDatas)
            {
                inventoryData.EquipmentDicBySlot[tempData.gearSlot].Remove(tempData);
            }
        }
        SaveData();
        
        
    }
    public void AddItems(List<EquipmentData> equipmentDatas)
    {
        for (int i = 0; i < equipmentDatas.Count; i++)
        {
            EquipmentData newEquipment = equipmentDatas[i];
            newEquipment.id = GenerateIdentityEquipment();
            newEquipment.isNewItem = true;
            if (inventoryData.EquipmentDicBySlot.ContainsKey(newEquipment.gearSlot))
            {
                inventoryData.EquipmentDicBySlot[newEquipment.gearSlot].Add(newEquipment);
            }
            else
            {
                List<EquipmentData> tempList = new List<EquipmentData>();
                tempList.Add(newEquipment);
                inventoryData.EquipmentDicBySlot.Add(newEquipment.gearSlot, tempList);
            }
        }
        SaveData();
    }
    public void AddItem(EquipmentData equipmentData)
    {
        equipmentData.id = GenerateIdentityEquipment();
        equipmentData.isNewItem = true;
        if (inventoryData.EquipmentDicBySlot.ContainsKey(equipmentData.gearSlot))
        {
            inventoryData.EquipmentDicBySlot[equipmentData.gearSlot].Add(equipmentData);
        }
        else
        {
            List<EquipmentData> tempList = new List<EquipmentData>();
            tempList.Add(equipmentData);
            inventoryData.EquipmentDicBySlot.Add(equipmentData.gearSlot, tempList);
        }
        SaveData();
    }

    public void LevelUpItem(EquipmentData equipmentData)
    {
        equipmentData.level += 1;
        SaveData();
    }

    public void SetOldItem(EquipmentData equipmentData)
    {
        equipmentData.isNewItem = false;
        SaveData();
    }
}
[Serializable]
public class InventoryData : DataObject
{
    public Dictionary<GearSlot, List<EquipmentData>> EquipmentDicBySlot = new Dictionary<GearSlot, List<EquipmentData>>();
}
[Serializable]
public class EquipmentData
{
    public int id;
    public int idConfig;
    public int idOfHero;
    public GearSlot gearSlot;
    public Rarity rarity;
    public int level;
    public StatData mainStatData;
    public bool isNewItem;
}
[Serializable]
public class StatData
{
    public StatType statType;
    public float baseValue;
}

