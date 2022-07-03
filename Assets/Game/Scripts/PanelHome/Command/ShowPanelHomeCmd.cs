using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelHomeCmd : AbsShowPopupCmd
{
//    [Inject] public ParameterPanelHome ParameterPanelHome { get; set; }

    public override void Execute()
    {
        PanelHomeView panelHomeView = GetInstance<PanelHomeView>();
        panelHomeView.ShowPopup(new ParameterPopup());
    }
    public override string GetResourcePath()
    {
        return GameResourcePath.PANEL_HOME;
    }

    public override UILayer GetUiLayer()
    {
        return UILayer.UI1;
    }
}
