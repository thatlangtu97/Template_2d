using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextView : MonoBehaviour
{
    public TextMeshPro textMesh;
    public Animator anim;
    public string nameAnim;
    private float timeTrigger;
    
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
//    private void OnEnable()
//    {
//        timeTrigger = 0;
//    }
//    private void Update()
//    {
//        timeTrigger += 0.0167f;
//        if (timeTrigger > timeRecyce)
//        {
//            ObjectPool.Recycle(this.gameObject);
//        }
//    }
}
