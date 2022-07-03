using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;

public class TestSkeletonAnimation : MonoBehaviour
{        
    public string skinWeaponName;
    public string skinOufitName;
    public SkeletonAnimation skeletonMecanim;
    

    [Button("MOFIFY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void Modify()
    {
        Skin mixSkin = new Skin("NewSkin");
        //                                                        skeletonMecanim.runInEditMode = true;
        mixSkin.AddSkin(skeletonMecanim.skeleton.Data.FindSkin(skinWeaponName));
        mixSkin.AddSkin(skeletonMecanim.skeleton.Data.FindSkin(skinOufitName));
        skeletonMecanim.skeleton.SetSkin(mixSkin);
        skeletonMecanim.skeleton.SetSlotsToSetupPose();
    }

    public string nameAnim;
    public bool loop;
    [Button("PLAY ANIM", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void PlayAnim()
    {
        skeletonMecanim.AnimationName = nameAnim;
        skeletonMecanim.loop = loop;
    }


}
