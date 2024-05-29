using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braco_robo : MonoBehaviour
{
    [SerializeField] private Transform braco;
    [SerializeField] private GameObject mao;
    [SerializeField] public Transform shootPoint;
    private Camera mainCam;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        braco = GetComponent<Transform>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direcao = mousePos - braco.position;
        float rotZ = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject maoInt = Instantiate(mao, shootPoint.position, Quaternion.identity);
            maoInt.GetComponent<MaoBot>().position = direcao;
            maoInt.GetComponent<MaoBot>().braco = shootPoint;
        }
        else
        {
            braco.rotation = Quaternion.Euler(0, 0, rotZ-90);
        }


    }
}
