using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class LevelUpGearCmd : Command
{
    [Inject] public EquipmentData equipmentData { get; set; }
    [Inject] public LevelUpGearSuccessSignal LevelUpGearSuccessSignal { get; set; }

    public override void Execute()
    {
        EquipmentLogic.LevelUpGear(equipmentData);
        LevelUpGearSuccessSignal.Dispatch(equipmentData);
    }
}
