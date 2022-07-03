using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class ShowPopupRewardCmd : AbsShowPopupCmd
{
    [Inject] public List<AbsRewardLogic> listRewardLogics { get; set; }
    [Inject] public Action action { get; set; }

    public override void Execute()
    {
        PopupRewardView popupReward = GetInstance<PopupRewardView>();
        popupReward.SetParameter(new ShowPopupRewardParameter(listRewardLogics));
//        popupReward.ShowPopupByCmd();
        popupReward.ShowPopup(new ParameterPopup());
    }

    public override UILayer GetUiLayer()
    {
        return UILayer.UI4;
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_REWARD;
    }
}

public class ShowPopupRewardParameter : ParameterPopup
{
    public List<AbsRewardLogic> listRewardLogics;
    public ShowPopupRewardParameter(){}

    public ShowPopupRewardParameter(List<AbsRewardLogic> listRewardLogics)
    {
        this.listRewardLogics = listRewardLogics;
    }
}
