using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFPS : MonoBehaviour
{

    private float frameTime;
    private int frame;
    public Text txtFPS;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void CalculateFPS()
    {
        frameTime += Time.deltaTime;
        ++frame;
        if (frameTime >= 1)
        {
            txtFPS.text = "" + frame;
            frame = 0;
            frameTime = 0;
        }
    }

    private void Update()
    {
        CalculateFPS();
    }
}
