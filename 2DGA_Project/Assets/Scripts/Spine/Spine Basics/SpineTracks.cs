using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class shows off the track system of Spine
/// </summary>

public class SpineTracks : MonoBehaviour
{
    [SpineAnimation]
    public string walkAnimation;

    [SpineAnimation]
    public string gunGrabAnimation;

    [SpineAnimation]
    public string gunHolsterAnimation;

    SkeletonAnimation skeletonAnimation;

    Spine.AnimationState spineAnimationState;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;

        StartCoroutine(Animation());
    }

    IEnumerator Animation() {
        // in 'SetAnimation' and 'AddAnimation' the first parameter we pass is the track number
        // we can have a seperate animation looping on track 0
        spineAnimationState.SetAnimation(0, walkAnimation, true);

        while (true)
        {
            // while another animation that controls the upper body is played on track one
            spineAnimationState.SetAnimation(1, gunGrabAnimation, false);
            yield return new WaitForSeconds(1.5f);
            spineAnimationState.SetAnimation(1, gunHolsterAnimation, false);
            yield return new WaitForSeconds(1.5f);
        }

        // higher track numbers are layed in top of lower ones 
    }
}
