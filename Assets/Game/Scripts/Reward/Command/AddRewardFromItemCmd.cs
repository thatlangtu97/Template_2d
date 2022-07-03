using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRewardFromItemCmd : strange.extensions.command.impl.Command
{
    [Inject] public AddRewardParameter Parameter { get; set; }

    [Inject] public ShowPopupRewardSignal ShowPopupRewardSignal { get; set; }

    [Inject] public GlobalData globalData { get; set; }
    
    List<AbsRewardLogic> afterProcess = new List<AbsRewardLogic>();

    public override void Execute()
    {
        afterProcess = new List<AbsRewardLogic>();
        for (int i = 0; i < Parameter.ItemInfos.Count; i++)
        {
            afterProcess.Add(Parameter.ItemInfos[i].AddReward(injectionBinder));
        }

        Process();
        Parameter.rewardGenerated.Invoke(afterProcess);
        globalData.UpdateDataAllCurrencyView();
    }

    public void Process()
    {
        Action callBack = delegate
        {
            Parameter.finish.Invoke();
        };
        if (Parameter.showPopup)
        {
            ShowPopupRewardSignal.Dispatch(afterProcess, callBack);
        }
        else
        {
            callBack.Invoke();
        }
    }
}