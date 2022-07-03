using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class InventoryView : View
{
    [Inject] public GlobalData global{ get; set; }
    [Inject] public SetOldItemSignal SetOldItemSignal { get; set; }
    [Inject] public ShowEquipmentCompareSignal ShowEquipmentCompareSignal { get; set; }
    public List<TabType> tabTypes = new List<TabType>();
    public List<EquipmentItemView> equipmentItemViews = new List<EquipmentItemView>();
    [ShowInInspector]
    public List<EquipmentData> ListEquipment = new List<EquipmentData>();

    public Transform gridContainer;
    public ScrollRect scrollRectContainer;
    public int currentPage = 1;
    public int maxSlotInPage = 15;
    public CompareEquipmentInfo compareEquipmentInfo;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        currentPage = 1;
        foreach (TabType temp in tabTypes)
        {
            temp.button.onClick.AddListener(() => Open(temp.slot));
        }
        
        if (equipmentItemViews.Count == 0)
        {
            GameObject prefab = PrefabUtils.LoadPrefab(GameResourcePath.ITEM_EQUIPMENT_VIEW);
            for (int i = 0; i < maxSlotInPage; i++)
            {
                EquipmentItemView temp = Instantiate(prefab, gridContainer).GetComponent<EquipmentItemView>();
                equipmentItemViews.Add(temp);
                temp.SetupAction( ()=>ShowDetail(temp) );
            }
        }
    }
    public void ShowDetail(EquipmentItemView tempEquipment)
    {
//        ParameterEquipmentDetail temp = new ParameterEquipmentDetail();
//        temp.equipmentData = tempEquipment.data;
//        //temp.equipmentConfig = tempEquipment.config;
//        temp.popupkey = popupKeyDetail;
//        showEquipmentDetailSignal.Dispatch(temp);
        SetOldItemSignal.Dispatch(tempEquipment.data);
        
        ShowEquipmentCompareSignal.Dispatch(new ParameterEquipmentCompare(CompareEquipmentType.Right, compareEquipmentInfo ,tempEquipment.data));
        
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        currentPage = 1;
        Open(global.CurrentTab);
    }
    public void Open(GearSlot gearSlot)
    {
        //Todo show ListEquipment;
        currentPage = 1;
        if (global.CurrentTab != gearSlot)
        {
            ResetScroll();
        }
        global.CurrentTab = gearSlot;
        ReloadPage();
        ShowButton();
    }
    public void NextPage()
    {
        if (equipmentItemViews.Count * (currentPage) > ListEquipment.Count) return;
        currentPage += 1;
        ReloadPage();
    }
    public void PreviousPage()
    {
        currentPage -= 1;
        if (currentPage < 1) { currentPage = 1; }
        ReloadPage();
    }
    public void ReloadPage()
    {
        ListEquipment = EquipmentLogic.GetAllEquipmentBySlotOfHeroNotEquiped(global.CurrentTab,global.CurrentIdHero);        
        int countEquipment = ListEquipment.Count;
        int indexStart = equipmentItemViews.Count * (currentPage - 1);
        

        if (equipmentItemViews.Count < ListEquipment.Count)
        {
            int count = ListEquipment.Count - equipmentItemViews.Count;
            GameObject prefab = PrefabUtils.LoadPrefab(GameResourcePath.ITEM_EQUIPMENT_VIEW);
            for (int i = 0; i < count; i++)
            {
                EquipmentItemView temp = Instantiate(prefab, gridContainer).GetComponent<EquipmentItemView>();
                equipmentItemViews.Add(temp);
                temp.SetupAction( ()=>ShowDetail(temp) );
                maxSlotInPage += count;
            }
        }
        for (int i = 0; i < equipmentItemViews.Count; i++)
        {
            if (indexStart < countEquipment)
            {
                EquipmentLogic.ShowEquipmentView(ListEquipment[indexStart],equipmentItemViews[i]);
            }
            else
            {
                equipmentItemViews[i].gameObject.SetActive(false);
            }
            indexStart += 1;
        }
    }

    public void ReloadDataRemove(List<EquipmentData> datas)
    {
        foreach (var tempData in datas)
        {
            if(ListEquipment.Contains(tempData))
                ListEquipment.Remove(tempData);
        }
        int countEquipment = ListEquipment.Count;
        int indexStart = equipmentItemViews.Count * (currentPage - 1);
        

        if (equipmentItemViews.Count < ListEquipment.Count)
        {
            int count = ListEquipment.Count - equipmentItemViews.Count;
            GameObject prefab = PrefabUtils.LoadPrefab(GameResourcePath.ITEM_EQUIPMENT_VIEW);
            for (int i = 0; i < count; i++)
            {
                EquipmentItemView temp = Instantiate(prefab, gridContainer).GetComponent<EquipmentItemView>();
                equipmentItemViews.Add(temp);
                temp.SetupAction( ()=>ShowDetail(temp) );
                maxSlotInPage += count;
            }
        }
        for (int i = 0; i < equipmentItemViews.Count; i++)
        {
            if (indexStart < countEquipment)
            {
                EquipmentLogic.ShowEquipmentView(ListEquipment[indexStart],equipmentItemViews[i]);
            }
            else
            {
                equipmentItemViews[i].gameObject.SetActive(false);
            }
            indexStart += 1;
        }
        
    }

    public void EquipGear(EquipmentData datas)
    {
        ReloadPage();
    }
    public void ReShow(EquipmentData data)
    {
        for (int i = 0; i < ListEquipment.Count; i++)
        {
            if (ListEquipment[i].id == data.id)
            {
                ListEquipment[i] = data;
                EquipmentLogic.ShowEquipmentView(data,equipmentItemViews[i]);
            }
        }
    }
    
    public void ShowButton()
    {
        foreach (TabType temp in tabTypes)
        {
            Color newColor = Color.white;
            if (global.CurrentTab == temp.slot)
            {                
                temp.text.color = new Vector4(newColor.r, newColor.g, newColor.b, 1f);
            }
            else
            {
                temp.text.color = new Vector4(newColor.r, newColor.g, newColor.b, .5f);
            }
        }
    }

    void ResetScroll()
    {
        scrollRectContainer.verticalNormalizedPosition = 1f;
    }
    [System.Serializable]
    public struct TabType 
    {
        public GearSlot slot;
        public Button button;
        public Text text;
    }

}

