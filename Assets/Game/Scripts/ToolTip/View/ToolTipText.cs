using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipText : View
{
    private static ToolTipText instance;
    public static ToolTipText Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject ToolTip = Instantiate(Resources.Load<GameObject>("ToolTipText"));
                instance = ToolTip.GetComponent<ToolTipText>();
                DontDestroyOnLoad(ToolTip);
            }

            return instance;
        }
        
    }

    public string nameAnimAuto;
    public string nameAnimShow;
    public string nameAnimHide;
    public Text text;
    public Animator animator;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    public void Show(string textString)
    {
        text.text = textString;
        animator.Play(nameAnimAuto,0,0);
    }
}
