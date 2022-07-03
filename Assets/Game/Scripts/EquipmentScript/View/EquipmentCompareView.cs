using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentCompareView : AbsPopupView
{
    public EquipmentDetailView leftDetailView, rightDetailView;
    private ParameterEquipmentCompare param;
    protected override void Awake()
    {
        base.Awake();
        leftDetailView.ListenerActionOnHide(CheckHide);
        rightDetailView.ListenerActionOnHide(CheckHide);
    }

    protected override void Start()
    {
        base.Start();
    }
    
    
    public override bool EnableBack()
    {
        return true;
    }

    public override void Hide()
    {       
        
        if (leftDetailView.gameObject.activeInHierarchy)
        {
            leftDetailView.Hide();
            return;
        }

        if (rightDetailView.gameObject.activeInHierarchy)
        {
            rightDetailView.Hide();
            return;
        }
        base.Hide();
    }

    void CheckHide()
    {
        bool flag1 = leftDetailView.gameObject.activeInHierarchy;
        bool flag2 = rightDetailView.gameObject.activeInHierarchy;
        if(flag1 && !flag2 || !flag1 && flag2)
            base.Hide();
    }
    protected override void OnShowPopup<T>(T parameter)
    {
        param = parameter as ParameterEquipmentCompare;
        switch (param.compareType)
        {
            case CompareEquipmentType.Left:
                if (param.leftData != null)
                {
                    leftDetailView.SetupData(param.leftData);
                    leftDetailView.ShowButton(param.compareInfo);
                    //leftDetailView.gameObject.SetActive(true);
                    ActionBufferManager.Instance.ActionDelayFrame(
                        delegate
                        {
                            leftDetailView.Show();
                        },1 );
                }
                break;
            case CompareEquipmentType.Right:
                if (param.rightData != null)
                {
                    rightDetailView.SetupData(param.rightData);
                    rightDetailView.ShowButton(param.compareInfo);
                    //rightDetailView.gameObject.SetActive(true);
                    ActionBufferManager.Instance.ActionDelayFrame(
                        delegate
                        {
                            rightDetailView.Show();
                        },1 );
                }
                break;
        }
    }
}
