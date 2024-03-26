using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the V(iew) Part of the MVC Model
/// This class is responsible for changing the animations/sprites/looks of the character
/// </summary>

public class PlayerCharacterView : MonoBehaviour
{
    #region variables
    public PlayerCharacterModel model;
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset run, idle, jump;

    PlayerCharacterModel.PlayerState previousState;
    #endregion

    // Update is called once per frame
    void Update()
    {
        // flip the character if current direction does not equal the facing direction from the model
        if (skeletonAnimation.Skeleton.ScaleX < 0 != model.facingLeft) {
            Turn(model.facingLeft);
        }

        // the new current PlayerState is read from the model
        PlayerCharacterModel.PlayerState currentState = model.state;

        // if the state in the previous loop is not matching the current one we need to adjust to a new animation
        if (previousState != currentState) { 
            PlayNewAnimation();
        }

        // the previous state will be the current state in the next loop
        previousState = currentState;
    }

    // sets a new antimation to be played depending on the current PlayerState
    void PlayNewAnimation() {
        PlayerCharacterModel.PlayerState newstate = model.state;
        Spine.Animation nextAnimation;

        if (newstate == PlayerCharacterModel.PlayerState.Jumping)
        {
            nextAnimation = jump;
        }
        else {
            if (newstate == PlayerCharacterModel.PlayerState.Running)
            {
                nextAnimation = run;
            }
            else {
                nextAnimation = idle;
            }
        }

        // set a new animation for the character immediatelly
        skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
    }

    public void Turn(bool facingLeft) {
        skeletonAnimation.Skeleton.ScaleX = facingLeft ? -1 : 1f;
    }
}
