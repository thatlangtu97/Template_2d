using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsTabController <T1, T2> where T2 : AbsTabView<T1> where T1 : struct, IComparable, IFormattable, IConvertible
{
    private List<TabInitInfo<T1>> tabInitInfos;
    private List<GameObject> tabGameObjects;
    private T1 curSelect = new T1();
    private List<T2> tabViews = new List<T2>();
    public AbsTabController( List<GameObject> tabGameObjects, List<TabInitInfo<T1>> tabInitInfos)
    {
        this.tabInitInfos = tabInitInfos;
        this.tabGameObjects = tabGameObjects;

        for (int i = 0; i < tabInitInfos.Count; i++)
        {
            GameObject o = tabGameObjects[i];
            T2 absTab = o.AddComponent<T2>();
//            TabViewCacheMonoBehaviour data = o.GetComponent<TabViewCacheMonoBehaviour>();
//            absTab.MapValue(data);
            tabViews.Add(absTab);
            tabViews[i].Init(tabInitInfos[i].Type, delegate(T1 obj)
            {
                OnSelect(obj);
                Show();
            });
//            tabViews[i].ShowHighlight(tabInitInfos[i].Type);
//
//            tabViews[i].toggle.@group = ToogleGroup();
//            tabViews[i].Lock(tabInitInfos[i].IsLock);
//            FontSize[] fontSizes = tabViews[i].GetComponentsInChildren<FontSize>(true);
//            foreach (FontSize fontSize in fontSizes)
//            {
//                fontSize.textType = TexType();
//            }
        }
    }
    public void SetTabInit(T1 tab)
    {
        curSelect = tab;
    }
    public void Show()
    {
        
        SelectTab(curSelect);
        foreach (var tabView in tabViews)
        {
            tabView.OnShow();
        }
        OnShow();
    }
    protected virtual void OnShow(){}
    public void SelectTab(T1 type)
    {
        for (int i = 0; i < tabViews.Count; i++)
        {
            tabViews[i].OnChangeTab(type);
        }
    }
    void OnSelect(T1 type)
    {
        curSelect = type;
        foreach (TabInitInfo<T1> tabInitInfo in tabInitInfos)
        {
            if (tabInitInfo.Type.GetHashCode() == type.GetHashCode())
            {
                tabInitInfo.GetCallBack.Invoke(type);
            }
        }
        
    }
}
