using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DemoAnim : MonoBehaviour
{
    public Animator anim;
    public List<string> listAnim = new List<string>();
    void Start()
    {
        listAnim = new List<string>();
        if(anim == null)
        anim = GetComponent<Animator>();
//        foreach (AnimatorControllerParameter p in anim.parameters)
//        {
//            listAnim.Add(p.name);
//        }

        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        
        
        foreach (AnimationClip p in clips)
        {
            listAnim.Add(p.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int curentAnim = 0;
    public void NextAnim()
    {
        curentAnim = (curentAnim + 1) % listAnim.Count;
        anim.Play(listAnim[curentAnim],0,0);
        
    }

    public void PlayAnim()
    {
        anim.Play(listAnim[curentAnim],0,0);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(DemoAnim))]
public class DemoAnimEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Next Anim"))
        {
            DemoAnim zone = (DemoAnim)target;
            zone.NextAnim();
        }
        if (GUILayout.Button("Play Anim"))
        {
            DemoAnim zone = (DemoAnim)target;
            zone.PlayAnim();
        }
    }
}
#endif
