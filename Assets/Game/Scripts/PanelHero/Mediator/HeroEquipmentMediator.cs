using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class HeroEquipmentMediator : Mediator
{
    [Inject] public  HeroEquipmentView View { get; set; }
    [Inject] public OnViewHeroSignal OnViewHeroSignal { get; set; }
    
    [Inject] public LevelUpGearSuccessSignal LevelUpGearSuccessSignal { get; set; }
    
    [Inject] public SetOldItemSuccessSignal SetOldItemSuccessSignal { get; set; }

    [Inject] public EquipGearSuccessSignal EquipGearSuccessSignal { get; set; }

    public override void OnRegister()
    {
        OnViewHeroSignal.AddListener(View.Show);
        LevelUpGearSuccessSignal.AddListener(View.ReShow);
        SetOldItemSuccessSignal.AddListener(View.ReShow);
        EquipGearSuccessSignal.AddListener(View.EquipGear);
    }

    public override void OnRemove()
    {
        OnViewHeroSignal.RemoveListener(View.Show);
        LevelUpGearSuccessSignal.RemoveListener(View.ReShow);
        SetOldItemSuccessSignal.RemoveListener(View.ReShow);
        EquipGearSuccessSignal.RemoveListener(View.EquipGear);
    }

    private void OnDestroy()
    {
        OnRemove();
    }
}
