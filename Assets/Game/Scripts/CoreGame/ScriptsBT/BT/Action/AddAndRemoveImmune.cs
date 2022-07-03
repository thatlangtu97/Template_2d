
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Sirenix.OdinInspector;

namespace CoreBT
{
    [TaskCategory("Extension")]
    public class AddAndRemoveImmune : Action
    {
        public SharedComponentManager componentManager;
        public bool add;
        [ShowIf("add")]
        public List<Immune> immunesAdd;
        
        public bool remove;
        [ShowIf("add")]
        public List<Immune> immunesRemove;
        public override void OnStart()
        {
            base.OnStart();
            if (add)
            {
                componentManager.Value.AddImunes(immunesAdd);
            }

            if (remove)
            {
                componentManager.Value.RemoveImmunes(immunesRemove);
            }
        }
    }
}