using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardGameplayPopup : AbsPopupView
{
    
    
    public Button backToHomeBtn;
    public Button ReloadBtn;
    protected override void Awake()
    {
        base.Awake();
        backToHomeBtn.onClick.AddListener(BackToHome);
        ReloadBtn.onClick.AddListener(Reload);
    }

    public void BackToHome()
    {
        ComponentManagerUtils.ResetAll();
        PoolManager.DestroyAllEntity();
        Contexts.sharedInstance.game.DestroyAllEntities();
        Contexts.sharedInstance.Reset();
        
        Action action = delegate
        {
            PlayFlashScene.instance.Loading("HomeScene",1.2f,null);
        };
        ActionBufferManager.Instance.ActionDelayFrame(action,1);
    }

    public void Reload()
    {
        ComponentManagerUtils.ResetAll();
        PoolManager.DestroyAllEntity();
        Contexts.sharedInstance.game.DestroyAllEntities();
        Scene scene = SceneManager.GetActiveScene();
        
        Action action = delegate
        {
            PlayFlashScene.instance.Loading(scene.name,1.2f,null);
        };
        ActionBufferManager.Instance.ActionDelayFrame(action,1);
    }

    public override bool EnableBack()
    {
        return true;
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        throw new NotImplementedException();
    }
}