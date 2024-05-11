using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] public float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        float moveX;
        moveX = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(moveX * _speed + moveX, _rb.velocity.y);
    }
}
