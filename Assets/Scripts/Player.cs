using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //varables
    //Player speed 
    public float moveSpeed;

    //Jumping
    public float jumpHeight;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 1.5f;

    //Ground checking
    public bool isGrounded;
    public LayerMask midground;
    public float groundCheckLength = 1.25f;

    //Coyote time
    public float coyoteTime;
    public float coyoteTimeMax = 0.2f;

    //Rigidbody
    private Rigidbody2D rb2d;

    //Animations
    private Animator anim;
    private SpriteRenderer sr;

    //character color control
    public int playerColor = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Initialization
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
        sr = GetComponent<SpriteRenderer>();

        //Reset the coyoteTime
        coyoteTime = coyoteTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal input
        float h = Input.GetAxis("Horizontal");

        MoveCharacter(h);
        CharacterColor();

        //Initialize isGrounded with whatever the Jump() function
        isGrounded = Jump();

        //coyoteTime countdown check
        if (isGrounded)
        {
            coyoteTime = coyoteTimeMax;
        }
        else
        {
            coyoteTime -= Time.deltaTime;
        }

        //coyoteTime jump alowance
        if (coyoteTime > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
        }
        //jump handling
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += (Vector2.up * Physics2D.gravity.y * (fallMultiplier * Time.deltaTime));
        }

        else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb2d.velocity += (Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier * Time.deltaTime));
        }
        //animation
        anim.SetFloat("walkSpeed", Mathf.Abs(h));
        anim.SetBool("isJumping", !isGrounded);
    }

    //movement
    void MoveCharacter(float hSpeed)
    {
        rb2d.velocity = new Vector2(hSpeed * moveSpeed, rb2d.velocity.y);
        //Flip the player left and right
        if (hSpeed < 0)
        {
            sr.flipX = true;
        }
        else if (hSpeed > 0)
        {
            sr.flipX = false;
        }
    }
    bool Jump()
    {
        bool check = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, midground);
        return check;
    }

    void CharacterColor()
    {
        if (playerColor == 1)
        {
            sr.color = Color.blue;
        }
        else if(playerColor == -1)
        {
            sr.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerColor = playerColor * -1;
        }
    }

    //collisions
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the player hits a collider that is on a gameObject with the name "WateringCan"
        if (collision.gameObject.name == "WateringCan")
        {
            //This will destroy the object you collide with (like a collectable):
            //Destroy(collision.gameObject);

            //This will switch Scenes
            //SceneManager.LoadScene("Scene2");
        }
    }
    */
}
