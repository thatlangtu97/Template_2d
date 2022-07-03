using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabUtils 
{
    static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    private static Dictionary<string, Component> p = new Dictionary<string, Component>();
	public static GameObject LoadPrefab(string path)
	{
		if (!prefabs.ContainsKey(path) || prefabs[path] == null)
		{
			GameObject prefab = Resources.Load<GameObject>(path);
			if (prefab == null)
			{
				Debug.LogError("Not found prefab: " + path);
			}
			if (!prefabs.ContainsKey(path)){
				prefabs.Add(path, prefab);
			}
            else
            {
				prefabs[path] = prefab;
			}
		}
		return prefabs[path];
	}

	public static T LoadPrefab<T>(string path) where T : Component
	{
		if (!p.ContainsKey(path) || p[path] == null)
		{
			T prefab = Resources.Load<T>(path);
			if (prefab == null)
			{
				Debug.LogError("Not found prefab: " + path);
			}
			if (!p.ContainsKey(path)){
				p.Add(path, prefab);
			}
			else
			{
				p[path] = prefab;
			}
		}
		return p[path].GetComponent<T>();
	}
}
