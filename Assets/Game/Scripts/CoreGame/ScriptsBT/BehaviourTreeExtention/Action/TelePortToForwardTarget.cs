using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class TelePortToForwardTarget : Action
    {
        public SharedComponentManager component;
        public SharedTransform target;
        public Vector3 foward;
        public LayerMask layerLimit;

        public override void OnStart()
        {
            base.OnStart();

            RaycastHit2D hit = Physics2D.Raycast(target.Value.position, target.Value.right,
                Vector2.Distance(target.Value.position, target.Value.position + foward), layerLimit);
            if (hit.collider == null)
            {
                component.Value.transform.position = target.Value.position +
                                                     new Vector3(target.Value.right.x * foward.x,
                                                         target.Value.right.y * foward.y);
            }
            else
            {
                component.Value.transform.position = new Vector3(hit.point.x, target.Value.position.y);
            }
                
        }
    }


}
