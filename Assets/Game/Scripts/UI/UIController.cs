using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerController controller;

    public Button btnAttack,btnJump,btnDash, btnCounter;

    public void Awake()
    {
        btnAttack.onClick.AddListener( () =>controller.Attack());
        btnJump.onClick.AddListener( () =>controller.Jump());
        btnDash.onClick.AddListener( () =>controller.Dash());
        btnCounter.onClick.AddListener( () =>controller.Counter());
    }
}
