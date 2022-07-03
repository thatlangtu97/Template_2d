using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ActionData", menuName = "CoreGame/ActionData")]
public class ActionData : ScriptableObject
{
    public List<SkillData> datas;
}
[System.Serializable]
public class SkillData
{
    public int id;
    public float coolDown;
}
