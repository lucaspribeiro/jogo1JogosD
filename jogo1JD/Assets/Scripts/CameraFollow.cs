using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float minX, maxX;
    public float minY, maxY;
    public float timeLerp;
    private void FixedUpdate()
    {
        Vector3 newPosition = player.position + new Vector3(0, 0, -10);
        //newPosition.y = 0.05f;
        newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, minX, maxX), Mathf.Clamp(newPosition.y, minY, maxY), newPosition.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
