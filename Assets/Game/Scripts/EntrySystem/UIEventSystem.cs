using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventSystem : View
{
    [Inject] public PopupManager popupManager { get; set; }
    public EventSystem eventSystem;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
//        if (!popupManager.eventSystem)
//        {
//            popupManager.eventSystem = eventSystem;
//            DontDestroyOnLoad(this.gameObject);
//        }
    }

}
