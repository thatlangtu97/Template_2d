using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class AddGoldCmd : Command
{
    [Inject] public int value { get; set; }
    [Inject] public GlobalData GlobalData { get; set; }

    public override void Execute()
    {
        DataManager.Instance.CurrencyDataManager.UpGold(value, false);
        GlobalData.UpdateDataAllCurrencyView();
    }
}
