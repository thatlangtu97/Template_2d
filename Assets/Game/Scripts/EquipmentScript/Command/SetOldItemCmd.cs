using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class SetOldItemCmd : Command
{
    [Inject] public EquipmentData equipmentData { get; set; }

    [Inject] public SetOldItemSuccessSignal SetOldItemSuccessSignal { get; set; }

    public override void Execute()
    {
        DataManager.Instance.InventoryDataManager.SetOldItem(equipmentData);
        SetOldItemSuccessSignal.Dispatch(equipmentData);
    }
}
