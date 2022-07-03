using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelHeroCmd : AbsShowPopupCmd
{
    [Inject] public ParameterPanelHero ParameterPanelHero { get; set; }

    public override void Execute()
    {
        PanelHeroView panelHeroView = GetInstance<PanelHeroView>();
        //panelHeroView.ShowPanelByCmd();
        panelHeroView.ShowPopup(ParameterPanelHero);
    }
    public override string GetResourcePath()
    {
        return GameResourcePath.PANEL_HERO;
    }

    public override UILayer GetUiLayer()
    {
        return UILayer.UI1;
    }
}
