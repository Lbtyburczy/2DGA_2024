using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the C(ontroller) Part of the MVC Model
/// This class is responsible for reading our inputs and calling the corresponding functions that should be triggered
/// This class does not define any functionality for any given input, this task is handled by the model
/// </summary>

public class PlayerCharacterController : MonoBehaviour
{
    #region variables
    // get the buttons we want
    public string inputAxis = "Horizontal";
    public string jumpButton = "Jump";

    // get a reference to the model
    public PlayerCharacterModel model;
    #endregion

    // Update is called once per frame
    void Update()
    {
        // read the current input for movement
        float currentHorizontal = Input.GetAxis(inputAxis);
        // call the appropriate model class that handles functionality
        model.TryMove(currentHorizontal);

        if (Input.GetButtonDown(jumpButton)) { 
            model.TryJump();
        }
    }
}
