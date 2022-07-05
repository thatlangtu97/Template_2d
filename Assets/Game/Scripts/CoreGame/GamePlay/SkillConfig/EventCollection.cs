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
        [InfoBox("Duration => Hãy tính time của animation với các event ở dưới để khớp nếu có")]
        [EnumToggleButtons, HideLabel] 
        public AnimationTypeState typeAnim;
        [LabelText("$typeAnim")] 
        public string nameTrigger;
        [ShowIf("typeAnim",AnimationTypeState.PlayAnim)]
        public float timeStart;
        public float duration;
        public AnimationCurve curveVelocityX, curveVelocityY;
        [GUIColor(0f, 1f, 0f)]
        public AnimationCurve curveSpeedAnimation= new AnimationCurve(new Keyframe(0,1f));
        [HideReferenceObjectPicker]
        [LabelText("EVENT")]
        public List<IComboEvent> eventCombo = new List<IComboEvent>();

        //protected override void OnAfterDeserialize()
        //{
        //    //base.OnAfterDeserialize();
        //    //Modify();
        //}
//        protected override void OnBeforeSerialize()
//        {
//            //base.OnBeforeSerialize();
//            //Modify();
//        }
        [Button("ACCEPT MODIFY",ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1),]

        public void Modify()
        {
            if (eventCombo != null)
            {
                for (int i = 0; i < eventCombo.Count; i++)
                {
                
                    eventCombo[i].id = i*100;
                }
            }
            else
            {
                eventCombo = new List<IComboEvent>();
            }
        }
    }

    public enum AnimationTypeState
    {
        PlayAnim,
        Trigger,
    }

}

