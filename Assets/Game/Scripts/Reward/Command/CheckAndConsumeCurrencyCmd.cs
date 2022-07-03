using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class CheckAndConsumeCurrencyCmd : Command
{
    [Inject]
    public CheckAndConsumeCurrencyParameter Parameter { get; set; }

    [Inject] public ShowTooltipTextSignal ShowTooltipTextSignal { get; set; }

    public override void Execute()
    {
        CurrencyType type = Parameter.type;
        switch (type)
        {
            case CurrencyType.gold:
                if (DataManager.Instance.CurrencyDataManager.gold >= Parameter.value)
                {
                    DataManager.Instance.CurrencyDataManager.DownGold(Parameter.value, false);
                    Parameter.success.Invoke();
                }
                else
                {
                    if(Parameter.failure!=null)
                        Parameter.failure.Invoke();
                    ShowTooltipTextSignal.Dispatch("Not Enough Gold",true);
                    
                }
                break;
            case CurrencyType.gem:
                if (DataManager.Instance.CurrencyDataManager.gem >= Parameter.value)
                {
                    DataManager.Instance.CurrencyDataManager.DownGem(Parameter.value, false);
                    Parameter.success.Invoke();
                }
                else
                {
                    if(Parameter.failure!=null)
                        Parameter.failure.Invoke();
                    ShowTooltipTextSignal.Dispatch("Not Enough Gem",true);
                }
                break;
            case CurrencyType.stamina:
                if (DataManager.Instance.CurrencyDataManager.stamina >= Parameter.value)
                {
                    DataManager.Instance.CurrencyDataManager.DownStamina(Parameter.value, false);
                    Parameter.success.Invoke();
                }
                else
                {
                    if(Parameter.failure!=null)
                        Parameter.failure.Invoke();
                }
                break;
        }
    }
}

public class CheckAndConsumeCurrencyParameter
{
    public CurrencyType type;
    public int value;
    public Action success;
    public Action failure;
    public CheckAndConsumeCurrencyParameter (){}
    
    public CheckAndConsumeCurrencyParameter ListenOnFailure(Action onFailure)
    {
        failure = onFailure;
        return this;
    }
    public CheckAndConsumeCurrencyParameter(CurrencyType type, int value, Action success)
    {
        this.type = type;
        this.value = value;
        this.success = success;
    }
}