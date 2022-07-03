using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameUIController : MonoBehaviour
{
    public static GameUIController instance;
    public Joystick Joystick;
    public CameraFollow cameraFollow;

    public StateMachineController stateMachine;

    public LayerMask maskToolTest;

    public PlayerInput playerInput;

    public Transform HpBarLeft;

    public Transform HpBarRight;

    public Transform WaveContarner;
    
    private bool useRayCastTest;
    private WaveInfoUI prefabWaveUI;
    public GameEntity EntityController;
    public HPBarUI SpawnHPBar(HPBarUI hpBarUi,bool left)
    {
        if (left)
        {
            for(int i = HpBarLeft.childCount-1;i>=0;i--)
            {
                PoolManager.Recycle(HpBarLeft.GetChild(i).gameObject);
            }

            HPBarUI temp = PoolManager.Spawn<HPBarUI>(hpBarUi.gameObject, HpBarLeft);
            temp.Show();
            return temp;
            
        }
        else
        {
            for(int i = HpBarRight.childCount-1;i>=0;i--)
            {
                PoolManager.Recycle(HpBarRight.GetChild(i).gameObject);
            }
            HPBarUI temp = PoolManager.Spawn<HPBarUI>(hpBarUi.gameObject, HpBarRight);
            temp.Show();
            return temp;
        }
    }

    public WaveInfoUI SpawnWaveInfo()
    {
        if (WaveContarner.childCount != 0)
        {
            var temp = WaveContarner.GetChild(0).GetComponent<WaveInfoUI>();
            return temp;
        }
        
        WaveInfoUI spawnWaveUI = PoolManager.Spawn<WaveInfoUI>(prefabWaveUI.gameObject, WaveContarner);
        return spawnWaveUI;
    }
    
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        prefabWaveUI = Resources.Load<WaveInfoUI>("GamePlay/WaveInfoUI");
        
    }
    [Button("MODIFY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void MODIFY()
    {
        if (!stateMachine) return;
        Joystick.componentManager = stateMachine.componentManager;
        if(cameraFollow)
            cameraFollow.player = stateMachine.gameObject;
    }
    [Button("RAYCAST TEST", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void RAYCASTTEST()
    {
        useRayCastTest = true;
    }
    
    private Gamepad gamePad;
    public Vector2 VectorMove;
    private void Update()
    {
        if (gamePad!=null)
        {

            if(gamePad.squareButton.wasPressedThisFrame || gamePad.buttonWest.wasPressedThisFrame)
                Attack();
            if(gamePad.circleButton.wasPressedThisFrame || gamePad.buttonEast.wasPressedThisFrame )
                Dash();
            if( gamePad.triangleButton.wasPressedThisFrame|| gamePad.buttonNorth.wasPressedThisFrame || gamePad.buttonSouth.wasPressedThisFrame || gamePad.crossButton.wasPressedThisFrame)
                Jump();
            if(gamePad.leftShoulder.wasPressedThisFrame || gamePad.leftTrigger.wasPressedThisFrame)
                Skill1();
            if(gamePad.rightShoulder.wasPressedThisFrame || gamePad.rightTrigger.wasPressedThisFrame)
                Skill2();
            Joystick.MoveGamePad(gamePad);
        }
        else
        {
            gamePad = Gamepad.current;
            Joystick.MoveHorizontal(Input.GetAxisRaw("Horizontal"));
        }

        if(Input.GetMouseButtonDown(0) && useRayCastTest )
            RayCastChangeObject();
    }

    public void Dash(InputValue value)
    {
        
    }
    private void Start()
    {
        MODIFY();
        
    }
    public void Jump()
    {
        if(stateMachine)
            stateMachine.OnInputJump();
    }
    public void Dash()
    {
        if(stateMachine)
            stateMachine.OnInputDash();
    }
    public void Attack()
    {
        if(stateMachine)
            stateMachine.OnInputAttack();
    }
    public void Skill1()
    {
        if(stateMachine)
            stateMachine.OnInputSkill(0);
    }
    public void Skill2()
    {
        if(stateMachine)
            stateMachine.OnInputSkill(1);
    }
    public void Skill(int idSkill)
    {
        if(stateMachine)
            stateMachine.OnInputSkill(idSkill);
    }

    public void RayCastChangeObject()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position ,(Camera.main.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) ).normalized , 100f,maskToolTest);
        if(hit.collider != null)
        {
            Debug.Log ("Target Position: " + hit.collider.gameObject);
            if (hit.collider.gameObject.GetComponent<StateMachineController>() != null)
            {
                stateMachine = hit.collider.gameObject.GetComponent<StateMachineController>();
                MODIFY();
            }
        }
        Debug.DrawRay(Camera.main.transform.position , (Camera.main.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) ).normalized  *100f,Color.blue);
    }



}
