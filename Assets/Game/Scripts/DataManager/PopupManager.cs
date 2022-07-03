using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.context.impl;
using UnityEngine;
using strange.extensions.signal.impl;
using UnityEngine.EventSystems;

//public class PopupManager 
//{
//    public string panelKey { get; set; }
//    public string popupKey { get; set; }
//
//    public string sceneKey { get; set; }
//    
//    public string BasePabelKey;
//    
//    public Dictionary<string, List<string>> backPanelData = new Dictionary<string, List<string>>();
//    public Dictionary<UILayer, Transform> UIDic = new Dictionary<UILayer, Transform>();
//    public Dictionary<string, AbsPopupView> PanelDic = new Dictionary<string, AbsPopupView>();
//    public Dictionary<string, AbsPopupView> PopupDic = new Dictionary<string, AbsPopupView>();
//    
//    public EventSystem eventSystem { get; set; }
//    public List<AbsPopupView> ListPopupShow = new List<AbsPopupView>();
//    [Inject] public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
//    [Inject] public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
//    [Inject] public ShowPanelShopSignal showPanelShopSignal { get; set; }
//    
//    
//    // 
//    public List<Type> fullScreenPopup= new List<Type>();
//    private List<AbsPopupView> listPopupShow = new List<AbsPopupView>();
//    private Type curPopup = null;
//    
//    
//    public PopupManager()
//    {
//        SetupFullScreen();
//    }
//
//    void SetupFullScreen()
//    {
//        fullScreenPopup.Add(typeof(PanelHomeView));
//        fullScreenPopup.Add(typeof(PanelHeroView));
//        fullScreenPopup.Add(typeof(PanelShopView));
//        fullScreenPopup.Add(typeof(PanelCraftView));
//    }
//    
//    
//    
//    
//    
//    public void SetFirstSelect(GameObject gameObjectFirst)
//    {
//        if (eventSystem)
//        {
//            eventSystem.firstSelectedGameObject = gameObjectFirst;
//        }
//    }
//    public void AddUILayer(UILayer layer, Transform transform)
//    {
//
//        if (UIDic.ContainsKey(layer))
//        {
//            UIDic[layer] = transform;
//        }
//        else
//        {
//            UIDic.Add(layer, transform);
//
//        }
//    }
//    
//    public Transform GetUILayer(UILayer layer)
//    {
//        if (UIDic.ContainsKey(layer))
//        {
//            return UIDic[layer];
//        }
//        else
//        {
//            return null;
//        }
//
//    }
//
//    #region PANEL
////    public bool CheckContainPanel(string key)
////    {
////        if (PanelDic.ContainsKey(key))
////        {
////            return true;
////        }
////        return false;
////    }
////    public AbsPopupView GetPanelByPanelKey(string key)
////    {
////        if (PanelDic.ContainsKey(key))
////        {
////            return PanelDic[key];
////        }
////        
////        return null;
////    }
////    public void AddPanel(AbsPopupView panel)
////    {
////        string key = panel.GetType().ToString();
////        if (PanelDic.ContainsKey(key))
////        {
////            PanelDic[key] = panel;
////        }
////        else
////        {
////            PanelDic.Add(key, panel);
////        }
////
////        if (!backPanelData.ContainsKey(sceneKey))
////        {
////            backPanelData.Add(sceneKey, new List<string>(){key});
////        }
////        if (!backPanelData[sceneKey].Contains(key))
////        {
////            backPanelData[sceneKey].Add(key);
////        }
////    }
//
//    
//    public void ResetPanelAfterLoadHomeScene()
//    {
//        panelKey = typeof(PanelHomeView).ToString();
//        BasePabelKey = typeof(PanelHomeView).ToString();
//    }
//    public string GetPanelAfterLoadHomeScene()
//    {
//        return panelKey;
//    }
//    public void ResetPanelShowAfterLoadHomeScene()
//    {
//        //panelKey = PanelKey.PanelHome;
//    }
//    
//    public void BackPanel()
//    {
//        foreach (var VARIABLE in listPopupShow)
//        {
//            VARIABLE.Hide();
//            listPopupShow.Remove(VARIABLE);
//            return;
//        }
////        //Disable popup
////        AbsPopupView lastPopup = null;
////        foreach (AbsPopupView temp in PopupDic.Values)
////        {
////            if (temp != null)
////            {
////                if (temp.gameObject.activeInHierarchy == true)
////                {
////                    lastPopup = temp;
////                }
////            }
////        }
////        if (lastPopup != null)
////        {
////            lastPopup.Hide();
////            return;
////        }
////        //disable Panel
////        foreach (string temp in PanelDic.Keys)
////        {
////            if (temp != BasePabelKey)
////            {
////                if (PanelDic[temp] != null)
////                {
////                    if (PanelDic[temp].gameObject.activeInHierarchy == true)
////                    {
////                        PanelDic[temp].GetComponent<AbsPopupView>().Hide();
////                    }
////                }
////            }
////        }
////
////        if (sceneKey == "Home")
////        {
////            if (BasePabelKey == typeof(PanelHomeView).ToString())
////            {
////                showPanelHomeSignal.Dispatch(new ParameterPanelHome());
////                panelKey = typeof(PanelHomeView).ToString();
////            }
////        }
//    }
//    #endregion
//
//    #region POPUP
//    public bool CheckContainPopup(AbsPopupView popup)
//    {
//        string key = popup.GetType().ToString();
//        if (PopupDic.ContainsKey(key))
//        {
//            return true;
//        }
//
//        if (PanelDic.ContainsKey(key))
//        {
//            return true;
//        }
//        return false;
//    }
//    public AbsPopupView GetPopupByPopupKey(AbsPopupView popup)
//    {
//        string key = popup.GetType().ToString();
//        if (PanelDic.ContainsKey(key))
//        {
//            return PanelDic[key];
//        }
//        if (PopupDic.ContainsKey(key))
//        {
//            return PopupDic[key];
//        }
//
//        return null;
//    }
//    public void AddPopup(AbsPopupView panel)
//    {
//        string key = panel.GetType().ToString();
//        if (PopupDic.ContainsKey(key))
//        {
//            PopupDic[key] = panel;
//        }
//        else
//        {
//            PopupDic.Add(key, panel);
//        }
//    }
//    public void ShowPopup(AbsPopupView absPopup,bool addToListShow=true)
//    {
//        Debug.Log($"ShowPopup {absPopup.GetType().ToString()}");
//        if (fullScreenPopup.Contains(absPopup.GetType()))
//        {
//            
////            string key = absPopup.GetType().ToString();
////            if (!listTypeHasSetup.Contains(absPopup.GetType()))
////            {
////                SetupBackForPopup(absPopup);
////            }
////            if (backData.ContainsKey(key))
////            {
////                currentBackData.Add(absPopup.GetType(), key);
////            }
//
//            curPopup = absPopup.GetType();
//            string key = absPopup.GetType().ToString();
//            Debug.Log(key);
//            
//            foreach (Type temp in fullScreenPopup)
//            {
//                string keyFullScreen = temp.GetType().ToString();
//                Debug.Log(keyFullScreen);
//                if (keyFullScreen != key)
//                {
//                    if (PopupDic.ContainsKey(keyFullScreen))
//                    {
//                        if (PopupDic[keyFullScreen] != null)
//                            PopupDic[keyFullScreen].Hide();
//                    }
//                }
//            }
//    
//            for (int i = listPopupShow.Count - 1; i >= 0; i--)
//            {
//                if (listPopupShow[i] != null)
//                    listPopupShow[i].Hide();
//            }
//            listPopupShow.Clear();
//        }
//        if (addToListShow)
//            listPopupShow.Add(absPopup);
//    }
//    #endregion
//    
//    
//    
//    public T GetPopupShow<T>() where T : AbsPopupView {
//        for (int i = 0; i < ListPopupShow.Count; i++)
//        {
//            if (ListPopupShow[i].GetType() == typeof(T))
//            {
//                return (T)ListPopupShow[i];
//            }
//        }
//
//        return null;
//    }
//    public bool GetPopupShow<T>(out AbsPopupView popup) where T : AbsPopupView
//    {
//        if (IsShowPopup<T>())
//        {
//            popup = GetPopupShow<T>();
//            return true;
//        }
//
//        popup = null;
//        return false;
//    }
//    public bool IsShowPopup<T>() where T : AbsPopupView
//    {
//        for (int i = 0; i < ListPopupShow.Count; i++)
//        {
//            if (ListPopupShow[i].GetType() == typeof(T))
//            {
//                return true;
//            }
//        }
//
//        return false;
//    }
//
//}
public class PopupManager
	{
		[Inject]
		public GlobalData GlobalData { get; set; }

		public MVCSContext CurrentContext { get; set; }
		List<Type> listTypeHasSetup = new List<Type>();
		private Dictionary<UILayer, Transform> UIDic = new Dictionary<UILayer, Transform>();
		private Dictionary<Type, Type> autoBackData = new Dictionary<Type, Type>();
		private Dictionary<string, Type> backData = new Dictionary<string, Type>();
		private Dictionary<Type, string> currentBackData = new Dictionary<Type, string>();
		private List<Type> popupIgnoreDisable = new List<Type>();

		private Type curPopup = null;
		public PopupKey PopupKey { get; set; }
		public Dictionary<string, AbsPopupView> PopupDic = new Dictionary<string, AbsPopupView>();
		private List<Type> fullScreenPopup = new List<Type>();
		private List<AbsPopupView> listPopupShow = new List<AbsPopupView>();
		Action<AbsPopupView, bool> onHidePopup = delegate(AbsPopupView popup, bool showAnotherPopup) { };

		public bool FirstSelectCharacter = true;
		public PopupManager()
		{
			Setup();
		}

		public void Setup()
		{
			SetFullscreenPopup();
			SetupPopupIgnoreDisable();
		}
    public void AddUILayer(UILayer layer, Transform transform)
    {

        if (UIDic.ContainsKey(layer))
        {
            UIDic[layer] = transform;
        }
        else
        {
            UIDic.Add(layer, transform);

        }
    }
    
    public Transform GetUILayer(UILayer layer)
    {
        if (UIDic.ContainsKey(layer))
        {
            return UIDic[layer];
        }
        else
        {
            return null;
        }

    }
		private void SetupPopupIgnoreDisable()
		{
//			popupIgnoreDisable.Add(typeof(AchivementUnlockNotifyPopup));
//			popupIgnoreDisable.Add(typeof(TooltipPopup));
//			popupIgnoreDisable.Add(typeof(WaitingResponsePopup));
		}

		private void SetupBackForPopup(AbsPopupView absPopup)
		{
			if (listTypeHasSetup.Contains(absPopup.GetType()))
			{
				return;
			}
			
            SetupAutoBack<ShowPanelHomeSignal>(absPopup, typeof(ShowPanelShopSignal));
            SetupAutoBack<ShowPanelHomeSignal>(absPopup, typeof(ShowPanelHeroSignal));
            SetupAutoBack<ShowPanelHomeSignal>(absPopup, typeof(ShowPanelCraftSignal));
//            SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(StartBloodTowerPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(WorldmapPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(CharacterEquipmentPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(UpgradeSkillEquipmentPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(AscensionPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(CraftingPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(StorageInventoryPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(ShopVendorPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(ShopResourcePopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(AchievementPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(ShopCardPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(NewsEventPopup));
//			SetupAutoBack<ShowCharacterEquipmentPopupSignal>(absPopup, typeof(DissembleEquipmentPopup));
//			SetupAutoBack<ReShowWorldMapPopupSignal>(absPopup, typeof(BossModePopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(PetPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(NewCostumePopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(CharacterSelectionPopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(EquipmentEnhanceEvolvePopup));
//			SetupAutoBack<ShowMainScenePopupSignal>(absPopup, typeof(MainArenaPopup));
			
			SetupBackPrevious<ShowPanelHomeSignal>(absPopup, typeof(PanelShopView),
				typeof(PanelHomeView));
			SetupBackPrevious<ShowPanelHomeSignal>(absPopup, typeof(PanelHeroView),
				typeof(PanelHomeView));
			SetupBackPrevious<ShowPanelHomeSignal>(absPopup, typeof(PanelCraftView),
				typeof(PanelHomeView));
//			SetupBackPrevious<ReShowWorldMapPopupSignal>(absPopup, typeof(UpgradeSkillEquipmentPopup),
//				typeof(WorldmapPopup));
//			SetupBackPrevious<ReShowWorldMapPopupSignal>(absPopup, typeof(AscensionPopup),
//				typeof(WorldmapPopup));
//			SetupBackPrevious<ReShowWorldMapPopupSignal>(absPopup, typeof(CraftingPopup),
//				typeof(WorldmapPopup));
//			SetupBackPrevious<ReShowWorldMapPopupSignal>(absPopup, typeof(StorageInventoryPopup),
//				typeof(WorldmapPopup));
//			SetupBackPrevious<ReShowWorldMapPopupSignal>(absPopup, typeof(AchievementPopup),
//				typeof(WorldmapPopup));
//			SetupBackPrevious<ReShowWorldMapPopupSignal>(absPopup, typeof(PetPopup),
//				typeof(WorldmapPopup));
//			SetupBackPrevious<ReShowWorldMapPopupSignal>(absPopup, typeof(NewCostumePopup),
//				typeof(WorldmapPopup));
//			SetupBackPrevious<ReShowWorldMapPopupSignal>(absPopup, typeof(EquipmentEnhanceEvolvePopup),
//				typeof(WorldmapPopup));
//			
//
//			SetupBackPrevious<ReshowBossModePopupSignal>(absPopup, typeof(CharacterEquipmentPopup),
//				typeof(BossModePopup));
////			SetupBackPrevious<ReshowBossModePopupSignal>(absPopup, typeof(ForceScenePopup),
////				typeof(BossModePopup));
//			SetupBackPrevious<ReshowBossModePopupSignal>(absPopup, typeof(UpgradeSkillEquipmentPopup),
//				typeof(BossModePopup));
//			SetupBackPrevious<ReShowWorldMapPopupSignal>(absPopup, typeof(AscensionPopup),
//				typeof(WorldmapPopup));
//			SetupBackPrevious<ReshowBossModePopupSignal>(absPopup, typeof(CraftingPopup),
//				typeof(BossModePopup));
//			SetupBackPrevious<ReshowBossModePopupSignal>(absPopup, typeof(StorageInventoryPopup),
//				typeof(BossModePopup));
//			SetupBackPrevious<ReshowBossModePopupSignal>(absPopup, typeof(AchievementPopup),
//				typeof(BossModePopup));
//			SetupBackPrevious<ReshowBossModePopupSignal>(absPopup, typeof(PetPopup),
//				typeof(BossModePopup));
//			SetupBackPrevious<ReshowBossModePopupSignal>(absPopup, typeof(NewCostumePopup),
//					typeof(BossModePopup));
//			SetupBackPrevious<ReshowBossModePopupSignal>(absPopup, typeof(EquipmentEnhanceEvolvePopup),
//				typeof(BossModePopup));
			
		}

		private bool SetupAutoBack<T>(AbsPopupView absPopup, Type targetType) where T : Signal
		{
			if (absPopup.GetType() == targetType && !autoBackData.ContainsKey(targetType))
			{
				autoBackData.Add(targetType, typeof(T));
				return true;
			}

			return false;
		}

		private bool SetupBackPrevious<T>(AbsPopupView absPopup, Type curPopupType, Type prePopupType) where T:Signal
		{
			if (absPopup.GetType() == curPopupType)
			{
				AddToBackPrePopupSystem<T>(curPopupType, prePopupType);
				return true;
			}

			return false;
		}

		private void SetFullscreenPopup()
		{
			fullScreenPopup.Add(typeof(PanelHomeView));
			fullScreenPopup.Add(typeof(PanelShopView));
			fullScreenPopup.Add(typeof(PanelCraftView));
			fullScreenPopup.Add(typeof(PanelHeroView));
		}

		private void AddToBackPrePopupSystem<T>(Type curPopup, Type prePopup) where T:Signal
		{
			string key = curPopup.ToString();// + "_" + prePopup;
			if (!backData.ContainsKey(key)) backData.Add(key, typeof(T));
		}

		public void ShowPopup(AbsPopupView absPopup,bool addToListShow=true)
		{
			if (!PopupDic.ContainsKey(absPopup.GetType().ToString()))
			{
				PopupDic.Add(absPopup.GetType().ToString(),absPopup);
			}
			else
			{
				PopupDic[absPopup.GetType().ToString()] = absPopup ;
			}
			
			if (fullScreenPopup.Contains(absPopup.GetType()))
			{
				string key = absPopup.GetType().ToString();// + "_" + curPopup;
				if (!listTypeHasSetup.Contains(absPopup.GetType()))
				{
					SetupBackForPopup(absPopup);
				}
				if (backData.ContainsKey(key))
				{
					if(!currentBackData.ContainsKey(absPopup.GetType()))
						currentBackData.Add(absPopup.GetType(), key);
					else
					{
						currentBackData[absPopup.GetType()] = key;
					}
				}

				curPopup = absPopup.GetType();
				for (int i = listPopupShow.Count - 1; i >= 0; i--)
				{
					if (listPopupShow[i] != null && !popupIgnoreDisable.Contains(listPopupShow[i].GetType()))
						listPopupShow[i].Hide();
				}

				listPopupShow.Clear();
			}
			else
			{
				if(absPopup!=null) {
//					CustomSound customSound = absPopup.GetComponent<CustomSound>();
//					if (customSound == null) {
//						SoundManager.instance.PlaySfx(Sfx.ShowPopup);
//					}
//					else {
//						SoundManager.instance.PlaySfx(customSound.sfx);
//					}
				}
			}
			
			if (addToListShow)
				listPopupShow.Add(absPopup);
		}
		
    public AbsPopupView GetPopupByPopupKey(AbsPopupView popup)
    {
        string key = popup.GetType().ToString();
        if (PopupDic.ContainsKey(key))
        {
            return PopupDic[key];
        }
        if (PopupDic.ContainsKey(key))
        {
            return PopupDic[key];
        }

        return null;
    }
    public bool CheckContainPopup(AbsPopupView popup)
    {
        string key = popup.GetType().ToString();
        if (PopupDic.ContainsKey(key))
        {
            return true;
        }
        return false;
    }
		public void DestroyPopup(AbsPopupView absPopup)
		{
			if (currentBackData.ContainsKey(absPopup.GetType()))
			{
				currentBackData.Remove(absPopup.GetType());
			}

			curPopup = null;
		}

		public void DisablePopup(AbsPopupView absPopup, bool showAnotherPopup)
		{
			if (listPopupShow.Contains(absPopup))
			{
				listPopupShow.Remove(absPopup);
				onHidePopup.Invoke(absPopup, showAnotherPopup);
			}

			if (currentBackData.ContainsKey(absPopup.GetType()))
			{
				currentBackData.Remove(absPopup.GetType());
			}
		}

		public void BackPopup(AbsPopupView currentPopup)
		{
			if (currentBackData.ContainsKey(currentPopup.GetType()))
			{
				string key = currentBackData[currentPopup.GetType()];
//				Signal signal = (GlobalData.GetLastestContext().injectionBinder
//					.GetInstance(backData[key]) as Signal);
				Signal signal = (CurrentContext.injectionBinder
					.GetInstance(backData[key]) as Signal);
				signal.Dispatch();
				currentBackData.Remove(currentPopup.GetType());
			}
			else
			{
				if (autoBackData.ContainsKey(currentPopup.GetType()))
				{
//					(GlobalData.GetLastestContext().injectionBinder
//						.GetInstance(autoBackData[currentPopup.GetType()]) as Signal).Dispatch();
					Signal signal = (CurrentContext.injectionBinder
									.GetInstance(autoBackData[currentPopup.GetType()]) as Signal);
					signal.Dispatch();
				}
				else
				{
					currentPopup.Hide();
				}
			}
		}

		public void ForceBackPopup()
		{
			for (int i = listPopupShow.Count-1; i >=0 ; i++)
			{
				var tempPopup = listPopupShow[i];
				if (!tempPopup.EnableBack()) return;
				
				BackPopup(tempPopup);
				listPopupShow.Remove(tempPopup);
				break;
			}
		}
		public bool HasPopupShow()
		{
			return this.listPopupShow.Count > 0;
		}

		public bool IsShowPopup<T>() where T : AbsPopupView
		{
			for (int i = 0; i < listPopupShow.Count; i++)
			{
				if (listPopupShow[i].GetType() == typeof(T))
				{
					return true;
				}
			}

			return false;
		}

		public T GetPopupShow<T>() where T : AbsPopupView {
			for (int i = 0; i < listPopupShow.Count; i++)
			{
				if (listPopupShow[i].GetType() == typeof(T))
				{
					return (T)listPopupShow[i];
				}
			}

			return null;
		}

		public bool GetPopupShow<T>(out AbsPopupView popup) where T : AbsPopupView
		{
			if (IsShowPopup<T>())
			{
				popup = GetPopupShow<T>();
				return true;
			}

			popup = null;
			return false;
		}

		public bool IsLastestPopup(AbsPopupView popup)
		{
			try
			{
				int index = listPopupShow.LastIndexOf(popup);
				return index == listPopupShow.Count - 1;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public void ListenOnHidePopup(Action<AbsPopupView, bool> onHidePopup)
		{
			this.onHidePopup += onHidePopup;
		}

		public void UnListenOnHidePopup(Action<AbsPopupView, bool> onHidePopup)
		{
			this.onHidePopup -= onHidePopup;
		}

		public Type GetCurrentPopup()
		{
			return curPopup;
		}
	}
public enum PanelKey
{
    PanelHome,
    PanelHero,
    PanelCraft,
    PanelShop,
}
public enum PopupKey
{
    Node=0,
    StaminaPopup = 1,
    EquipmentHeroDetailLeft = 2,
    EquipmentHeroDetailRight = 3,
    EquipmentCraftDetailLeft = 4,
    EquipmentCraftDetailRight = 5,
    ShopGoldPopup = 6,
    ShopGemPopup =7,
    ShopGachaPopup =8,
    GachaPopup =9,
    CraftPopup =10,
    GachaInfoPopup =11,
    RevivePopup = 12,
    RewardGameplayPopup = 13,
    RewardPopup=14,
    TooltipPopup=15,
    
    
    
}

public enum SceneKey
{
    Home,
    GamePlay,
}
public enum UILayer
{
    UI1,
    UI2,
    NODE,
    UI3,
    UI4,
}
