using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevivePopup : AbsPopupView
{
    public Button reviveBtn;
    public Button closeBtn;
    protected override void Awake()
    {
        base.Awake();
        reviveBtn.onClick.AddListener(Revive);
        closeBtn.onClick.AddListener(Hide);
    }

//    public override void ShowPopupByCmd()
//    {
//        base.ShowPopupByCmd();
//        //this.gameObject.SetActive(true);
//    }

    public override bool EnableBack()
    {
        return true;
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        throw new System.NotImplementedException();
    }

    public void Revive()
    {
        Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.value.OnInputRevive();
        Hide();
    }
    
}
