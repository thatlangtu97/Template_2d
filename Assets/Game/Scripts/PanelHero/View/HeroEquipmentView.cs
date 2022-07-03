using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroEquipmentView : View
{
    [Inject] public GlobalData global { get; set; }
    [Inject] public OnViewHeroSignal OnViewHeroSignal { get; set; }
    [Inject] public ShowEquipmentDetailSignal showEquipmentDetailSignal { get; set; }

    [Inject] public SetOldItemSignal SetOldItemSignal { get; set; }

    [Inject] public ShowEquipmentCompareSignal ShowEquipmentCompareSignal { get; set; }
    
    private Dictionary<GearSlot, EquipmentOfHeroView> DicEquipmentOfHeroView = new Dictionary<GearSlot, EquipmentOfHeroView>();
    [SerializeField]
    private List<EquipmentOfHeroView> listEquipmentOfHeroView = new List<EquipmentOfHeroView>();
    private List<EquipmentData> currentEquipment = new List<EquipmentData>();
    [SerializeField]
    private HeroPreViewData heroPreViewData;
    public PopupKey popupKeyDetail;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        InitItem();
    }
    protected override void Start()
    {
        base.Start();
        Show();
        
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        Show();

    }
    public void Show()
    {
        currentEquipment = EquipmentLogic.GetEquipmentOfHero(global.CurrentIdHero);
        foreach (EquipmentOfHeroView equipmentOfHero in DicEquipmentOfHeroView.Values) {
            equipmentOfHero.view.gameObject.SetActive(false);
            equipmentOfHero.backItem.SetActive(true);
        }
        foreach (EquipmentData data in currentEquipment)
        {
            if (data != null)
            {
                DicEquipmentOfHeroView[data.gearSlot].backItem.SetActive(false);
                EquipmentLogic.ShowEquipmentView(data, DicEquipmentOfHeroView[data.gearSlot].view);
                DicEquipmentOfHeroView[data.gearSlot].view.transform.localPosition = Vector3.zero;
            }
        }
        heroPreViewData.Show(global.CurrentIdHero);
    }

    public void ReShow(EquipmentData data)
    {
        for (int i = 0; i < currentEquipment.Count; i++)
        {
            if (currentEquipment[i].id == data.id)
            {
                currentEquipment[i] = data;
                EquipmentLogic.ShowEquipmentView(data,DicEquipmentOfHeroView[data.gearSlot].view);
            }
        }
    }

    public void EquipGear(EquipmentData data)
    {
        Show();
    }
    private void InitItem()
    {
        foreach(EquipmentOfHeroView temp in listEquipmentOfHeroView)
        {
            if (temp.container.childCount == 0)
            {
                GameObject prefab = PrefabUtils.LoadPrefab(GameResourcePath.ITEM_EQUIPMENT_VIEW_CIRCLE);
                EquipmentItemView itemview = Instantiate(prefab, temp.container).GetComponent<EquipmentItemView>();
                itemview.transform.localPosition=Vector3.zero;
                itemview.SetupAction( ()=>ShowDetail(itemview) );
                
                if (!DicEquipmentOfHeroView.ContainsKey(temp.slot))
                {
                    DicEquipmentOfHeroView.Add(temp.slot, temp);
                }
                else
                {
                    DicEquipmentOfHeroView[temp.slot] = temp;
                }
                temp.view = itemview;
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
//        SetOldItemSignal.Dispatch(tempEquipment.data);
        
        ShowEquipmentCompareSignal.Dispatch(new ParameterEquipmentCompare(CompareEquipmentType.Left, CompareEquipmentInfo.CharacterEquip ,tempEquipment.data));
    }
    
    [System.Serializable]
    public class EquipmentOfHeroView
    {
        public GearSlot slot;
        public EquipmentItemView view;
        public GameObject backItem;
        public Transform container;
    }
    [System.Serializable]
    public struct HeroPreViewData
    {
        public Image previewImage;
        public Text previewName;

        public void Show(int id)
        {
            previewImage.sprite = HeroLogic.GetHeroConfigById(id).preview;
            previewImage.SetNativeSize();
            previewName.text= HeroLogic.GetHeroConfigById(id).name;
        }
    }
}
