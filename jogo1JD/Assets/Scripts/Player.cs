using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private Animator playerAnim;
    [SerializeField]
    private Rigidbody2D rbPlayer;
    [SerializeField]
    private SpriteRenderer sr;
    
    private float speed = 5;
    private float jumpForce = 6;
    public bool inFloor = true;
    public bool doubleJump;

    public LayerMask groundLayer;  // LayerMask to specify the ground layer
    public float checkDistance = 0.1f;  // Distance for the Raycast check

    private GameController gcPlayer;
    void Start()
    {
        gcPlayer = GameController.gc;
        gcPlayer.coins = 0;
        //playerAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    void Update()
    {
        Jump();
    }

    void MovePlayer()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        rbPlayer.velocity = new Vector2(horizontalMovement * speed, rbPlayer.velocity.y);
    
        if(horizontalMovement > 0)
        {
            sr.flipX = false;
        }

        else if (horizontalMovement < 0)
        {
            sr.flipX = true;
        }
    }

    void Jump()
    {   
        if (Input.GetButtonDown("Jump"))
        {
            CheckGrounded();
            if (inFloor)
            {
                rbPlayer.velocity = Vector2.zero;
                rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                inFloor = false;
            }
        }
    }

 

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coins")
        {
            Destroy(collision.gameObject);
            gcPlayer.coins += 1;
            gcPlayer.coinsText.text = gcPlayer.coins.ToString();
        }

        if(collision.gameObject.tag == "Liminha")
        {
            rbPlayer.velocity = Vector2.zero;
            rbPlayer.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            collision.gameObject.GetComponent<Liminha>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collision.gameObject, 1f);
        }
    }

    void CheckGrounded()
    {
        // Position to start the Raycast (usually the bottom of the character)
        Vector2 position = transform.position;
        // Direction to cast the Ray (downwards)
        Vector2 direction = Vector2.down;

        // Perform the Raycast
        RaycastHit2D hit = Physics2D.Raycast(position, direction, checkDistance, groundLayer);

        // Check if the Raycast hit something
        if (hit.collider != null)
        {
            inFloor = true;
        }
        else
        {
            inFloor = false;
        }
    }

    public bool IsGrounded()
    {
        return inFloor;
    }
}

    