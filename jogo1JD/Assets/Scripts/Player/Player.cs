using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rbPlayer;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private CircleCollider2D circleCollider;

    public LifePointer LifePointer;

    private float speed = 4;
    private float jumpForce = 7;
    public bool inFloor = true;
    public bool doubleJump;
    public bool isGrapling = false;

    public LayerMask groundLayer;  // LayerMask to specify the ground layer
    public float checkDistance = 0.1f;  // Distance for the Raycast check

    public GameObject braco;
    private GameController gcPlayer;
    public CoinsSO coinsSO;

    public bool isKnockDir; // é jogado para direita
    public float KBcount; // é a quantidade de tempo até ser jogado para traz
    public float KBforce;
    public float KBTime = 0.2f;

    private Color c;

    void Start()
    {
        gcPlayer = GameController.gc;
        gcPlayer.coins = coinsSO.Value;
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        c = sr.material.color;
    }

    private void FixedUpdate()
    {
        KnockPlayer();
    }

    void Update()
    {
        Jump();
    }

    public void KnockPlayer()
    {
        if(KBcount < 0)
        {
            MovePlayer();
        }
        else
        {
            if (isKnockDir)
            {
                rbPlayer.velocity = new Vector2(-KBforce, KBforce);
            }
            else
            {
                rbPlayer.velocity = new Vector2(KBforce, KBforce);
            }
            KBcount -= Time.deltaTime;
        }
    }

    void MovePlayer()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (isGrapling)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y);
        }
        else
        {
            if (Mathf.Abs(rbPlayer.velocity.x) <= speed)
            {
                rbPlayer.velocity = new Vector2(horizontalMovement * speed, rbPlayer.velocity.y);
            }
            else if ((rbPlayer.velocity.x < 0 && horizontalMovement > 0) || (rbPlayer.velocity.x > 0 && horizontalMovement < 0))
            {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x + horizontalMovement * speed, rbPlayer.velocity.y);
            }
        }
        if (horizontalMovement > 0)
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
                //rbPlayer.velocity = Vector2.zero;
                rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                inFloor = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
            coinsSO.Value += 1;
            gcPlayer.coins = coinsSO.Value;
            gcPlayer.coinsText.text = gcPlayer.coins.ToString();
        }

        if (collision.gameObject.CompareTag("Liminha"))
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

        if(collision.gameObject.CompareTag("Balinha")) 
        {
            Dano();
            StartCoroutine(BeInvencible());
            if (collision.transform.position.x >= transform.position.x)
            {
                isKnockDir = true;
            }
            else
            {
                isKnockDir = false;
            }
            KBcount = KBTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Dano();
            StartCoroutine(BeInvencible());
            if (collision.transform.position.x >= transform.position.x)
            {
                isKnockDir = true;
            }
            else
            {
                isKnockDir = false;
            }
            KBcount = KBTime;
        }
    }

    IEnumerator BeInvencible()
    {
        Physics2D.IgnoreLayerCollision(10, 7, true);
        c.a = 0.5f;
        sr.material.color = c;
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(10, 7, false);
        c.a = 1f;
        sr.material.color = c;

    }
    void CheckGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, checkDistance, groundLayer);

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

    public void Dano()
    {

        LifePointer.SetPlayerLife(LifePointer.playerLife-25);
        if(LifePointer.playerLife <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        rbPlayer.velocity = Vector2.zero;
        circleCollider.enabled = false;
        rbPlayer.bodyType = RigidbodyType2D.Kinematic;
        braco.SetActive(false);
        GetComponent<Animator>().SetBool("Dead", true);
        this.enabled = false;
        LifePointer.SetPlayerLife(0);
        Physics2D.IgnoreLayerCollision(10, 7, false);
        StartCoroutine(LoadGameOverScene());
    }

    private IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(2); 
        SceneManager.LoadScene("GameOver"); 
    }
}
