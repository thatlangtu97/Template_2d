using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace Core.AI
{
    [TaskCategory("Extension")]
    public class CheckInRangeToAction : Conditional
    {
        public SharedFloat rangeToEnemy;
        public SharedComponentManager componentManager;
        public float maxDistance;
        public float minDistance;
        public float spaceY;
        public override TaskStatus OnUpdate()
        {
            if (rangeToEnemy.Value < maxDistance && rangeToEnemy.Value>minDistance &&  (componentManager.Value.transform.position.y + spaceY) > componentManager.Value.enemy.position.y)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}
