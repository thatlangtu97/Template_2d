using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.GamePlay;
using UnityEngine;
namespace Core.AI
{
    [TaskCategory("Extension")]
    public class FindTargetTransform : Action
    {
        public SharedComponentManager componentManager;
        public int frameUpdate = 5;
        public int frameCount;
        public override TaskStatus OnUpdate()
        {
            frameCount += 1;
            if (componentManager.Value.enemy != null)
            {
                if (frameCount % frameUpdate != 0)
                {
                    if (Contexts.sharedInstance.game.playerFlagEntity == null)
                    {
                        componentManager.Value.enemy = null;
                        componentManager.Value.stateMachine.ChangeState(NameState.IdleState, 0, true);
                        //componentManager.Value.speedMove = 0;
                        componentManager.Value.vectorMove= Vector2.zero;    
                        return TaskStatus.Failure;
                    }
                }
                return TaskStatus.Success;
            }
            else
            {
                if (frameCount % frameUpdate != 0)
                {
                    return TaskStatus.Failure;
                }
                if (Contexts.sharedInstance.game.playerFlagEntity != null)
                {
                    componentManager.Value.enemy = Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.value.transform;
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Failure;
                }
            }
        }
    }
}
