using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DamageTextView : MonoBehaviour
{
    public TextMeshPro textMesh;
    public Animator anim;
    public string nameAnim;

    public string text
    {
        set
        {
            textMesh.text = value;
        }
    }

    public Color color 
    {
        set { textMesh.color = value; }
    }

    public void PlayAnim()
    {
        anim.Play(nameAnim,0,0f);
    }

}
