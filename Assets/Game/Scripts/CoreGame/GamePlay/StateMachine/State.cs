using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    public class State : SerializedScriptableObject
    {

        protected StateMachineController controller;
        protected Dictionary<int, IComboEvent> dicEventState = new Dictionary<int, IComboEvent>();
        protected float timeTrigger;
        [ReadOnly]
        public int idState;
        public List<Immune> immunes = new List<Immune>();
        public List<EventCollection> eventCollectionData;
        public virtual void InitState(StateMachineController controller)
        {
            this.controller = controller;
        }
        public virtual void EnterState()
        {
            foreach (AnimatorControllerParameter p in controller.animator.parameters)
            {
                if (p.type == AnimatorControllerParameterType.Trigger)
                {
                    controller.animator.ResetTrigger(p.name);
                }
            }
            timeTrigger = 0f;
            dicEventState = new Dictionary<int, IComboEvent>();
            controller.componentManager.AddImunes(immunes);
        }
        public virtual void ResetTrigger()
        {
            foreach (AnimatorControllerParameter p in controller.animator.parameters)
            {
                if (p.type == AnimatorControllerParameterType.Trigger)
                {
                    controller.animator.ResetTrigger(p.name);
                }
            }

        }
        public virtual void ResetEvent()
        {
            dicEventState.Clear();
            timeTrigger = 0f;
        }
        public virtual void ResetTimeTrigger()
        {
            timeTrigger = 0f;
        }
        public virtual void RecycleEvent()
        {
            foreach (IComboEvent temp in dicEventState.Values)
            {
                temp.Recycle();
            }
        }
        public virtual void UpdateState()
        {
            controller.SetSpeedAnim( eventCollectionData[idState].curveSpeedAnimation.Evaluate(timeTrigger));
            timeTrigger += Time.deltaTime;
            if (eventCollectionData != null && eventCollectionData.Count > idState && idState >= 0)
            {
                if (eventCollectionData[idState].eventCombo != null)
                {
                    foreach (IComboEvent tempComboEvent in eventCollectionData[idState].eventCombo)
                    {
                        if (timeTrigger > tempComboEvent.timeTrigger )
                        {
                            if (!dicEventState.ContainsKey(tempComboEvent.id))
                            {
                                tempComboEvent.OnEventTrigger(controller.componentManager.entity);
                                dicEventState.Add(tempComboEvent.id, tempComboEvent);
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
            controller.componentManager.RemoveImmunes(immunes);
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
    }

}



