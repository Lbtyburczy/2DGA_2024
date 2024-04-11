using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

/// <summary>
/// This class shows how to control the skin system of Spine
/// </summary>

public class SpineSkinChange : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    // we create an array of skins we want to cycle through
    // SpineSkin annotation provides us with a dropdown for all skins of a skeletonAnimation component in the inspector
    [SpineSkin]
    public string[] skins;
    public int activeSkinIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    public void PrevSkin() {
        activeSkinIndex = (activeSkinIndex + skins.Length - 1) % skins.Length;
        // calling the setskin animations and inputting the skin name as a string can change the current skin at runtime
        // this can also be done for components of the character (e.g. face, legs, etc.)
        skeletonAnimation.skeleton.SetSkin(skins[activeSkinIndex]);
    }

    public void NextSkin() {
        activeSkinIndex = (activeSkinIndex + 1) % skins.Length;
        skeletonAnimation.skeleton.SetSkin(skins[activeSkinIndex]);
    }
}
