using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pneu : MonoBehaviour
{

    public float speed;
    public bool ground = true;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool facingRight = true;
    public float coolDown;
    //public Transform Player; // Refer�ncia ao jogador
    //public float attackDistance = 5f; // Dist�ncia para ativar a anima��o de ataque
    //private Animator animator; // Refer�ncia ao Animator

    // Start is called before the first frame update
    void Start()
    {
        coolDown = 0;
       // animator = GetComponent<Animator>(); // Inicializa o Animator
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);

        if (ground == false && coolDown < 0)
        {
            speed *= -1;
        }
        else
        {
            coolDown -= Time.deltaTime;
        }

        if (speed > 0 && !facingRight)
        {
            Flip();
        }
        else if (speed < 0 && facingRight)
        {
            Flip();
        }


        // Calcular a dist�ncia entre o inimigo e o jogador
        //float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        // Verificar se a dist�ncia � menor ou igual � dist�ncia de ataque
        //if (distanceToPlayer <= attackDistance)
        //{
            // Ativar a anima��o de ataque
            //animator.SetTrigger("Attack");
        //}
        //else
        //{
            // Desativar a anima��o de ataque se for necess�rio
           // animator.ResetTrigger("Attack");
        //}

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

