using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyAssetShower : View
{
    // Start is called before the first frame update
    [Inject] public GlobalData globalData { get; set; }
    public CurrencyType currencyType;
    public Text ValueText;
    public int value;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        Init();
        globalData.AddCurrencyAssetShower(currencyType, this);

    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        switch (currencyType) {
            case CurrencyType.gold:
                value = DataManager.Instance.CurrencyDataManager.gold;
                ValueText.text = $"{value}";
                
                break;
            case CurrencyType.gem:
                value = DataManager.Instance.CurrencyDataManager.gem;
                ValueText.text = $"{DataManager.Instance.CurrencyDataManager.gem}";
                break;
            case CurrencyType.stamina:
                value = DataManager.Instance.CurrencyDataManager.stamina;
                ValueText.text = $"{value}/{DataManager.Instance.CurrencyDataManager.maxStamina}";
                break;
        }
    }
    public void Setup()
    {
        int endValue = 0;
        switch (currencyType) {
            case CurrencyType.gold: 
                endValue = DataManager.Instance.CurrencyDataManager.gold;
                DOTween.To (() => value, x => value = x, endValue,.3f)
                    .OnUpdate
                    (
                        delegate
                        {
                            ValueText.text = $"{value}";
                        }
                    )
                    .OnComplete(
                    delegate
                    {
                        ValueText.text = $"{endValue}";
                    }
                    );
                //ValueText.text = $"{DataManager.Instance.CurrencyDataManager.gold}";
                break;
            case CurrencyType.gem:
                endValue = DataManager.Instance.CurrencyDataManager.gem;
                DOTween.To (() => value, x => value = x, endValue,.3f)
                    .OnUpdate
                    (
                        delegate
                        {
                            ValueText.text = $"{value}";
                        }
                    )
                    .OnComplete(
                        delegate
                        {
                            ValueText.text = $"{endValue}";
                        }
                    );
                //ValueText.text = $"{DataManager.Instance.CurrencyDataManager.gem}";
                break;
            case CurrencyType.stamina:
                endValue = DataManager.Instance.CurrencyDataManager.stamina;
                int maxValue = DataManager.Instance.CurrencyDataManager.maxStamina;
                DOTween.To (() => value, x => value = x, endValue,.3f)
                    .OnUpdate
                    (
                        delegate
                        {
                            ValueText.text = $"{value}/{maxValue}";
                        }
                    )
                    .OnComplete(
                        delegate
                        {
                            ValueText.text = $"{endValue}/{maxValue}";
                        }
                    );
                //ValueText.text = $"{DataManager.Instance.CurrencyDataManager.stamina}/{DataManager.Instance.CurrencyDataManager.maxStamina}";
                break;
        }
    }
}
