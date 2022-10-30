using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using Core.GamePlay;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Vector2 vMove;
    public ProCamera2D camera2D;
    public float distance;
    public StateMachineController controller;

    public void Awake()
    {
        instance = this;
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        vMove = context.ReadValue<Vector2>();
//        camera2D.CameraTargets[0].TargetTransform = controller.transform;
//        if (controller)
//        {
//            controller.componentManager.vectorMove = vMove;
//            if(vMove!=Vector2.zero)
//                controller.OnInputMove();
//        }
//
//        if (camera2D && controller)
//        {
//            float value = 0;
//            value = controller.transform.right.x * distance;
//
//            camera2D.OverallOffset = new Vector2(value,0);
//        }
        Move(vMove);
    }

    public void AttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
            Attack();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            Jump();
    }
    public void DashInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            Dash();
    }

    public void CounterInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            Counter();
    }

    public void Move(Vector2 valueJoystick)
    {
        vMove = valueJoystick;
        camera2D.CameraTargets[0].TargetTransform = controller.transform;
        if (camera2D && controller)
        {
            float value = 0;
            value = controller.transform.right.x * distance;

            camera2D.OverallOffset = new Vector2(value,0);
        }
        if (controller)
        {
            controller.componentManager.vectorMove = vMove;
            if(vMove!=Vector2.zero)
                controller.OnInputMove();
        }


    }

    private void FixedUpdate()
    {
        if (camera2D && controller)
        {
            float value = 0;
            value = controller.transform.right.x * distance;

            camera2D.OverallOffset = new Vector2(value,0);
        }
    }

    public void Attack()
    {
        if (controller)
        {
            controller.OnInputAttack();
        }
    }

    public void Dash()
    {
        if (controller)
        {
            controller.OnInputDash();
        }
    }

    public void Counter()
    {
        if (controller)
        {
            controller.OnInputCounter();
        }
    }

    public void Jump()
    {
        if (controller)
        {
            controller.OnInputJump();
        }
    }
}
