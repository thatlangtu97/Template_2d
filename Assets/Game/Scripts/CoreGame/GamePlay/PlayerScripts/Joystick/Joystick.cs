using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Unity.Collections;
using UnityEngine.InputSystem;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    public Image BackGround, PointJoystick;
    public Vector3 posStart, posEnd, ForceVector;
    public PlayerController controller;
    [Range(1.5f, 4f)]
    public float space =2.5f;
    public Transform effectLook;
    void Start()
    {
        posStart = Vector3.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        Vector2 sizeDelta;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BackGround.rectTransform
                                                                    , eventData.position
                                                                    , eventData.pressEventCamera
                                                                    , out pos))
        {
            sizeDelta = BackGround.rectTransform.sizeDelta;
            pos.x = (pos.x / sizeDelta.x);
            pos.y = (pos.y / sizeDelta.y);
            posEnd = new Vector3(pos.x * 2 , pos.y * 2);
            posEnd = (posEnd.magnitude >= 1f) ? posEnd.normalized : posEnd;
            ForceVector = new Vector3(posEnd.x, posEnd.y,0f);
            PointJoystick.rectTransform.anchoredPosition = new Vector3((posEnd.x *sizeDelta.x) / space, (posEnd.y * sizeDelta.y) / space);
            if (effectLook)
            {
                effectLook.gameObject.SetActive(true);
                effectLook.up = new Vector3(ForceVector.x, ForceVector.y, 0);
            }

            OnMove();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        posEnd = Vector3.zero;
        ForceVector = Vector3.zero;
        PointJoystick.rectTransform.anchoredPosition = posEnd;
        if (effectLook)
            effectLook.gameObject.SetActive(false);
        OnStop();
    }

    public Vector2 GetValue
    {
        get
        {
            return ForceVector;
        }
    }
    void OnMove()
    {
        if (controller)
        {
            controller.Move(GetValue);
        }
    }
    void OnStop()
    {
        if (controller)
        {
            controller.Move(GetValue);
        }
    }
}
