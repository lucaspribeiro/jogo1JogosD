using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaoBot : MonoBehaviour
{
    public Vector3 position;
    public Transform shootPoint;
    public LineRenderer linha;
    public Transform mao;
    public GameObject braco;
    public Transform baseMao;
    public float maxDist = 5;
    public bool validaVoltaMao;

    private float distancia = 0;
    private bool bateu;
    private float tempDeVida = 0;
    // Start is called before the first frame update
    void Start()
    {
        bateu = false;
        validaVoltaMao = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bateu) 
        {
            
            if (distancia <= maxDist)
            {
                transform.Translate(position.normalized * Time.deltaTime * 15);
                distancia = Vector2.Distance(shootPoint.position, transform.position);
            }
            else
            {
                Volta();
            }
        }

        if (validaVoltaMao)
        {
            Volta();
        }

        GetComponent<LineRenderer>().SetPosition(0, baseMao.position);
        GetComponent<LineRenderer>().SetPosition(1, shootPoint.position);
    }

    public void Prende()
    {
        transform.position = transform.position;
        bateu = true;
        braco.GetComponent<Braco_robo>().spring.enabled = true;
        braco.GetComponent<Braco_robo>().spring.connectedBody = GetComponent<Rigidbody2D>();
    }

    private void Volta()
    {
        position = transform.position - shootPoint.position;
        transform.Translate(position.normalized * Time.deltaTime * -50);
        if (Vector2.Distance(shootPoint.position, transform.position) <= 2 || tempDeVida > 0.9f)
        {
            braco.GetComponent<Braco_robo>().lancaMaoValido = true;
            Destroy(gameObject);
        }
        tempDeVida += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Agarravel"))
        {
            Prende();
            //print("Agarravel");
        }
        else if (collision.gameObject.CompareTag("NãoAgarravel"))
        {
            distancia = maxDist+1;
            //print("não agarravel");
        }
    }
}
