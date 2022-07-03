using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDetailView : AbsPopupView
{

    [Inject] public EquipGearSignal equipGearSignal { get; set; }
    [Inject] public UnequipGearSignal unEquipGearSignal { get; set; }
    [Inject] public NotificationPanelHeroSignal NotificationPanelHeroSignal { get; set; }
    [Inject] public NotificationPanelCraftSignal NotificationPanelCraftSignal { get; set; }
    
    [Inject] public LevelUpGearSignal LevelUpGearSignal { get; set; }
    
    [Inject] public SellGearSignal SellGearSignal  { get; set; }
    public EquipmentItemView EquipmentView;
    public Text nameEquipment;
    public Text rarityEquipment;
    public Text mainStatType,mainStatValue;
    public Text textLevel;
    EquipmentData equipmentData;
    EquipmentConfig equipmentConfig;
    [BoxGroup]
    public GameObject btnEquip, btnSell, btnSelect, btnLevelUp;

    public Action actionOnHide;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
    }
    public void SetupData(EquipmentData equipmentData)
    {
        this.equipmentData = equipmentData;
        //this.equipmentConfig = equipmentConfig;
        equipmentConfig = EquipmentLogic.GetEquipmentConfigById(equipmentData.idConfig);
        EquipmentLogic.ShowEquipmentView(equipmentData,EquipmentView);
        nameEquipment.text = equipmentConfig.gearName;
        nameEquipment.color = EquipmentLogic.GetColorByRarity(equipmentData.rarity);
        rarityEquipment.text = equipmentData.rarity.ToString();
        rarityEquipment.color = EquipmentLogic.GetColorByRarity(equipmentData.rarity);
        mainStatType.text = EquipmentLogic.StatTypeToString(equipmentData.mainStatData.statType);
        mainStatValue.text = EquipmentLogic.StatValueToString(equipmentData.mainStatData.statType,equipmentData.mainStatData.baseValue);
        textLevel.text = $"Lv.{equipmentData.level}";
    }

    public void Show()
    {
        UiViewController.Show();
    }
    public void ReShow(EquipmentData equipmentData)
    {
        if (this.equipmentData.id == equipmentData.id)
        {
            this.equipmentData = equipmentData;
            textLevel.text = $"Lv.{equipmentData.level}";
        }
    }
    public void EquipGear()
    {
        equipGearSignal.Dispatch(equipmentData);
        NotificationPanelHeroSignal.Dispatch();
        Hide();

    }
    public void UnEquipGear()
    {
        unEquipGearSignal.Dispatch(equipmentData);
        NotificationPanelHeroSignal.Dispatch();
        Hide();
    }
    public void SelectGearCraft()
    {
        EquipmentLogic.AddEquipmentToCraft(equipmentData);
        NotificationPanelCraftSignal.Dispatch();
        Hide();
    }
    public void UnSelectGearCraft()
    {
        EquipmentLogic.RemoveEquipmentToCraft(equipmentData);
        NotificationPanelCraftSignal.Dispatch();
        Hide();
    }

    public void LevelUpGear()
    {
        LevelUpGearSignal.Dispatch(equipmentData);
    }

    public void SellGear()
    {
        SellGearSignal.Dispatch(new DataSellGear(equipmentData));
    }

    public void CheckSell(List<EquipmentData> datas)
    {
        if (datas.Contains(equipmentData))
        {
            Hide();
        }
    }

    public void ShowButton(CompareEquipmentInfo compareInfo)
    {
        switch (compareInfo)
        {
            case CompareEquipmentInfo.CharacterEquip:
                ActiveObject(new List<GameObject>() {btnEquip, btnSell, btnLevelUp}, true);
                ActiveObject(new List<GameObject>() {btnSelect}, false);
                break;
            case CompareEquipmentInfo.Craft:
                ActiveObject(new List<GameObject>() {btnEquip, btnSell, btnLevelUp}, false);
                ActiveObject(new List<GameObject>() {btnSelect}, true);
                break;
        }
    }

    public override void Hide()
    {
        base.Hide();
        if (actionOnHide != null)
        {
            actionOnHide.Invoke();
        }
    }

    public void ListenerActionOnHide(Action action)
    {
        actionOnHide = action;
    }
    void ActiveObject(List<GameObject> listObject, bool active)
    {
        foreach (var obj in listObject)
        {
            if(obj != null)
                obj.SetActive(active);
        }
    }
    public override bool EnableBack()
    {
        return true;
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        
    }
}
