using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This is the M(odel) Part of the MVC Model
/// This class is responsible for all our characters functionality
/// as well as keeping track in which state the character currently is
/// </summary>

public class PlayerCharacterModel : MonoBehaviour
{
    #region variables
    public PlayerState state;
    public bool facingLeft;
    #endregion

    #region jump
    public void TryJump() {
        StartCoroutine(JumpRoutine());
    }

    IEnumerator JumpRoutine() {
        if (state == PlayerState.Jumping) {
            yield break;
        }

        state = PlayerState.Jumping;

        // our starting position in local space
        Vector3 position = transform.localPosition;
        // how long the jumo will take
        float jumpTime = 1.2f;
        float half = jumpTime * 0.5f;
        // the power of the jump, changes the jumpheight
        float jumpPower = 20f;

        // this is the first half of the jump, our character is getting propelled upwards
        for (float t = 0; t < half; t += Time.deltaTime) {
            // we calculate the power increment for the given frame
            // the power increment grows smaller with the t getting larger over time
            float powerIncrement = jumpPower * (half - t);
            // move the characters translate by the calculated power increment upwards each frame
            transform.Translate((powerIncrement * Time.deltaTime) * Vector3.up);
            // start next loop
            yield return null;
        }
        // this is the second half of the jump, which starts then the character reaches the highest point of the jump
        // our character is getting propelled downwards
        for (float t = 0; t < half; t += Time.deltaTime) {
            /* we calculate the power increment for the given frame
             * the power increment grows larger with the t getting larger over time
             * we want the power to get smaller to simulate our character getting dragged to the ground by gravity */
            float powerIncrement = jumpPower * t;
            // move the characters translate by the calculated power increment downward each frame
            transform.Translate((powerIncrement * Time.deltaTime) * Vector3.down);
            // start next loop
            yield return null;
        }
        // reset the character to starting position
        transform.localPosition = position;

        state = PlayerState.Idle;
    }
    #endregion

    #region movement
    public void TryMove(float speed) {

        // check which direction the character is facing
        if (speed != 0) {
            bool speedIsNegative = speed < 0f;
            facingLeft = speedIsNegative;
        }

        if (state != PlayerState.Jumping) {
            // this is the ternary operator, a shorthand for if/else
            // for mor info see: https://en.wikipedia.org/wiki/Ternary_conditional_operator
            state = (speed == 0) ? PlayerState.Idle : PlayerState.Running;

            /* alternative code:
             * 
                if (speed == 0)
                {
                    state = PlayerState.Idle;
                }
                else
                {
                    state = PlayerState.Running;
                }

             */
        }
    }
    #endregion

    public enum PlayerState { 
        Idle,
        Running,
        Jumping
    }
}
