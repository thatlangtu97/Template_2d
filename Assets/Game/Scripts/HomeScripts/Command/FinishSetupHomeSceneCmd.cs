using System.CodeDom;
using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSetupHomeSceneCmd : Command
{
    [Inject]
    public PopupManager popupManager { get; set; }
    [Inject]
    public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    [Inject]
    public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    [Inject]
    public ShowPanelShopSignal showPanelShopSignal { get; set; }
    [Inject]
    public ShowPanelCraftSignal showPanelCraftSignal { get; set; }

    public override void Execute()
    {
        showPanelHomeSignal.Dispatch();
//        string panelKey = popupManager.GetPanelAfterLoadHomeScene();
//        switch (panelKey)
//        {
//            case "PanelHomeView":
//                showPanelHomeSignal.Dispatch(new ParameterPanelHome());
//                break;
//            case "PanelHeroView":
//                showPanelHeroSignal.Dispatch(new ParameterPanelHero());
//                break;
//            case "PanelCraftView":
//                showPanelCraftSignal.Dispatch();
//                break;
//            case "PanelShopView":
//                showPanelShopSignal.Dispatch(new ParameterPanelShop(ShopTabType.Gold));
//                break;
//        }
        //popupManager.ResetPanelShowAfterLoadHomeScene();
    }
}
