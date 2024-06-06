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
    //public Transform Player; // Referência ao jogador
    //public float attackDistance = 5f; // Distância para ativar a animação de ataque
    //private Animator animator; // Referência ao Animator

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


        // Calcular a distância entre o inimigo e o jogador
        //float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        // Verificar se a distância é menor ou igual à distância de ataque
        //if (distanceToPlayer <= attackDistance)
        //{
            // Ativar a animação de ataque
            //animator.SetTrigger("Attack");
        //}
        //else
        //{
            // Desativar a animação de ataque se for necessário
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

