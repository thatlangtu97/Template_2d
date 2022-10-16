using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.GamePlay
{
    public static class ComponentManagerUtils
    {
        //Cach luu component 
        static Dictionary<int, ProjectileComponent> projectileComponentByInstanceId = new Dictionary<int, ProjectileComponent>();
        static Dictionary<int, ComponentManager> componentByInstanceId = new Dictionary<int, ComponentManager>();
        static Dictionary<int, Rigidbody2D> rigidbodyByInstanceId = new Dictionary<int, Rigidbody2D>();
        static Dictionary<int, HitBoxComponent> hitBoxComponents = new Dictionary<int, HitBoxComponent>();
        public static void AddComponent(ComponentManager component)
        {
            int instanceId = component.gameObject.GetInstanceID();
            if (!componentByInstanceId.Keys.Contains(instanceId))
            {
                componentByInstanceId.Add(instanceId,component);
            }
        }
        public static void AddComponent(ProjectileComponent component)
        {
            int instanceId = component.gameObject.GetInstanceID();
            if (!projectileComponentByInstanceId.Keys.Contains(instanceId))
            {
                projectileComponentByInstanceId.Add(instanceId,component);
            }
        }
        public static void AddComponent(Rigidbody2D component)
        {
            int instanceId = component.gameObject.GetInstanceID();
            if (!rigidbodyByInstanceId.Keys.Contains(instanceId))
            {
                rigidbodyByInstanceId.Add(instanceId,component);
            }
        }
        
        public static void AddComponent(HitBoxComponent component)
        {
            int instanceId = component.gameObject.GetInstanceID();
            if (!hitBoxComponents.Keys.Contains(instanceId))
            {
                hitBoxComponents.Add(instanceId,component);
            }
        }
        public static void ResetAll()
        {
            foreach(ComponentManager temp in componentByInstanceId.Values)
            {
                if (temp != null)
                {
                    temp.DestroyEntity();
                }
            }

            foreach (ProjectileComponent temp in projectileComponentByInstanceId.Values)
            {
                if (temp != null)
                {
                    temp.DestroyEntity();
                }
            }
            componentByInstanceId.Clear();
            projectileComponentByInstanceId.Clear();
            rigidbodyByInstanceId.Clear();
        }


        public static ComponentManager GetComponentByInstanceId(int id)
        {
            return componentByInstanceId[id];
        }
        public static Rigidbody2D GetRigidbodyByInstanceId(int id)
        {
            return rigidbodyByInstanceId[id];
        }
        public static HitBoxComponent GetHitBoxByInstanceId(int id)
        {
            return hitBoxComponents[id];
        }
    }

}

