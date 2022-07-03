using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupRewardView : AbsPopupView
{
    List<AbsRewardLogic> listReward = new List<AbsRewardLogic>();
    public List<ItemView> listItemView= new List<ItemView>();
    public Button btnClose;
    protected override void Awake()
    {
        base.Awake();
        InitItemView();
        btnClose.onClick.AddListener(Hide);
    }

    public override bool EnableBack()
    {
        return true;
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        listReward = (parameterPopup as ShowPopupRewardParameter).listRewardLogics;
        Show();
    }

    public void InitItemView()
    {
        
    }

    public void Show()
    {
        for (int i = 0; i < listItemView.Count; i++)
        {
            if(i<listReward.Count)
                listItemView[i].Show(listReward[i]);
            else
            {
                listItemView[i].Hide();
            }
        }
    }
}
