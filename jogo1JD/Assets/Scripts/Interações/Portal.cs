using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private DataSO dataSO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            dataSO.Level = "Level 2";
            dataSO.Value1 = dataSO.Value; 
            SceneManager.LoadScene("Level 2");
        }
    }
}
