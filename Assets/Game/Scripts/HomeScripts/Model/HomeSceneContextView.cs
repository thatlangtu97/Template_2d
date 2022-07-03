using System;
using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using EntrySystem;
using UnityEngine;

public class HomeSceneContextView : ContextView
{
    private void Awake()
    {
//        if (EntryContextView.Instance != null)
          //        {
          //            //EntryContextView.Instance.loadFlashScene = false;
          //        }
          //        
#if UNITY_EDITOR
          EntryContextView.Instance.loadFlashScene = false;
#endif

        //InitUI();
    }
//    public void InitUI()
//    {
//        GameObject UI1 = Resources.Load<GameObject>(GameResourcePath.UI1);
//        GameObject UI2 = Resources.Load<GameObject>(GameResourcePath.UI2);
//        GameObject UI3 = Resources.Load<GameObject>(GameResourcePath.UI3);
//        GameObject UI4 = Resources.Load<GameObject>(GameResourcePath.UI4);
//        UIBasePanel tempUI1 = Instantiate(UI1).GetComponent<UIBasePanel>();
//        UIBasePanel tempUI2 = Instantiate(UI2).GetComponent<UIBasePanel>();
//        UIBasePanel tempUI3 =Instantiate(UI3);.GetComponent<UIBasePanel>();
//        UIBasePanel tempUI4 =Instantiate(UI4);.GetComponent<UIBasePanel>();
//        
//        Debug.Log("Init UI");
//    }

    void Start()
    {
        context = new HomeSceneContext(this);
        context.Start();
        if (PlayFlashScene.instance != null)
        {
            PlayFlashScene.instance.HideLoading();
        }
    }
}
