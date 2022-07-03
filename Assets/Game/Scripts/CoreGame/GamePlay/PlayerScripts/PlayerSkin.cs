using System;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Spine;

public class PlayerSkin : MonoBehaviour
{
    public string skinWeaponName;
    public string skinOufitName;
    public SkeletonMecanim skeletonMecanim;
    

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

    private void Start()
    {
        //Modify();
    }
}
