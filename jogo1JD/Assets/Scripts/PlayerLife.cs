using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public bool alive = true;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void LoseLife()
    {
        if(alive == true)
        {
            alive = false;
        }
    }
}
