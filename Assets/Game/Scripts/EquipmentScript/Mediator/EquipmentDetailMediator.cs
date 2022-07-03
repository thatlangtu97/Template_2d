using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class EquipmentDetailMediator : Mediator
{
    [Inject] public EquipmentDetailView View { get; set; }
    [Inject] public LevelUpGearSuccessSignal LevelUpGearSuccessSignal { get; set; }

    [Inject] public SellGearSuccessSignal SellGearSuccessSignal { get; set; }

    public override void OnRegister()
    {
        LevelUpGearSuccessSignal.AddListener(View.ReShow);
        SellGearSuccessSignal.AddListener(View.CheckSell);
    }

    public override void OnRemove()
    {
        LevelUpGearSuccessSignal.RemoveListener(View.ReShow);
        SellGearSuccessSignal.RemoveListener(View.CheckSell);
    }

    private void OnDestroy()
    {
        OnRemove();
    }
}
