using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DamageTextView : MonoBehaviour
{
//    public TextMeshPro textMesh;
//    public Animator anim;
//    public string nameAnim;
//    private float timeTrigger;
//    
//    public string text
//    {
//        set
//        {
//            textMesh.text = value;
//        }
//    }
//
//    public Color color 
//    {
//        set { textMesh.color = value; }
//    }
//
//    public void PlayAnim()
//    {
//        anim.Play(nameAnim,0,0f);
//    }

    public string text
    {
        set
        {
//            int count = 0;
//            if (listSpriterender.Count < value.Length)
//            {
//                count = value.Length - listSpriterender.Count;
//            }
//
//            if (count > 0)
//            {
//                
//            }

            for (int i = listSpriterender.Count-1; i >=0; i--)
            {
                Destroy(listSpriterender[i].gameObject);
            }
            listSpriterender.Clear();

            float space = 0;
            foreach (var temp in value)
            {
                GameObject o = Instantiate(dicText[temp].Sprite, transform).gameObject;
                o .SetActive(true);
                listSpriterender.Add(o);
                space += (dicText[temp].Size.x  * delta) / 2f;
                o.transform.localPosition = new Vector3(space,0,0);
                
            }
        }
    }

    private void Start()
    {
        foreach (var temp in listSprite)
        {
            if (!dicText.ContainsKey(temp.Character))
            {
                dicText.Add(temp.Character, temp);
            }
        }
        
    }

    public List<SpriteText> listSprite;
    public SpriteText currentSprite;
    public SpriteText previosSprite;
    public List<GameObject> listSpriterender;
    public Dictionary<char, SpriteText> dicText = new Dictionary<char, SpriteText>();
    public float delta;
    public string textTest;
    private void OnGUI()
    {
        if (GUILayout.Button("Set Text"))
        {
            text = textTest;
        }
    }

    [System.Serializable]
    public class SpriteText
    {
        [PreviewField]
        public SpriteRenderer Sprite;
        public char Character;
        public Vector2 Size;
    }
}
