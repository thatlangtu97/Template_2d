using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.impl;
using UnityEngine;

public class GamePlayContextView : ContextView
{
    private static GamePlayContextView instance;
    public static GamePlayContextView Instance
    {
        get
        {
            if (instance == null)
            {

                GameObject Entry = Instantiate( Resources.Load<GameObject>("GamePlayContextView"));
                instance = Entry.GetComponent<GamePlayContextView>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
        context = new GamePlayContext(this);
        context.Start();
    }

    public void Load()
    {
        
    }
}
