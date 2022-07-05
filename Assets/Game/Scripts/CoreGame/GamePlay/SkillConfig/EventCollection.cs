using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "EventState", menuName = "CoreGame/EventState")]
    public class EventCollection : SerializedScriptableObject
    {
        [EnumToggleButtons, HideLabel]
        public AnimationTypeState typeAnim;
        [LabelText("$typeAnim")]
        public string NameTrigger;
        [ShowIf("typeAnim",AnimationTypeState.PlayAnim)]
        public float timeStart;

        [Header("TimeEvent")]
        [InfoBox("Duration => Hãy tính time của animation với các event ở dưới để khớp nếu có")]
        public float duration;
        public AnimationCurve curveX, curveY;
        [GUIColor(0f, 1f, 0f)]
        public AnimationCurve curveSpeedAnimation= new AnimationCurve(new Keyframe(0,1f));
        [HideReferenceObjectPicker]
        [LabelText("EVENT")]
        public List<IComboEvent> EventCombo = new List<IComboEvent>();

        //protected override void OnAfterDeserialize()
        //{
        //    //base.OnAfterDeserialize();
        //    //Modify();
        //}
        protected override void OnBeforeSerialize()
        {
            //base.OnBeforeSerialize();
            //Modify();
        }
        [Button("ACCEPT MODIFY",ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]

        public void Modify()
        {
            if (EventCombo != null)
            {
                for (int i = 0; i < EventCombo.Count; i++)
                {
                
                    EventCombo[i].id = i*100;
                }
            }
            else
            {
                EventCombo = new List<IComboEvent>();
            }
        }
    }

    public enum AnimationTypeState
    {
        Trigger,
        PlayAnim,
    
    }

}

