using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject projectilePrefab; // Prefab do proj�til
    public Transform firePoint; // Ponto de onde os proj�teis s�o disparados
    public float detectionRange = 10f; // Dist�ncia em que o inimigo detecta o jogador
    public float fireRate = 2f; // Intervalo entre disparos
    public float projectileSpeed = 10f; // Velocidade do proj�til

    private Transform player; // Refer�ncia ao Transform do jogador
    private float nextFireTime = 0f; // Tempo at� o pr�ximo disparo
    private Animator anim;

    void Start()
    {
        // Encontre o jogador na cena
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifique se o jogador est� dentro da faixa de detec��o
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // Verifique se � hora de disparar
            if (Time.time >= nextFireTime)
            {

                FireProjectile();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void FireProjectile()
    {
        // Instancie o proj�til no ponto de disparo
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Adicione velocidade ao proj�til para a esquerda
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-projectileSpeed, 0);

        // Destrua o proj�til ap�s um certo tempo para evitar sobrecarregar a cena
        Destroy(projectile, 5f);
    }
}
