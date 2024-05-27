using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject projectilePrefab; // Prefab do projétil
    public Transform firePoint; // Ponto de onde os projéteis são disparados
    public float detectionRange = 10f; // Distância em que o inimigo detecta o jogador
    public float fireRate = 2f; // Intervalo entre disparos
    public float projectileSpeed = 10f; // Velocidade do projétil

    private Transform player; // Referência ao Transform do jogador
    private float nextFireTime = 0f; // Tempo até o próximo disparo

    void Start()
    {
        // Encontre o jogador na cena
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Verifique se o jogador está dentro da faixa de detecção
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // Verifique se é hora de disparar
            if (Time.time >= nextFireTime)
            {
                FireProjectile();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void FireProjectile()
    {
        // Instancie o projétil no ponto de disparo
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Adicione velocidade ao projétil para a esquerda
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-projectileSpeed, 0);

        // Destrua o projétil após um certo tempo para evitar sobrecarregar a cena
        Destroy(projectile, 5f);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        //if (collision.gameObject.tag == "Player")
        //{
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            //collision.gameObject.GetComponent<Player>().enabled = false;
            //collision.gameObject.GetComponent<Animator>().SetBool("Jump", false);
        //}
    //}
}
