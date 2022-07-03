using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfoUI : MonoBehaviour
{
    public Text waveText;
    public Animator animator;
    public string nameAnimShow;

    public void Show(string value)
    {
        waveText.text = value;
        animator.Play(nameAnimShow);
    }

    public void Show(int value)
    {
        waveText.text = "wave "+value;
        animator.Play(nameAnimShow);
    }
}
