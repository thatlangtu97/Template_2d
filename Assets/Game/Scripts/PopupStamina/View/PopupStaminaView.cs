using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupStaminaView : AbsPopupView
{
    
    public override bool EnableBack()
    {
        return true;
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        
    }
}
