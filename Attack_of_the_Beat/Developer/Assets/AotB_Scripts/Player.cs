using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb2d;
    private GameInput input;

    //public Vector2 location;
    public Vector2 movement;
    public char direction;
    public bool isSprinting;

    public int SprintToggleLimit;
    public int SprintToggleCount;

    public int MovementHoldLimit;
    public int MovementHoldCount;

    public float speed_walk;
    public float speed_sprint;

    public bool swordEquipped;
    public bool isAttacking;

    public bool controllerChanged;
    public bool playerControlled;
    
    void Awake()
    {
        // set initial values

        //location = new Vector2(0.0f, 0.0f);
        movement = new Vector2(0.0f, 0.0f);
        speed_walk = 5.0f;
        speed_sprint = 8.0f;
        direction = 'D';

        isSprinting = false;
        SprintToggleCount = 0;
        SprintToggleLimit = 5;

        MovementHoldCount = 0;
        MovementHoldLimit = 2;

        isAttacking = false;

        rb2d = GetComponent<Rigidbody2D>();

        swordEquipped = false;

        playerControlled = true;
        controllerChanged = false;
    }

    private void Update()
    {
        if (playerControlled != controllerChanged)
        {
            if (playerControlled)
                input = gameObject.AddComponent<PlayerInput>();
            else
                input = gameObject.AddComponent<ComputerInput>();
            controllerChanged = playerControlled;
        }

        GetInput_Attack();                                                // check input for attack (can set the isAttacking to true)
        if (!isAttacking)                                                 // if the player is not attacking, check for movement input
            GetInput_Movement();                                          // get the input for movement
        else                                                              // else the player is either attacking or standing (in both cases, the input movement should be zero)
            movement = Vector2.zero;                                      // set movement input to zero
    }

    void FixedUpdate()
    {
        UpdateDirection();
        UpdateMovement();
        UpdateCollider();
    }

    void UpdateCollider()                                                 // during each frame, each of these colliders need to be destroyed and re-made to fit the new animation sprite
    {
        Transform Player_Sprite = transform.Find("Player_Sprite");        // process for the player "body"
        Destroy(Player_Sprite.GetComponent<PolygonCollider2D>());
        Player_Sprite.gameObject.AddComponent<PolygonCollider2D>();

        Transform PlayerSword_Sprite = transform.Find("PlayerSword_Sprite");  // process for the player sword
        Destroy(PlayerSword_Sprite.GetComponent<PolygonCollider2D>());
        PlayerSword_Sprite.gameObject.AddComponent<PolygonCollider2D>();

        Transform PlayerSwordCharge_Sprite = transform.Find("PlayerSwordCharge_Sprite");  // process for the player sword's charge
        Destroy(PlayerSwordCharge_Sprite.GetComponent<PolygonCollider2D>());
        if (isAttacking)
        {
            PlayerSwordCharge_Sprite.gameObject.AddComponent<PolygonCollider2D>();
        }
    }

    void GetInput_Attack()
    {
        if (input.GetAttackButton())                                  // if the Attack Button is down, then player should attack
        {
            isAttacking = true;
        }
    }

    void GetInput_Movement() // get the movement from player/controller input
    {
        movement = new Vector2(input.GetAxisRaw("Horizontal"), input.GetAxisRaw("Vertical")); // get the raw (-1, 0, 1) axis data

        if (input.GetSprintButton())                          // is the Sprint Button (to toggle sprinting)
        {
            if (SprintToggleCount == 0) isSprinting = !isSprinting;       // SprintToggleCount is clear, so change the sprint flag
            else SprintToggleCount++;                                     // SprintToggle is not clear, so increment the count
        }

                                                                          // the use of the SprintToggleCount is a buffer to limit 
                                                                          // the system from toggling the sprint before the user can lift off of the button

        if (SprintToggleCount == SprintToggleLimit) SprintToggleCount = 0;  // check if the SprintToggleCount has reached the limit, if so reset it to 0
    }



    void UpdateMovement()                                                 // update the player movement, based on the new movement data
    {
        if (movement.magnitude > 0.0f)                                    // if the magnitude is greater than 0, then character should start/continue moving
        {
            if (isSprinting)                                              // if player is sprinting
                rb2d.velocity = movement * speed_sprint;                  // make velocity equal to the new movement times the sprint speed
            else                                                          // else player is walking
                rb2d.velocity = movement * speed_walk;                    // make velocity equal to the new movement times the walk speed

            MovementHoldCount = MovementHoldLimit;                        // we are moving, so set the count to the max limit;
        }
        else                                                              // else, the character is/should begin standing (maybe)
        {
            if (MovementHoldCount > 0)                                    // if the count is greater than 0, then keep moving for some updates (incase the player is changing direction, but not wanting to fully stop)
                MovementHoldCount--;                                      // lower the count (it will fill if the player resumes movement)
            else                                                          // count is 0 for this "movement buffer", so movement can stop and the player will stand
            {
                rb2d.velocity = Vector2.zero;                             // set velocity to 0
                isSprinting = false;                                      // set sprinting to false, incase the player was sprinting
            }
        }
        
    }

    void UpdateDirection()                                                // change the direction the character is facing
    {
        if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))                // is the movement in the y direction greater than x (give direction to vertical)
        {
            if (movement.y > 0.0f)                                        // if y > 0, then up
                direction = 'U';
            else                                                          // else down
                direction = 'D';
        }  
        else if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))           // is movement x > y (give direction to horizontal)
        {
            if (movement.x > 0.0f)                                        // if x > 0, then right
                direction = 'R';
            else                                                          // else left
                direction = 'L';
        }

    }



    void AttackEnded()                                                    // change the isAttacking flag to false, meant to be used by animation events (to know when an attack animation has ended)
    {
        isAttacking = false;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }*/
}
