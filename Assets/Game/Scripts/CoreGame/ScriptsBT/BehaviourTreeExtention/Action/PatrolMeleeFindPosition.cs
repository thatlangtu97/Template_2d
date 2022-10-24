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
        
        public SharedVector2 patrolLeftLimit;
        public SharedVector2 patrolRightLimit;
        public SharedVector2 spawnPosition;
        public SharedComponentManager copmponent;
        public LayerMask layerLimit;
        public float spaceStep;
        public float range;
        public float distanceDown;
        public bool left = false;
        public bool right = false;
        
        public override void OnStart()
        {
            
        }

        public override TaskStatus OnUpdate()
        {
            float current = 0;
            spawnPosition.Value= (Vector2) copmponent.Value.transform.position;
            Vector2 OriginPosition = spawnPosition.Value;
            while (current<range)
            {
                RaycastHit2D hitLeft =  Physics2D.Raycast(OriginPosition + new Vector2(-current,0f), Vector2.down, distanceDown, layerLimit);
                RaycastHit2D hitRight =  Physics2D.Raycast(OriginPosition + new Vector2(current,0f), Vector2.down, distanceDown, layerLimit);

                RaycastHit2D hitWallLeft =  Physics2D.Raycast(OriginPosition , Vector2.left, current, layerLimit);
                RaycastHit2D hitWallRight =  Physics2D.Raycast(OriginPosition , Vector2.right, current, layerLimit);
                
                if (hitWallLeft.collider != null)
                {
                    left = true;
                }
                else
                {
                    left = false;
                }

                if (hitWallRight.collider != null)
                {
                    right = true;
                }
                else
                {
                    right = false;
                }
                
                if (hitLeft.collider != null)
                {
                    if(left==false)
                        patrolLeftLimit.Value = hitLeft.point ;
                }
                
                if (hitRight.collider != null)
                {
                    if (right == false)
                        patrolRightLimit.Value = hitRight.point;
                }
                current += spaceStep;
            }
            
            
            return TaskStatus.Success;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            float current = 0;

            if (copmponent == null) return;
            
            Vector2 basePos = spawnPosition.Value;
            while (current<range)
            {
                RaycastHit2D hitLeft =  Physics2D.Raycast(basePos + new Vector2(-current,0f), Vector2.down, distanceDown, layerLimit);
                RaycastHit2D hitRight =  Physics2D.Raycast(basePos + new Vector2(current,0f), Vector2.down, distanceDown, layerLimit);

                RaycastHit2D hitWallLeft =  Physics2D.Raycast(basePos , Vector2.left, current, layerLimit);
                RaycastHit2D hitWallRight =  Physics2D.Raycast(basePos , Vector2.right, current, layerLimit);
                
                if (hitWallLeft.collider != null)
                {
                    left = true;
                }
                else
                {
                    left = false;
                }

                if (hitWallRight.collider != null)
                {
                    right = true;
                }
                else
                {
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

