using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;

public class ComponentGameController : MonoBehaviour
{
    public static ComponentGameController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject temp = new GameObject();
                temp.name = "ComponentGameController";
                instance =  temp.AddComponent<ComponentGameController>();
            }
            return instance;
        }
    }

    private static ComponentGameController instance;
    public List<ComponentManager> componentManagers= new List<ComponentManager>();
    public List<ProjectileComponent > projectileComponents = new List<ProjectileComponent>(); 
    public List<int> InstanceIds= new List<int>();
    public void AddComponent(ProjectileComponent component)
    {
        if (!projectileComponents.Contains(component))
        {
            projectileComponents.Add(component);
        }
    }
    public void AddComponent(ComponentManager component)
    {
        if (!componentManagers.Contains(component))
        {
            componentManagers.Add(component);
            InstanceIds.Add(component.gameObject.GetInstanceID());
        }
    }
    public void ResetAll()
    {
        componentManagers.Clear();
        projectileComponents.Clear();
        InstanceIds.Clear();
    }
}
