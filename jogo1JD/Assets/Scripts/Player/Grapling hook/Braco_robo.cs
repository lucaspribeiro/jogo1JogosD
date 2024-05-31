using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braco_robo : MonoBehaviour
{
    [SerializeField] private Transform braco;
    [SerializeField] private GameObject mao;
    [SerializeField] public Transform shootPoint;
    public SpriteRenderer spriteMao;
    public bool lancaMaoValido;
    public SpringJoint2D spring;

    private MaoBot maoInstanciada;
    private Camera mainCam;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        braco = GetComponent<Transform>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        lancaMaoValido = true;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direcao = mousePos - braco.position;
        float rotZ = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        if (Input.GetMouseButtonDown(0) && lancaMaoValido)
        {
            GameObject maoInt = Instantiate(mao, shootPoint.position, Quaternion.identity);
            maoInt.GetComponent<MaoBot>().mao.transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
            maoInt.GetComponent<MaoBot>().position = direcao;
            maoInt.GetComponent<MaoBot>().shootPoint = shootPoint;
            maoInt.GetComponent<MaoBot>().braco = gameObject;
            maoInstanciada = maoInt.GetComponent<MaoBot>();
            lancaMaoValido = false;
            direcao = maoInt.GetComponent<Transform>().position - braco.position;
            rotZ = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            braco.rotation = Quaternion.Euler(0, 0, rotZ - 90);
        }
        else if (Input.GetMouseButtonUp(0) && maoInstanciada)
        {
            spring.enabled = false;
            maoInstanciada.validaVoltaMao = true;
        }
        else
        {
            if (maoInstanciada)
            {
                direcao = maoInstanciada.GetComponent<Transform>().position - braco.position;
                rotZ = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            }
            braco.rotation = Quaternion.Euler(0, 0, rotZ - 90);
        }

        spriteMao.enabled = lancaMaoValido;
    }
}
