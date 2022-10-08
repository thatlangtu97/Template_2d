using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class FindWayPatrol : Action
    {
        public SharedComponentManager componentManager;
        public float range;

        public Vector2 Pos1;
        public Vector2 Pos2;

        public LayerMask layer;
        
        public override void OnStart()
        {
            base.OnStart();
            Transform transform = componentManager.Value.transform;
            Pos1 = transform.position - new Vector3(range,0f);
            Pos2 = transform.position + new Vector3(range,0f);
//            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.left , range ,layer);
//            if (hit1 == null)
//            {
//                Pos1 = transform.position - new Vector3(range,0f);
//            }
//            else
//            {
//                Pos1 = hit1.point;
//            }
//            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right , range ,layer);
//            if (hit2 == null)
//            {
//                Pos2 = transform.position + new Vector3(range,0f);
//            }
//            else
//            {
//                Pos2 = hit2.point;
//            }

        }
    }
}