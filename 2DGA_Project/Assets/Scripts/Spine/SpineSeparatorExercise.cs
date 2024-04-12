using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

/// <summary>
/// This class (and scene setup) should show the usage of the SkeletonRenderSeparator
/// </summary>


public class SpineSeparatorExercise : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public SkeletonRenderSeparator separator;

    public AnimationReferenceAsset run;
    public AnimationReferenceAsset pole;

    public float startX;
    public Transform endPos;

    float speed = 18f;

    public void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation() {
        Spine.AnimationState state = skeletonAnimation.state;

        while (true) {
            // run animation
            // set the starting positions for our object
            SetXPosition(startX);
            separator.enabled = false;
            state.SetAnimation(0, run, true);

            while (transform.localPosition.x < endPos.position.x) {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                yield return null;
            }

            // hit animation
            // set the exact end positions for our object
            SetXPosition(endPos.position.x);
            separator.enabled = true;
            // save the animation to a trackentry
            Spine.TrackEntry poleTrack = state.SetAnimation(0, pole, false);
            // so we can use it to create a break in the coroutine until the animation is finished
            yield return new WaitForSpineAnimationComplete(poleTrack);
            yield return new WaitForSeconds(1f);
        }
    }

    // helper func to set the local x positon
    void SetXPosition(float x) {
        Vector3 tp = transform.localPosition;
        tp.x = x;
        transform.localPosition = tp;
    }
}
