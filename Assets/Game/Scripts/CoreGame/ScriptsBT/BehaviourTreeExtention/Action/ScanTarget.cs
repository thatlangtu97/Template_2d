using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class ScanTarget : Action
    {
        public SharedComponentManager component;
        public SharedTransform target;
        public Vector2 boxScan;
        public Vector2 positionFoward;
        public LayerMask layerTarget;
        public Color colorGizmo = Color.red;
        public float timeHoldTarget = 1f;
        private float triggerHoldTarget;
        public override void OnStart()
        {
            base.OnStart();
            Collider2D[] cols = null;
            cols = Physics2D.OverlapBoxAll((Vector2)component.Value.transform.position + new Vector2(component.Value.transform.right.x * positionFoward.x, /*component.Value.transform.right.y * */positionFoward.y), boxScan, 0, layerTarget);
            if (cols != null && cols.Length > 0)
            {
                target.Value = cols[0].transform;
            }
            else
            {
                if (target.Value != null)
                {
                    triggerHoldTarget+= Time.deltaTime;
                    if (triggerHoldTarget >= timeHoldTarget)
                    {
                        triggerHoldTarget = 0;
                        target.Value = null;
                    }
                }
            }

        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = colorGizmo;
            Gizmos.DrawWireCube((Vector2)component.Value.transform.position + new Vector2(component.Value.transform.right.x * positionFoward.x, /*component.Value.transform.right.y * */ positionFoward.y), boxScan);
            
        }
    }

}

