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
        public LayerMask layerTarget;
        public override void OnStart()
        {
            base.OnStart();
            Collider2D[] cols = null;
            cols = Physics2D.OverlapBoxAll(component.Value.transform.position, boxScan, 0, layerTarget);
            if (cols != null && cols.Length > 0)
            {
                target.Value = cols[0].transform;
            }
            else
            {
                target.Value = null;
            }

        }
    }

}

