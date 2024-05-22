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

    void Start()
    {   
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
            if(inFloor)
            {
                rbPlayer.velocity = Vector2.zero;
                rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                inFloor = false;
                doubleJump = true;
            }
            else if (!inFloor && doubleJump)
            {
                rbPlayer.velocity = Vector2.zero;
                rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                inFloor = false;
                doubleJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ground") 
        {
            inFloor = true;
            doubleJump = false;
        }
    }
}
