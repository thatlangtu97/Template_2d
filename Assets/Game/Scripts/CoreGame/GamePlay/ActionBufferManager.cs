using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ActionBufferManager : MonoBehaviour
{
    public static ActionBufferManager Instance
    {            
        get
        {
            if(instance == null)
            {
                GameObject temp = new GameObject("ActionBufferManager",typeof(ActionBufferManager));
                instance = temp.GetComponent<ActionBufferManager>();
            }
            return instance;
        }    
    }
    private static ActionBufferManager instance;
    
    
    
    CompositeDisposable _disposable;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        _disposable = new CompositeDisposable();
    }

    public void ActionDelayTime(Action action ,float timedelay)
    {
        Observable.Timer(TimeSpan.FromSeconds(timedelay)).Subscribe(l => { action.Invoke(); }).AddTo(_disposable);
    }

    public void ActionDelayFrame(Action action, int frameDelay)
    {
        Observable.TimerFrame(frameDelay,FrameCountType.Update).Subscribe(l => { action.Invoke(); }).AddTo(_disposable);
    }
}
