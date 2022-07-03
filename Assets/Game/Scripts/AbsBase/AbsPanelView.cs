//using System.Collections;
//using System.Collections.Generic;
//using strange.extensions.mediation.impl;
//using UnityEngine;
//
//public abstract class AbsPanelView : View
//{
//	[Inject]
//	public PopupManager popupManager { get; set; }
//	public UILayer uILayer;
//	public PanelKey panelKey;
//	AutoFIllPanelInParent autoFIllPanelInParent;
//	public UiViewController UiViewController;
//	public ParameterPopup parameterPopup;
//
//	public void SetParameter(ParameterPopup parameterPopup)
//	{
//		this.parameterPopup = parameterPopup;
//	}
//
//	protected override void Start()
//	{
//		base.Start();
////		transform.parent = popupManager.GetUILayer(uILayer);
////		autoFIllPanelInParent = GetComponent<AutoFIllPanelInParent>();
////		autoFIllPanelInParent.AutoFill();
//	}
////	public virtual void ShowPanelByCmd() 
////	{
////		base.CopyStart();
////		//this.gameObject.SetActive(true);
////		//NotifyShowPanel();
////		popupManager.ShowPanel(this);
////		
////	}
////	public virtual void ShowPanel()
////    {
////		UiViewController.Show();
////	}
////	public void HidePanel()
////    {
////		UiViewController.Hide();
////	}
////
////	public virtual void NotifyShowPanel()
////	{
////		
////	}
////
////	public virtual void SetUpGamePad()
////	{
////		
////	}
////	
////	public virtual bool EnableBack()
////	{
////		return true;
////	}
////	public void ShowPopup<T>(T parameter) where T : ParameterPopup
////	{
//////		this.WaitUntilFinshRegister(delegate
//////		{
//////			OnBeforeShowPopup(parameter);
////		NotifyShowPanel();
//////			NGUITools.SetActiveSelf(this.gameObject,true);
////		OnShowPopup(parameter);
////		popupManager.ShowPanel(this);
//////		});
////	}
////	protected abstract void OnShowPopup<T>(T parameter) where T : ParameterPopup;
//}
