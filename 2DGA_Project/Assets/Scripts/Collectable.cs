using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls every functionality for a collectable and can be reused for each collectable or used a base for a more complex one
/// it inherits from the ICollectable interface and defines a functionality for the required function
/// </summary>

public class Collectable : MonoBehaviour, ICollectable
{
    void ICollectable.PickUp() {
        Destroy(gameObject);
    }
}
