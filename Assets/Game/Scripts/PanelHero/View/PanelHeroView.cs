using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroView : AbsPopupView
{
    public Button backBtn;
    public GameObject EquipmentDetailLeft, EquipmentDetailFight;
    public AbsPopupView PopupEquipmentDetailLeft, PopupEquipmentDetailFight;
    public InventoryView inventoryView;
    public HeroEquipmentView heroEquipmentView;
    protected override void Start()
    {
        base.Start();
        backBtn.onClick.AddListener(()=>popupManager.BackPopup(this));
//        popupManager.AddPopup(PopupKey.EquipmentHeroDetailLeft, PopupEquipmentDetailLeft);
//        popupManager.AddPopup(PopupKey.EquipmentHeroDetailRight, PopupEquipmentDetailFight);
        
    }
//    public void NotifyShowPanel()
//    {
//        base.NotifyShowPanel();
//        heroEquipmentView.Show();
//        inventoryView.ReloadPage();
////        PopupEquipmentDetailLeft.HidePopup();
////        PopupEquipmentDetailFight.HidePopup();
//    }

    public override bool EnableBack()
    {
        return true;
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        //throw new System.NotImplementedException();
    }

//    public override void ShowPanelByCmd()
//    {
//        EquipmentLogic.RemoveAllEquipmentToCraft();
//        base.ShowPanelByCmd();
//        
//    }

}
