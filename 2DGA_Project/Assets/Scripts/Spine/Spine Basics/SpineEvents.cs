using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class shows off the event system of Spine
/// </summary>


public class SpineEvents : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    // [SpineEvent] creates a dropdown in the inspector that let's us choose from all the events on a specific 'SkeletonAnimation' instance
    // if there is no 'SkeletonAnimation' present it defaults to a simple textfield
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField = true)]
    public string eventName;

    // is used to play different audio clips, like a speaker/stereo
    public AudioSource audioSource;
    // a soundclip that is played by an 'AudioSource'
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // binding the event handler to our 'SkeletonAnimation' instance
        // if any event triggers, the bound fucntion will be called
        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
    }

    // event handler function that provides us with details about the event as well as the animtion track it happened on
    private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e) {
        // we check if the name of the event that was triggerd is the footstep event
        if ("footstep" == e.Data.Name) {
            Play();
        }
    }

    public void Play()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
