using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liminha : MonoBehaviour
{
    //private Animator liminhaAnim;
    public float speed;
    public bool ground = true;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool facingRight = true;
    public float coolDown;

    void Start()
    {
        //liminhaAnim = GetComponent<Animator>();
        coolDown = 0;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);

        if(ground == false && coolDown < 0)
        {   
            speed *= -1;
        }
        else
        {
            coolDown -= Time.deltaTime;
        }

        if(speed > 0 && !facingRight) 
        {
            Flip();
        }
        else if(speed < 0 && facingRight)
        {
            Flip(); 
        }
    }

    void Flip()
    {
        coolDown = 0.1f;
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
