using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AutoBackScripts : View
{
    [Inject] public PopupManager popupManager { get; set; }
    
    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Back panel;
            popupManager.ForceBackPopup();
        }

//        if (Gamepad.current != null)
//        {
//            if (Gamepad.current.leftShoulder.wasPressedThisFrame || Gamepad.current.leftTrigger.wasPressedThisFrame)
//            {
//                popupManager.BackPanel();
//            }
//        }
    }
}
