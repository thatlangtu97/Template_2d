using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCraftView : AbsPopupView
{
    public Button backBtn;
    public GameObject EquipmentDetailLeft, EquipmentDetailFight;
    public AbsPopupView PopupEquipmentDetailLeft, PopupEquipmentDetailFight;
    public CraftEquipmentView craftEquipmentView;
    public InventoryView inventoryView;
    protected override void Start()
    {
        base.Start();
        backBtn.onClick.AddListener(() => popupManager.BackPopup(this));
//        popupManager.AddPopup(PopupKey.EquipmentCraftDetailLeft, PopupEquipmentDetailLeft);
//        popupManager.AddPopup(PopupKey.EquipmentCraftDetailRight, PopupEquipmentDetailFight);
    }
    public void NotifyShowPopup()
    {
        base.NotifyShowPopup();
        craftEquipmentView.Show();
        inventoryView.ReloadPage();
    }

    public override bool EnableBack()
    {
        return true;
    }

    protected override void OnShowPopup<T>(T parameter)
    {
//        throw new System.NotImplementedException();
    }

//    public override void ShowPanelByCmd()
//    {
//        EquipmentLogic.RemoveAllEquipmentToCraft();
//        base.ShowPanelByCmd();
//        
//        //craftEquipmentView.Show();
//        //inventoryView.ReloadPage();
//        
//    }
}
