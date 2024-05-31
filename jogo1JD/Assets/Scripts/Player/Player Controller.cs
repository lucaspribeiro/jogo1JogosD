using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    private GameObject teste ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector2((transform.position.x + movementSpeed) * Time.deltaTime, transform.position.y);
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        // Input for vertical movement
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;
        rigidBody.velocity = movement * movementSpeed;
    }
}
