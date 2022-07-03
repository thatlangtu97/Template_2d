using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class AddStaminaCmd : Command
{
    [Inject] public int value { get; set; }

    public override void Execute()
    {
        DataManager.Instance.CurrencyDataManager.UpStamina(value, false);
    }
}