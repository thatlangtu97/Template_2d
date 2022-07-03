using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class ShowPopupCraftCmd : AbsShowPopupCmd
{
    [Inject] public EquipmentData equipmentData { get; set; }
    public override void Execute()
    {
        ShowPopupCraftView showPopupCraftView = GetInstance<ShowPopupCraftView>();
        showPopupCraftView.equipmentData = equipmentData;
        showPopupCraftView.ShowPopup(new ParameterPopup());
    }

    public override UILayer GetUiLayer()
    {
        return UILayer.UI2;
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_CRAFT;
    }
}
