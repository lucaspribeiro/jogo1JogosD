using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();

            if (playerScript != null)
            {
                // Implementar a l�gica para matar o player
                playerScript.Die();
            }

            // Destruir o proj�til ap�s a colis�o
            Destroy(gameObject);
        }
        else
        {
            // Destruir o proj�til ap�s colidir com qualquer outra coisa
            Destroy(gameObject);
        }
    }
}
