using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelShopCmd : AbsShowPopupCmd
{
    [Inject] public ParameterPanelShop ParameterPanelShop { get; set; }

    public override void Execute()
    {
        PanelShopView panelShopView = GetInstance<PanelShopView>();
        panelShopView.ShowPopup(ParameterPanelShop);
    }
    public override string GetResourcePath()
    {
        return GameResourcePath.PANEL_SHOP;
    }

    public override UILayer GetUiLayer()
    {
        return UILayer.UI1;
    }
}
