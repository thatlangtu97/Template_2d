using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class CraftEquipmentMediator : Mediator
{
    [Inject] public CraftEquipmentView View { get; set; }
    [Inject] public NotificationPanelCraftSignal NotificationPanelCraftSignal { get; set; }
    
    public override void OnRegister()
    {
        NotificationPanelCraftSignal.AddListener(View.Reshow);
    }

    public override void OnRemove()
    {
        NotificationPanelCraftSignal.RemoveListener(View.Reshow);
    }

    private void OnDestroy()
    {
        OnRemove();
    }
}
