using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "ResourceIconCollection", menuName = "Data/ResourceIconCollection")]
public class ResourceIconCollection : ScriptableObject
{
    public List<ResourceIconConfig> configs;

    public Sprite GetResourceIcon(CurrencyType type)
    {
        for (int i = 0; i < configs.Count; i++)
        {
            if (type == configs[i].type)
            {
                return configs[i].sprite;
            }
        }

        return null;
    }

    public Sprite GetResourceBackGround(CurrencyType type)
    {
        for (int i = 0; i < configs.Count; i++)
        {
            if (type == configs[i].type)
            {
                return configs[i].spriteBackGround;
            }
        }

        return null;
    }
}
[System.Serializable]
public class ResourceIconConfig
{
    [PreviewField]
    public Sprite sprite;
    [PreviewField]
    public Sprite spriteBackGround;


    public CurrencyType type;
}