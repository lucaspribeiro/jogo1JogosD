using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaoBot : MonoBehaviour
{
    public Vector3 position;
    public Transform braco;
    public LineRenderer linha;
    // Start is called before the first frame update
    void Start()
    {
        linha = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(position.normalized * Time.deltaTime * 25);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, braco.position);
    }
}
