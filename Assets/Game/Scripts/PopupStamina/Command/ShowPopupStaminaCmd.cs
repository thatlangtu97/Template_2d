using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowPopupStaminaCmd : AbsShowPopupCmd
{
    public override void Execute()
    {
        PopupStaminaView popupStaminaView = GetInstance<PopupStaminaView>();
        popupStaminaView.ShowPopup(new ParameterPopup());
    }

    public override UILayer GetUiLayer()
    {
        return UILayer.UI2;
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_STAMINA;
    }
}
