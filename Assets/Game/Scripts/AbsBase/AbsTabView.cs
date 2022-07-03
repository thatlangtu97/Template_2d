using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsTabView<T> : View where T : struct, IComparable, IFormattable, IConvertible
{
    public Image backGroundTab;
    public Text[] textTab;
    public T tabType;
    public Action<T> onSelect = delegate (T action) { };
    public bool isSelect = false;
    public bool isLock = false;
    public void Init(T tabType, Action<T> onSelect) {
        this.tabType = tabType;
        this.onSelect += onSelect;
        OnInit();
        GetComponent<Button>().onClick.AddListener(delegate
        {
            this.onSelect.Invoke(this.tabType);
            //Select();
        } );
        textTab = GetComponentsInChildren<Text>();
    }
    public void OnChangeTab(T type)
    {
        isSelect = this.tabType.GetHashCode() == type.GetHashCode();
//        if (isSelect)
//        {
//            //Debug.Log("change Tab "+type);
//            //onSelect.Invoke(tabType);
//            this.onSelect.Invoke(tabType);
//        }
//        Debug.Log($"change Tab {type} {isSelect}" );
        onChange(isSelect);
    }
    protected virtual void Select() {
        OnChangeTab(tabType);
    }
    protected abstract void OnMapValue();
    protected virtual void OnInit(){}
    public virtual void OnShow(){}
    
    public virtual void onChange(bool isSelect) {}
    
    public virtual void ShowHighlight(T tabtype) { }
}
