using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelHomeView : AbsPopupView
{
    [Inject] public ShowPopupStaminaSignal showPopupStaminaSignal { get; set; }
    [Inject] public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    [Inject] public ShowPanelCraftSignal showPanelCraftSignal { get; set; }
    [Inject] public ShowPanelShopSignal showPanelShopSignal { get; set; }
    public Button StaminaBtn;
    public Button ShopBtn;
    public Button HeroBtn;
    public Button CraftBtn;

    public Button StartGameBtn;
    //public Button ShopGoldBtn, ShopGemBtn;
    public Doozy.Engine.UI.UIButton UIBtnShop, UIBtnHero , UIBtnCraft;
    protected override void Start()
    {
        base.Start();
        HeroBtn.onClick.AddListener(ShowPanelHero);
        CraftBtn.onClick.AddListener(ShowPanelCraft);

        ShopBtn.onClick.AddListener(ShowPanelShopGold);
        StartGameBtn.onClick.AddListener(StartGame);

    }

//    public override void ShowPanelByCmd()
//    {
//        base.ShowPanelByCmd();
//        popupManager.SetFirstSelect(ShopBtn.gameObject);
//    }

    public override bool EnableBack()
    {
        return false;
    }

    protected override void OnShowPopup<T>(T parameter)
    {
    }

    public void ShowPanelHero()
    {
        showPanelHeroSignal.Dispatch(new ParameterPanelHero());
    }
    public void ShowPanelCraft()
    {
        showPanelCraftSignal.Dispatch();
    }
    public void ShowPanelShopGold()
    {
        //popupManager.popupKey = PopupKey.ShopGoldPopup;
        showPanelShopSignal.Dispatch(new ParameterPanelShop(ShopTabType.Gold));
    }
    public void LoadScene(string name)
    {
        if (PlayFlashScene.instance)
            PlayFlashScene.instance.Loading(name, 1.2f);
        else
            SceneManager.LoadScene(name);
    }

    public void StartGame()
    {

        //SceneManager.LoadScene("TestBt");
        LoadScene("TestBt");
    }
}
