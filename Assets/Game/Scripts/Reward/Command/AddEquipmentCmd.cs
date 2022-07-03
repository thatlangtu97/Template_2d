using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class AddEquipmentCmd : Command
{
    [Inject()] public DataEquipmentRewardParameter DataEquipmentRewardParameter { get; set; }

    public override void Execute()
    {
        DataManager.Instance.InventoryDataManager.AddItems(DataEquipmentRewardParameter.afterAddToInventory);
    }
}

public class DataEquipmentRewardParameter
{
    public List<EquipmentData> afterAddToInventory = new List<EquipmentData> ();
    public DataEquipmentRewardParameter(EquipmentData afterAddToInventory)
    {
        this.afterAddToInventory.Add(afterAddToInventory);
    }
    public DataEquipmentRewardParameter(List<EquipmentData> afterAddToInventory)
    {
        this.afterAddToInventory = afterAddToInventory;
    }
}