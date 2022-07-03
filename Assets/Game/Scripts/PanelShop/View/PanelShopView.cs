using strange.extensions.signal.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelShopView : AbsPopupView
{
    public Button backBtn;
    public List<PopupShopType> ListPopup;
//    public List<TabShopType> ListTab;
    public List<GameObject> listTabGameObject;
    
    private ShopTabController tabController;
    protected override void Awake()
    {
        base.CopyStart();
        Setup();
        //ShowPopupShop(popupManager.popupKey);
        InitTab();

    }
    protected override void Start()
    {
        base.Start();
//        Setup();
        //ShowPopupShop(popupManager.popupKey);

    }
    public void Setup()
    {
        backBtn.onClick.AddListener(() => popupManager.BackPopup(this));
//        foreach (TabShopType tab in ListTab)
//        {
//            tab.btn.onClick.AddListener(() => ShowPopupShop(tab.key));
//        }
    }
//    public override void ShowPanelByCmd()
//    {
//        base.ShowPanelByCmd();
//        //ShowPopupShop(popupManager.popupKey);
//    }
//    public override void ShowPanel()
//    {
//        base.ShowPanel();
//        //ShowPopupShop(popupManager.popupKey);
//    }

    public override bool EnableBack()
    {
        return true;
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        ParameterPanelShop p = parameter as ParameterPanelShop;
        tabController.SetTabInit(p.shopTabType);
        tabController.Show();
        ClickTab(p.shopTabType);
    }

//    protected override void OnEnable()
//    {
//        base.CopyStart();
//        base.OnEnable();
//        //ShowPopupShop(popupManager.popupKey);
//    }
//    void ShowPopupShop(PopupKey popupKey)
//    {
//        foreach (PopupShopType popup in ListPopup)
//        {
//            if(popup.key == popupKey)
//            {
//                popup.Prefab.GetComponent<AbsPopupView>().ShowPopup();
//            }
//            else
//            {
//                if(popup.Prefab.activeInHierarchy)
//                    popup.Prefab.GetComponent<AbsPopupView>().HidePopup();
//            }
//        }
//        foreach (TabShopType tab in ListTab)
//        {
//            Color colorTemp = tab.text.color;
//            if (tab.key == popupKey)
//            {
//                
//                tab.text.color = new Vector4(colorTemp.r, colorTemp.g, colorTemp.b, 1f);
//            }
//            else
//            {
//                tab.text.color = new Vector4(colorTemp.r, colorTemp.g, colorTemp.b, 0.5f);
//            }
//        }
//    }
    List<TabInitInfo<ShopTabType>> TabInitInfos() {
        List<TabInitInfo<ShopTabType>> ret = new List<TabInitInfo<ShopTabType>>();
        ret.Add(new TabInitInfo<ShopTabType>(ShopTabType.Gold, ClickTab));
        ret.Add(new TabInitInfo<ShopTabType>(ShopTabType.Gem, ClickTab));
        ret.Add(new TabInitInfo<ShopTabType>(ShopTabType.Gacha, ClickTab));
        return ret;
    }
    void ClickTab(ShopTabType shopResourceTabType) {
       
//        switch (shopResourceTabType) {
//            case ShopTabType.Gold:
//                Debug.Log("vao tab gold");
//                break;
//            case ShopTabType.Gem:
//                Debug.Log("vao tab gem");
//                break;
//            case ShopTabType.Gacha:
//                Debug.Log("vao tab gacha");
//                break;
//        }

        ActionBufferManager.Instance.ActionDelayFrame(
            delegate
            {
                foreach (var VARIABLE in ListPopup)
                {
                    UiViewController childView = VARIABLE.Prefab.GetComponent<UiViewController>();
                    childView.StopAllCoroutines();
                    if (VARIABLE.key == shopResourceTabType)
                    {
                
                        childView.Show();
                        //VARIABLE.Prefab.GetComponent<UiViewController>().Show();
                    }
                    else
                    {
                        childView.Hide();
                        //VARIABLE.Prefab.GetComponent<UiViewController>().Hide();
                    }
                }
            }, 
            1
            );

    }
    
    void InitTab() {
        tabController = new ShopTabController(listTabGameObject,TabInitInfos());
    }
    
    

    [System.Serializable]
    public class PopupShopType
    {
        public GameObject Prefab;
        public ShopTabType key;
    }
    [System.Serializable]
    public class TabShopType
    {
        public Button btn;
        public Text text;
        public PopupKey key;
    }

}
