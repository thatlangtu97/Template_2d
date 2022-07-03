using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelCraftCmd : AbsShowPopupCmd
{
    public override void Execute()
    {
        PanelCraftView panelCraftView = GetInstance<PanelCraftView>();
        panelCraftView.ShowPopup(new ParameterPopup());
    }
    public override UILayer GetUiLayer()
    {
        return UILayer.UI1;
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.PANEL_CRAFT;
    }
}
