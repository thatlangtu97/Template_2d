using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class InventoryMediator : Mediator
{
    [Inject] public InventoryView View { get; set; }
    [Inject] public OnViewHeroSignal OnViewHeroSignal { get; set; }
    
    [Inject] public LevelUpGearSuccessSignal LevelUpGearSuccessSignal { get; set; }
    
    [Inject] public SellGearSuccessSignal SellGearSuccessSignal { get; set; }

    [Inject] public SetOldItemSuccessSignal SetOldItemSuccessSignal { get; set; }
    
    [Inject] public EquipGearSuccessSignal EquipGearSuccessSignal { get; set; }

    [Inject] public NotificationPanelCraftSignal NotificationPanelCraftSignal { get; set; }

    public override void OnRegister()
    {
        OnViewHeroSignal.AddListener(View.ReloadPage);
        LevelUpGearSuccessSignal.AddListener(View.ReShow);
        SellGearSuccessSignal.AddListener(View.ReloadDataRemove);
        SetOldItemSuccessSignal.AddListener(View.ReShow);
        EquipGearSuccessSignal.AddListener(View.EquipGear);
        NotificationPanelCraftSignal.AddListener(View.ReloadPage);
    }

    public override void OnRemove()
    {
        OnViewHeroSignal.RemoveListener(View.ReloadPage);
        LevelUpGearSuccessSignal.RemoveListener(View.ReShow);
        SellGearSuccessSignal.RemoveListener(View.ReloadDataRemove);
        SetOldItemSuccessSignal.RemoveListener(View.ReShow);
        EquipGearSuccessSignal.RemoveListener(View.EquipGear);
        NotificationPanelCraftSignal.RemoveListener(View.ReloadPage);
    }

    private void OnDestroy()
    {
        OnRemove();
    }
}
