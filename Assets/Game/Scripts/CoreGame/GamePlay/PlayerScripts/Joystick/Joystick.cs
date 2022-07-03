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
    //public GameObject gameinitObj;

    //public int deviceId;
    [SerializeField]
    public Image BackGround, PointJoystick;
    public Vector3 posStart, posEnd, ForceVector;
    [Range(1.5f, 4f)]
    public float space =2.5f;
    public Transform effectLook;
    public ComponentManager componentManager;
    // GAME PAD
    private Gamepad gamePad;
    public Vector2 VectorMove;
    //
    private bool isPress = false;
    
    IEnumerator delayActive(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(true);
    }
    void Start()
    {
        posStart = Vector3.zero;
        gamePad = Gamepad.current;
        

    }
    public void OnDrag(PointerEventData eventData)
    {
        isPress = true;
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
            effectLook.gameObject.SetActive(true);
            effectLook.up = new Vector3(ForceVector.x, ForceVector.y,0);
            OnMove();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        posEnd = Vector3.zero;
        ForceVector = Vector3.zero;
        PointJoystick.rectTransform.anchoredPosition = posEnd;
        effectLook.gameObject.SetActive(false);
        OnStop();
    }
    void OnMove()
    {
        if (componentManager != null)
        {
            if (ForceVector.x > 0)
            {
                componentManager.speedMove = componentManager.maxSpeedMove;
            }
            if (ForceVector.x < 0)
            {
                componentManager.speedMove = -componentManager.maxSpeedMove;
            }

            componentManager.vectorSpeed = ForceVector;
        }
    }
    void OnStop()
    {
        if (componentManager != null)
        {
            componentManager.speedMove = 0f;
        }
    }

//    private void Update()
//    {
//        LeftStick(gamePad);
//    }

    public void LeftStick(Gamepad gamePad)
    {
        if (gamePad!=null )
        {
            Vector2 sizeDelta = BackGround.rectTransform.sizeDelta;
                VectorMove = gamePad.leftStick.ReadValue();
                PointJoystick.rectTransform.anchoredPosition = new Vector3((VectorMove.x * sizeDelta.x) / space,
                    (VectorMove.y * sizeDelta.y) / space);
                ForceVector = VectorMove;
                if (gamePad.leftStick.IsPressed())
                    OnMove();
                else
                {
                    OnStop();
                }
        }
    }

    public void DPad(Gamepad gamePad)
    {
        if (gamePad!=null)
        {
            Vector2 sizeDelta = BackGround.rectTransform.sizeDelta;
                VectorMove = gamePad.dpad.ReadValue();
                PointJoystick.rectTransform.anchoredPosition = new Vector3((VectorMove.x * sizeDelta.x) / space,
                    (VectorMove.y * sizeDelta.y) / space);
                ForceVector = VectorMove;
                if (gamePad.dpad.IsPressed())
                    OnMove();
                else
                {
                    OnStop();
                    PointJoystick.rectTransform.anchoredPosition = Vector3.zero;
                }

        }
    }

    public void MoveGamePad(Gamepad gamePad)
    {
        if (!isPress)
        {
            if (gamePad != null)
            {
                Vector2 sizeDelta = BackGround.rectTransform.sizeDelta;
                VectorMove = gamePad.dpad.ReadValue();
                PointJoystick.rectTransform.anchoredPosition = new Vector3((VectorMove.x * sizeDelta.x) / space,
                    (VectorMove.y * sizeDelta.y) / space);
                ForceVector = VectorMove;
                if (gamePad.dpad.IsPressed())
                    OnMove();
                else
                {
                    VectorMove = gamePad.leftStick.ReadValue();
                    PointJoystick.rectTransform.anchoredPosition = new Vector3((VectorMove.x * sizeDelta.x) / space,
                        (VectorMove.y * sizeDelta.y) / space);
                    ForceVector = VectorMove;
                    if (gamePad.leftStick.IsPressed())
                        OnMove();
                    else
                    {
                        OnStop();
                        PointJoystick.rectTransform.anchoredPosition = Vector3.zero;
                    }
                }
            }
        }
    }

    public void MoveHorizontal(float horizontal)
    {
        if (!isPress)
        {
            Vector2 sizeDelta = BackGround.rectTransform.sizeDelta;
            //float horizontal = Input.GetAxis("Horizontal");
            VectorMove = new Vector2(horizontal, 0f);
            PointJoystick.rectTransform.anchoredPosition = new Vector3((VectorMove.x * sizeDelta.x) / space,
                (VectorMove.y * sizeDelta.y) / space);
            ForceVector = VectorMove;
            if (horizontal != 0)
            {
                OnMove();
            }
            else
            {
                OnStop();
                PointJoystick.rectTransform.anchoredPosition = Vector3.zero;
            }
        }
    }
}
