using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "AttackState", menuName = "CoreGame/State/AttackState")]
    public class AttackState : State
    {
        public List<float> timeBuffers = new List<float>();
        public List<CheckTarget> checkTargets = new List<CheckTarget>(); 
        private EventCollection currentState;
        private float duration;
        private bool bufferAttack;
        protected override void OnBeforeSerialize()
        {
            if (eventCollectionData == null) return;
            if (eventCollectionData.Count==0 ) return;

            if (timeBuffers.Count < eventCollectionData.Count)
            {
                timeBuffers.Add(0f);
                checkTargets.Add(new CheckTarget());
            }
            else
            {
                if (timeBuffers.Count > eventCollectionData.Count)
                {
                    int index = timeBuffers.Count - 1;
                    timeBuffers.RemoveAt(index);
                    checkTargets.RemoveAt(index);
                }
            }
        }
        public override void EnterState()
        {
            base.EnterState();
            Cast();
        }
        public override void UpdateState()
        {
            base.UpdateState();
            controller.componentManager.rgbody2D.velocity = new Vector2(currentState.curveX.Evaluate(timeTrigger) * controller.transform.right.x,currentState.curveY.Evaluate(timeTrigger) + controller.componentManager.rgbody2D.velocity.y );
            
            if(checkTargets[idState].CanStop(controller.transform))
                controller.componentManager.rgbody2D.velocity = new Vector2(0,currentState.curveY.Evaluate(timeTrigger) + controller.componentManager.rgbody2D.velocity.y);
            
            
            if (timeTrigger >= duration)
            {
                if (bufferAttack)
                {
                    idState = (idState+1)%eventCollectionData.Count;
                    Cast();
                }
                else
                {
                    if (controller.componentManager.IsGround)
                    {
                        if (controller.componentManager.vectorMove != Vector2.zero)
                        {
                            controller.ChangeState(NameState.MoveState);
                            return;
                        }
                        controller.ChangeState(NameState.IdleState);
                    }
                    else
                    {
                        controller.ChangeState(NameState.FallingState);
                    }
                }
            }
        }
        public override void ExitState()
        {
            base.ExitState();
        }
        
        public override void OnInputAttack()
        {
            base.OnInputAttack();
            if (idState >= eventCollectionData.Count - 1)
            {
                bufferAttack = false;
                return;
            }
            bufferAttack = true;
        }

        void Cast()
        {
            controller.componentManager.Rotate();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
            duration = currentState.durationAnimation;
            bufferAttack = false;
            ResetEvent();
        }
    }
    [System.Serializable]
    public class CheckTarget
    {
        public bool useCheck;
        public float distance = 0.1f;
        public LayerMask layerStop;

        public bool CanStop(Transform transform)
        {
             
             RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance, layerStop);
             if (hit.collider.transform != null)
             {
                 return true;
             }
             else
             {
                 return false;
             }
        }
    }
}

