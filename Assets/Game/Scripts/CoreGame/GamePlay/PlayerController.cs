using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 vMove;

    public StateMachineController controller;
    public void MoveInput(InputAction.CallbackContext context)
    {
        vMove = context.ReadValue<Vector2>();
        
        if (controller)
        {
            controller.componentManager.vectorMove = vMove;
            if(vMove!=Vector2.zero)
                controller.OnInputMove();
        }
    }

    public void AttackInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            if (controller)
            {
                controller.OnInputAttack();
            }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            if (controller)
            {
                controller.OnInputJump();
            }
    }
    public void DashInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            if (controller)
            {
                controller.OnInputDash();
            }
    }  
    public void SkillInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            if (controller)
            {
                controller.OnInputSkill(0);
            }
    }  
    
    public void CounterInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            if (controller)
            {
                controller.OnInputCounter();
            }
    }  
}
