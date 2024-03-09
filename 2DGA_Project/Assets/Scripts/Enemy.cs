using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls every functionality for an enemy or hazard
/// </summary>

public class Enemy : MonoBehaviour
{
    #region variables
    // amount of damage we want to deal damageable player or object
    [SerializeField]
    private int damageDealt = 25;
    #endregion

    // triggers when collides with another object with a 2Dcollider
    private void OnCollisionEnter2D(Collision2D collision) {
        // when the enemy collides with another object, it checks if the other gameobject has the IDamagle component
        if(collision.gameObject.TryGetComponent(out IDamageable damageableObj))
        {
            // if the component is present the enemy calls the function provided by the interface and passes on its damage
            damageableObj.TakeDamage(damageDealt);
        }
    }
}
