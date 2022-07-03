using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace Core.AI
{
    [TaskCategory("Extension")]
    public class CheckInRangeWorld : Conditional
    {
        public SharedFloat rangeToEnemy;
        public float distance;
        public override TaskStatus OnUpdate()
        {
            if (rangeToEnemy.Value < distance )
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