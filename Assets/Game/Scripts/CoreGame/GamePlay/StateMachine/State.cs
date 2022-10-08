using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
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
    public virtual void InitState(StateMachineController controller)
    {
        this.controller = controller;
    }
    public virtual void EnterState()
    {
//        foreach (AnimatorControllerParameter p in controller.componentManager.animator.parameters)
//        {
//            if (p.type == AnimatorControllerParameterType.Trigger)
//            {
//                controller.componentManager.animator.ResetTrigger(p.name);
//            }
//        }
        timeTrigger = 0f;
        idEventTrigged = new Dictionary<int, IComboEvent>();
        controller.componentManager.AddImunes(Immunes);
    }
//    public virtual void ResetTrigger()
//    {
//        foreach (AnimatorControllerParameter p in controller.componentManager.animator.parameters)
//        {
//            if (p.type == AnimatorControllerParameterType.Trigger)
//            {
//                controller.componentManager.animator.ResetTrigger(p.name);
//            }
//        }
//
//    }
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
        controller.componentManager.SetSpeed( eventCollectionData[idState].curveSpeedAnimation.Evaluate(timeTrigger));
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

    protected void PlayAnim(string name, AnimationTypeState type , float timestart)
    {
        if (controller.componentManager.animator)
        {
            switch (type)
            {
                case AnimationTypeState.Trigger:
                    controller.componentManager.animator.SetTrigger(name);
                    break;
                case AnimationTypeState.PlayAnim:
                    controller.componentManager.animator.Play(name,0,timestart);
                    break;
            }
        }
    }
    protected void PlayAnim(EventCollection collection)
    {
        if (controller.componentManager.animator)
        {
            switch (collection.typeAnim)
            {
                case AnimationTypeState.Trigger:
                    controller.componentManager.animator.SetTrigger(collection.NameTrigger);
                    break;
                case AnimationTypeState.PlayAnim:
                    controller.componentManager.animator.Play(collection.NameTrigger,0,collection.timeStart);
                    break;
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
    }
    public virtual void OnInputAttack()
    {
    }
    public virtual void OnInputDash()
    {
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

    public virtual void OnInputCounter()
    {
        
    }
}

}



