using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
public class State : SerializedScriptableObject
{
    [SerializeField]
    protected StateMachineController controller;
    protected Dictionary<int, IComboEvent> idEventTrigged = new Dictionary<int, IComboEvent>();
    [SerializeField]
    protected float timeTrigger;
    [ReadOnly]
    public int idState;
    public List<Immune> Immunes = new List<Immune>();
    
    public List<EventCollection> eventCollectionData;
    [SerializeField]
    protected GameEntity entity;
    protected bool bufferJump,bufferAttack,bufferDash;

    protected virtual void ResetBuffer()
    {
        bufferJump = false;
        bufferAttack = false;
        bufferDash = false;
    }
    public virtual void InitState(StateMachineController controller, ComponentManager componentManager)
    {
        this.controller = controller;
        entity = componentManager.entity;
    }
    public virtual void EnterState()
    {
//        foreach (AnimatorControllerParameter p in controller.animator.parameters)
//        {
//            if (p.type == AnimatorControllerParameterType.Trigger)
//            {
//                controller.animator.ResetTrigger(p.name);
//            }
//        }


            foreach (var VARIABLE in controller.nameTrigger)
            {
                
                 controller.animator.ResetTrigger(VARIABLE);
            }
        timeTrigger = 0f;
        idEventTrigged = new Dictionary<int, IComboEvent>();
        controller.componentManager.AddImunes(Immunes);
        ResetBuffer();
    }
    public virtual void ResetTrigger()
    {
//        foreach (AnimatorControllerParameter p in controller.animator.parameters)
//        {
//            if (p.type == AnimatorControllerParameterType.Trigger)
//            {
//                controller.animator.ResetTrigger(p.name);
//            }
//        }
        foreach (var VARIABLE in controller.nameTrigger)
        {
            controller.animator.ResetTrigger(VARIABLE);
        }
            
    }
    public virtual void ResetEvent()
    {
        idEventTrigged.Clear();
        timeTrigger = 0f;
    }
    public virtual void ResetTimeTrigger()
    {
        timeTrigger = 0f;
    }
    public virtual void RecycleEvent()
    {
        foreach (IComboEvent temp in idEventTrigged.Values)
        {
            temp.Recycle();
        }
    }
    public virtual void UpdateState()
    {
        controller.SetSpeed( eventCollectionData[idState].curveSpeedAnimation.Evaluate(timeTrigger));
        timeTrigger += Time.deltaTime;
        if (eventCollectionData != null && eventCollectionData.Count > idState && idState >= 0)
        {
            if (eventCollectionData[idState].EventCombo != null)
            {
                foreach (IComboEvent tempComboEvent in eventCollectionData[idState].EventCombo)
                {
                    if (timeTrigger > tempComboEvent.timeTrigger )
                    {
                        if (!idEventTrigged.ContainsKey(tempComboEvent.id))
                        {
                            tempComboEvent.OnEventTrigger(controller.componentManager.entity);
                            idEventTrigged.Add(tempComboEvent.id, tempComboEvent);
                        }
                        else
                        {
                            tempComboEvent.OnUpdateTrigger();
                        }
                    }
                }
            }
        }

        
    }
    public virtual void ExitState()
    {
        RecycleEvent();
        controller.componentManager.RemoveImmunes(Immunes);
    }
    public virtual void OnInputMove()
    {
    }
    public virtual void OnInputJump()
    {
        ResetBuffer();
        bufferJump = true;
    }
    public virtual void OnInputAttack()
    {
        ResetBuffer();
        bufferAttack = true;
    }
    public virtual void OnInputDash()
    {
        ResetBuffer();
        bufferDash = true;
    }
    public virtual void OnInputSkill(int idSkill)
    {
    }
    public virtual void OnHit()
    {
    }
    public virtual void OnRevive()
    {
    }
}


