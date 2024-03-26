using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This class controls every functionality that controls the player character, their collision handling as well as it's animations
/// </summary>

public class PlayerController : MonoBehaviour, IDamageable
{
    #region variables
    // [SerializeField] - used to display private variables in the inspector 
    // https://docs.unity3d.com/ScriptReference/SerializeField.html
    #region movement variables
    private float direction;
    [SerializeField]
    private float speed = 5f;
    #endregion

    #region jump variables
    [SerializeField]
    private float jumpPower = 5f;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    #endregion

    #region flip character
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    #endregion

    #region animation
    private Animator anim;

    // Enumerations allow you to create a collection of related constants.
    private enum MovementState { idle, movement, jump, fall, attack, death }
    private MovementState state;

    private bool immovable = false;
    #endregion

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int points = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // search for the instance of the component on the object the script is present or on all child objects
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        /* Ideally you want to check if the components you are getting are not null e.g. 
         * 
         * if (rb == null) {
         *      do something here to prevent error
         * }
         * 
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (!immovable) {
            // get the input from Left and Right Arrows or A and D
            // to change input keys go to "Edit<Project Settings< Input Manager"
            direction = Input.GetAxis("Horizontal");
            // we set the speed paramter from the blend tree in the animator by assignin our input
            // since the speed parametere should only be a positive number, but Input.GetAxis() returns as a value between 0.1 and -0.1, we ue the absolute value
            anim.SetFloat("speed", Mathf.Abs(direction));
            // apply the input to transform component
            // don't forget Time.deltaTime to be framerate independent!
            transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

            Flip();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) {
            // apply force as an impulse to the rigidbody of the player
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }

        UpdateAnimations();
    }

    // change the state of our character and assign the state to the animator
    private void UpdateAnimations()
    {
        // if we get an input in the x direction we want to display a movement animation, otherwise idle
        if (direction != 0)
        {
            state = MovementState.movement;
        }
        else
        {
            state = MovementState.idle;
        }

        // we check if the velocity of our character is going upward or downward and adjust the animation accordingly
        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            state = MovementState.attack;
        }

        if (health <= 0) {
            state = MovementState.death;
        }

        // our script state is assigned to the parameter with the name "state" inside the animator
        // this is used as a condition to transition between different animtions
        anim.SetInteger("state", (int)state);
    }

    // used as an animationEvent to stop the characters movement
    public void SwitchImmovable()
    {
        immovable = !immovable;
    }

    // triggers when player collides with another collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if the gameobject the player has collided with has the corresponted tag
        if(collision.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.TryGetComponent(out ICollectable collectableObj)) {
            collectableObj.PickUp();
            points++;

            print(points);
        }
    }

    private void Flip() {
        // check which direction the player is facing and if the direction pressed is the opposite of facing direction
        if (facingRight && direction > 0f || !facingRight && direction < 0f) {
            //invert the direction and flip the sprite on the x-Axis
            facingRight = !facingRight;
            spriteRenderer.flipX = facingRight;
        }
    }

    void IDamageable.TakeDamage(int damage) {
        health -= damage;
        print(health);

        if (health <= 0)
        {
            // disabled so animation is played
            //Destroy(gameObject);
        } 
    }
}
