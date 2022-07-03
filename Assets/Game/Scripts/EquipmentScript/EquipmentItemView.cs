using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using strange.extensions.mediation.impl;

public class EquipmentItemView : View
{
    [Inject] public GlobalData global { get; set; }
    [Inject] public ShowEquipmentDetailSignal showEquipmentDetailSignal { get; set; }

    public Image icon;
    public Image boderRarity;
    public Image backgroundRarity;
    public GameObject notice;
    public Text level;
    public Button btnClick;
    public Action onClickAction;
    public EquipmentData data;
    //public EquipmentConfig config;
    
    protected override void Awake()
    {
        base.Awake();
        if(btnClick)
            btnClick.onClick.AddListener(Onclick);
    }
    public void Show(EquipmentData data, EquipmentConfig config )
    {
        base.CopyStart();
        this.data = data;
        //this.config = config;
        icon.sprite = config.GearIcon;
        boderRarity.color = EquipmentLogic.GetColorByRarity(data.rarity);
        if(level!=null) level.text = $"Lv.{data.level}";
        backgroundRarity.sprite = EquipmentLogic.GetBackGroundByRarity(data.rarity);
        if (notice)
        {
            if (data.isNewItem == true)
            {
                notice.SetActive(true);
            }
            else
            {
                notice.SetActive(false);
            }
        }

    }
    public void ShowDetail(int valuePopup)
    {
        ParameterEquipmentDetail temp = new ParameterEquipmentDetail();
        temp.equipmentData = data;
        //temp.equipmentConfig = config;
        temp.popupkey = (PopupKey)valuePopup;
        showEquipmentDetailSignal.Dispatch(temp);
    }

    public void SetupAction(Action action )
    {
        onClickAction = action;
    }

    public void Onclick()
    {
        if (onClickAction != null)
        {
            onClickAction.Invoke();
        }
    }


}
public class ParameterEquipmentDetail
{
    public EquipmentData equipmentData;
    //public EquipmentConfig equipmentConfig;
    public PopupKey popupkey;
}
