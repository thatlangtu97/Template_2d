using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopupGachaInfoCmd : AbsShowPopupCmd
{
    [Inject] public Gacha gacha { get; set; }
    public override void Execute()
    {
        base.Execute();
        PopupGachaInfoView popupGachaInfoView = GetInstance<PopupGachaInfoView>();
        popupGachaInfoView.gacha = gacha;
        popupGachaInfoView.ShowPopup(new ParameterPopup());
    }

    public override UILayer GetUiLayer()
    {
        return UILayer.UI3;
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_INFO_GACHA;
    }
}
