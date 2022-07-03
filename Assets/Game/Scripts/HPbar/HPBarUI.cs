using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Image slider;
    public Image sliderAfter;
    public Text textValue;

    public float timelerp;
    public AnimationCurve curveLerp;
    public Animator animator;
    public string nameAnimShow,nameAnimHide;
    public Transform parentObject;
    public void Setvalue(GameEntity e ,float value, float maxvalue)
    {
        parentObject = e.stateMachineContainer.value.transform;
        slider.fillAmount = value / maxvalue;
        textValue.text = $"{(int) value} / {(int) maxvalue}";
        if (value <= 0 && animator)
        {
            animator.Play(nameAnimHide);
        }
    }
    
    public void Setvalue(GameEntity e ,int value, int maxvalue)
    {
        parentObject = e.stateMachineContainer.value.transform;
        if (slider.fillAmount <= 0f)
        {
            if (value > 0 && animator)
            {
                animator.Play(nameAnimShow);
                slider.fillAmount = (float)value / (float)maxvalue;
                textValue.text = $"{value} / {maxvalue}";
                return;
            }
        }
        slider.fillAmount = (float)value / (float)maxvalue;
        textValue.text = $"{value} / {maxvalue}";
        if (value <= 0 && animator)
        {
            if(animator)
                animator.Play(nameAnimHide);
        }

        
    }

    private void OnEnable()
    {
//        slider.fillAmount = 1f;
//        sliderAfter.fillAmount = 1f;
        if(animator)
            animator.Play(nameAnimShow);
    }

    public void Show()
    {
        if(animator)
            animator.Play(nameAnimShow);
    }
    public void Update()
    {
        if (parentObject == null || parentObject.gameObject.activeInHierarchy == false)
        {
            if(animator)
                animator.Play(nameAnimHide);
            return;
        }
        if (sliderAfter.fillAmount > slider.fillAmount)
        {
            timelerp = Mathf.Clamp(timelerp +Time.deltaTime, 0f, 1f);
            sliderAfter.fillAmount = Mathf.Lerp(sliderAfter.fillAmount, slider.fillAmount, curveLerp.Evaluate(timelerp));
        }
        else
        {
            timelerp = 0;
            sliderAfter.fillAmount = slider.fillAmount;
        }
    }
}
