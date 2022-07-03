using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRewardGamePlayPopupCmd : AbsShowPopupCmd
{
    public override void Execute()
    {
        base.Execute();
        RewardGameplayPopup popup = GetInstance<RewardGameplayPopup>();
        popup.ShowPopup(new ParameterPopup());
    }

    public override UILayer GetUiLayer()
    {
        return UILayer.UI2;
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_REWARD_GAMEPLAY;
    }
}
