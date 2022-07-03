using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "AttackState", menuName = "CoreGame/State/AttackState")]
    public class AttackState : State
    {
        public List<float> timeBuffers = new List<float>();
        protected override void OnBeforeSerialize()
        {
            if (eventCollectionData == null) return;
            if (eventCollectionData.Count==0 ) return;

            if (timeBuffers.Count < eventCollectionData.Count)
            {
                timeBuffers.Add(0f);
            }
            else
            {
                if (timeBuffers.Count > eventCollectionData.Count)
                {
                    timeBuffers.RemoveAt(timeBuffers.Count-1);
                }
            }
        }
        public override void EnterState()
        {
            base.EnterState();
        }
        public override void UpdateState()
        {
            base.UpdateState();
        }
        public override void ExitState()
        {
            base.ExitState();
        }
    }

}

