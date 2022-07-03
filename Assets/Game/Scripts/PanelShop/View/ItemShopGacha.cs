using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopGacha : View
{
    [Inject] public ShowPopupGachaSignal showPopupGachaSignal { get; set; }
    [Inject] public GlobalData global { get; set; }
    [Inject] public ShowPopupGachaInfoSignal ShowPopupGachaInfoSignal { get; set; }


    [Inject] public AddRewardFromItemSignal AddRewardFromItemSignal { get; set; }
    [Inject] public CheckAndConsumeCurrencySignal CheckAndConsumeCurrencySignal { get; set; }
    
    
    public int idGacha;
    public Text costOpen1Text, costOpen10Text;
    public Gacha gacha;
    public Button infoGachaBtn;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        Setup();
    }
    void Setup()
    {
        gacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
        costOpen1Text.text = gacha.costOpen1.ToString();
        costOpen10Text.text = gacha.costOpen10.ToString();
        infoGachaBtn.onClick.AddListener(ShowInfoGacha);
    }
    public void ShowInfoGacha()
    {
        ShowPopupGachaInfoSignal.Dispatch(gacha);
    }
    public void Open()
    {
        Action Success = delegate { 
            global.CurrenctGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
            global.UpdateDataAllCurrencyView();

            DataGachaOpened dataGachaOpened = new DataGachaOpened();
            DataGachaRandom data= GachaLogic.GetGachaRandom(idGacha);        
            dataGachaOpened.datas.Add(data);

            List<EquipmentData> newItems = new List<EquipmentData>();
            List<AbsRewardLogic> rewardLogics = new List<AbsRewardLogic>();
            foreach (DataGachaRandom tempData in dataGachaOpened.datas)
            {
                EquipmentConfig config = EquipmentLogic.GetEquipmentConfigById(tempData.idConfig);
                EquipmentData newItem = EquipmentLogic.CloneEquipmentData(tempData.idConfig, tempData.Rarity, tempData.GearSlot, tempData.idOfHero, 1);
                newItem.mainStatData = EquipmentLogic.RandomStatEquipment(config.mainStatConfig, tempData.Rarity);
                newItems.Add(newItem);
                rewardLogics.Add(RewardUtils.ParseToRewardLogic(newItem));
            }
            
            AddRewardParameter parameter = new AddRewardParameter(rewardLogics,delegate {  }, false);
            
            AddRewardFromItemSignal.Dispatch(parameter);
            showPopupGachaSignal.Dispatch(dataGachaOpened);
        };
        CheckAndConsumeCurrencyParameter param = new CheckAndConsumeCurrencyParameter(CurrencyType.gem, gacha.costOpen1, Success);
        CheckAndConsumeCurrencySignal.Dispatch(param);
        

    }
    public void Open10()
    {
        Action Success = delegate { 
            global.CurrenctGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
            global.UpdateDataAllCurrencyView();

            DataGachaOpened dataGachaOpened = new DataGachaOpened();
            for (int i=0;i< 10; i++)
            {           
                DataGachaRandom data = GachaLogic.GetGachaRandom(idGacha);
                dataGachaOpened.datas.Add(data);
            }
            
            List<EquipmentData> newItems = new List<EquipmentData>();
            List<AbsRewardLogic> rewardLogics = new List<AbsRewardLogic>();
            foreach (DataGachaRandom tempData in dataGachaOpened.datas)
            {
                EquipmentConfig config = EquipmentLogic.GetEquipmentConfigById(tempData.idConfig);
                EquipmentData newItem = EquipmentLogic.CloneEquipmentData(tempData.idConfig, tempData.Rarity, tempData.GearSlot, tempData.idOfHero, 1);
                newItem.mainStatData = EquipmentLogic.RandomStatEquipment(config.mainStatConfig, tempData.Rarity);
                newItems.Add(newItem); 
                rewardLogics.Add(RewardUtils.ParseToRewardLogic(newItem));
            }

            AddRewardParameter parameter = new AddRewardParameter(rewardLogics,delegate {  }, false);
            
            AddRewardFromItemSignal.Dispatch(parameter);
            showPopupGachaSignal.Dispatch(dataGachaOpened);
        };
        CheckAndConsumeCurrencyParameter param = new CheckAndConsumeCurrencyParameter(CurrencyType.gem, gacha.costOpen10, Success);
        CheckAndConsumeCurrencySignal.Dispatch(param);
    }
    
    
}
public class DataGachaOpened
{
    public List<DataGachaRandom> datas = new List<DataGachaRandom>();
}
