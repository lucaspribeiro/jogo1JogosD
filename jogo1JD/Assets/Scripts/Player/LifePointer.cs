using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePointer : MonoBehaviour
{
    [Range(0, 100)]
    public float playerLife = 100f;
    //public LifePointer lifePointer;

    private float startAngle = 40f;
    private float endAngle = -220f;

    void Start()
    {
        
    }

    void Update()
    {   
        float angle = Mathf.Lerp(startAngle, endAngle, (100 - playerLife) / 100 );

        transform.rotation = Quaternion.Euler(0, 0, angle);
        
    }

    public void SetPlayerLife(float life)
    {
        playerLife = Mathf.Clamp(life, 0f, 100f);
    }
}
