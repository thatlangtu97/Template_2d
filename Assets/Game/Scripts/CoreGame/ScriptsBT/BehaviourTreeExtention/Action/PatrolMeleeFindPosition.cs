using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEditor.U2D.Common;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class PatrolMeleeFindPosition : Action
    {
        
        public SharedVector2 patrolTargetPosition;
        public SharedVector2 spawnPosition;
        public SharedComponentManager copmponent;
        public LayerMask layerLimit;
        public float spaceStep;
        public float range;
        public float distanceDown;
        public Vector2 basePos;
        
        public override void OnStart()
        {
            
        }

        public override TaskStatus OnUpdate()
        {
//            RaycastHit2D hit =  Physics2D.Raycast(spawnPosition.Value, Vector2.down, 100f, layerLimit);
//            basePos = hit.point;
//            float current = 0;
//            while (current<range)
//            {
//                RaycastHit2D hitLeft =  Physics2D.Raycast(spawnPosition.Value + new Vector2(current,0f), Vector2.down, 100f, layerLimit);
//                RaycastHit2D hitRight =  Physics2D.Raycast(spawnPosition.Value + new Vector2(-current,0f), Vector2.down, 100f, layerLimit);
//                
//
//                if (hitLeft.collider.gameObject != null)
//                {
//                    Gizmos.DrawRay(spawnPosition.Value + new Vector2(current,0f),Vector2.down );
//                }
//                if (hitRight.collider.gameObject != null)
//                {
//                    Gizmos.DrawRay(spawnPosition.Value + new Vector2(-current,0f),Vector2.down );
//                }
//                current += spaceStep;
//            }

            return TaskStatus.Running;
        }

        public GameObject leftObj, rightObj;
        public bool left = false;
        public bool right = false;
        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            float current = 0;

            if (copmponent == null) return;
            
//            spawnPosition.Value = (Vector2)copmponent.Value.transform.position;
            basePos = (Vector2) copmponent.Value.transform.position;
            while (current<range)
            {
                RaycastHit2D hitLeft =  Physics2D.Raycast(basePos + new Vector2(-current,0f), Vector2.down, distanceDown, layerLimit);
                RaycastHit2D hitRight =  Physics2D.Raycast(basePos + new Vector2(current,0f), Vector2.down, distanceDown, layerLimit);

                RaycastHit2D hitWallLeft =  Physics2D.Raycast(basePos , Vector2.left, current, layerLimit);
                RaycastHit2D hitWallRight =  Physics2D.Raycast(basePos , Vector2.right, current, layerLimit);
                
                if (hitWallLeft.collider != null)
                {
                    leftObj = hitWallLeft.collider.gameObject;
                    
                    left = true;
                }
                else
                {
                    leftObj = null;
                    left = false;
                }

                if (hitWallRight.collider != null)
                {
                    rightObj = hitWallRight.collider.gameObject;
                    right = true;
                }
                else
                {
                    rightObj = null;
                    right = false;
                }
                
                if (hitLeft.collider != null)
                {
                    if(left==false)
                        Gizmos.DrawLine(basePos + new Vector2(-current,0f),hitLeft.point );
                }
                
                if (hitRight.collider != null)
                {
                    if(right==false)
                        Gizmos.DrawLine(basePos + new Vector2(current,0f),hitRight.point );
                }
                current += spaceStep;
            }
            
        }
    }

}

