using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestUtils :View
{
    [Inject] public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    [Inject] public PopupManager popupManager { get; set; }
    [Inject] public GlobalData globalData { get; set; }
    [Inject] public ShowPopupCraftSignal ShowPopupCraftSignal { get; set; }
    [Inject] public ShowTooltipPopupSignal ShowTooltipPopupSignal { get; set; }
    [Inject] public AddRewardFromItemSignal AddRewardFromItemSignal { get; set; }
    [Inject] public ShowTooltipTextSignal ShowTooltipTextSignal { get; set; }
    
    [Inject] public SellGearSignal SellGearSignal { get; set; }
    public PanelKey panelKey;
    public PopupKey popupKey;
    public Transform Transform;
    public CurrencyType currencyType;
    public int valueCurrency;

    public string StringToolTipText;
    
    public GearSlot gearSlotToRemove;
    public int gearcCuntRemove;
    protected override void Start()
    {
        base.Start();
    }
    
    private void Update()
    {

//        if (Input.GetKeyDown(KeyCode.U))
//        {
//            SceneManager.LoadScene("testSpawnObject");
//        }
//        if (Input.GetKeyDown(KeyCode.I))
//        {
//            SceneManager.LoadScene("HomeScene");
//        }
//        if (Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.End)){
//
//            ShowPopupCraftSignal.Dispatch(DataManager.Instance.InventoryDataManager.GetAllEquipmentBySlot(GearSlot.weapon)[0]);
//        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //globalData.UpdateDataAllCurrencyView();
//            EquipmentRewardLogic temp = new EquipmentRewardLogic(dataEquip);
//            AddRewardFromItemSignal.Dispatch(new AddRewardParameter(temp,null,true));
            ShowTooltipTextSignal.Dispatch(StringToolTipText,true);
        }

    }

    
    [Button("PARSE CURRENCY ABS LOGIC", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void TestParseCurrencyAbsLogic()
    {
        AbsRewardLogic temp = RewardUtils.ParseToRewardLogic(currencyType, valueCurrency);
        ShowTooltipPopupSignal.Dispatch(new ToolTipPopupParameter(temp,Transform.position));
    }
    public EquipmentData dataEquip;
    [Button("PARSE EQUIpMENT ABS LOGIC", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void TestParseEquipmentAbsLogic()
    {
        AbsRewardLogic temp = new EquipmentRewardLogic(dataEquip);
        ShowTooltipPopupSignal.Dispatch(new ToolTipPopupParameter(temp,Transform.position));
        
    }
    [Button("SELL ALL GEAR BY SLOT", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void TestRemoveAllGearBySlot()
    {
        DataSellGear data = new DataSellGear();
        data.datas = EquipmentLogic.GetEquipmentInventory(gearSlotToRemove, 0);
        data.gearOfHero = 0;
        SellGearSignal.Dispatch(data);
        
    }
//    [Button("SELL GEAR BY SLOT", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
//    void TestRemoveGearBySlot()
//    {
//        List<EquipmentData> tempData = EquipmentLogic.GetEquipmentInventory(gearSlotToRemove, 0);
//        int count = gearcCuntRemove;
//        foreach (var VARIABLE in tempData)
//        {
//            if (count <= 0)
//            {
//                break;
//            }
//            else
//            {
//                int idGearEquiped = DataManager.Instance.HeroDataManager.GetIdEquipmentEquiped(gearSlotToRemove, 0);
//                if (VARIABLE.id != idGearEquiped)
//                {
//                    tempData.Remove(VARIABLE);
//                }
//            }
//        }
//        
////        while (count>0)
////        {
////            tempData.RemoveAt(0);
////            count -= 1;
////        }
//        SellGearSignal.Dispatch(tempData);
//        
//    }
}
