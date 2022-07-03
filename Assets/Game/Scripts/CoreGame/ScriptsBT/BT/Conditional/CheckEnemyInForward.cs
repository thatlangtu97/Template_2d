using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Sirenix.OdinInspector;

namespace CoreBT
{
    [TaskCategory("Extension")]
    public class CheckEnemyInForward : Conditional
    {
        public SharedComponentManager componentManager;
//        public override void OnStart()
//        {
//            base.OnStart();
//        }

        public override TaskStatus OnUpdate()
        {
            if (componentManager.Value.enemy)
            {
                if (componentManager.Value.transform.localScale.x > 0 && componentManager.Value.enemy.transform.position.x > componentManager.Value.transform.position.x
                    ||
                    componentManager.Value.transform.localScale.x < 0 && componentManager.Value.enemy.transform.position.x < componentManager.Value.transform.position.x
                    )
                {
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Failure;
                }
            }
            else
            {
                return TaskStatus.Success;
            }
        }
    }
}