//using System.Collections;
//using System.Collections.Generic;
//using strange.extensions.command.impl;
//using UnityEngine;
//
//public abstract class AbsShowPanelCmd : Command
//{
//    [Inject] public PopupManager popupManager { get; set; }
//    public override void Execute()
//    {
//        
//    }
//	public T GetInstance<T>() where T : Component
//	{
//		bool isInit = injectionBinder.GetBinding<T>(GetInjectName()) == null ||
//					  injectionBinder.GetInstance<T>(GetInjectName()) == null;
//		if (isInit)
//		{
//			GameObject q = Instantiate();
//			//q.transform.localScale = new Vector3(1, 1, 1);
//			if (injectionBinder.GetBinding<T>(GetInjectName()) != null)
//			{
//				injectionBinder.Unbind<T>(GetInjectName());
//			}
//			injectionBinder.Bind<T>()
//				.ToValue(q.GetComponent<T>())
//				.ToName(GetInjectName());
//		}
//
//		return injectionBinder.GetInstance<T>(GetInjectName());
//	}
//	
//	public virtual string GetInjectName()
//	{
//		return "";
//	}
//	public GameObject Instantiate()
//	{
//		GameObject o = PrefabUtils.LoadPrefab(GetResourcePath());
//		GameObject spawned = null;
//		AbsPopupView typePanel = o.GetComponent<AbsPopupView>();
//		Transform parent = popupManager.GetUILayer(GetUiLayer());
//		if (!popupManager.CheckContainPanel(typePanel.GetType().ToString()))
//        {
//			spawned = GameObject.Instantiate(o, parent) as GameObject;
//
//			popupManager.AddPanel(spawned.GetComponent<AbsPopupView>());
//		}
//        else
//        {
//			if (popupManager.GetPanelByPanelKey(typePanel.GetType().ToString()) == null)
//            {
//				spawned = GameObject.Instantiate(o, parent) as GameObject;
//				popupManager.AddPanel(spawned.GetComponent<AbsPopupView>());
//			}
//            else
//            {
//				spawned = popupManager.GetPanelByPanelKey(typePanel.GetType().ToString()).gameObject;
//            }
//        }
//		
//		return spawned;
//	}
//	public virtual UILayer GetUiLayer()
//	{
//		return UILayer.NODE;
//	}
//	public abstract string GetResourcePath();
//}
